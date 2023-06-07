using Assignment.DataAccess.Configurations;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace Assignment.DataAccess.Kafka;

public sealed class KafkaConsumerContext<TTopic>: KafkaContext
{
    private readonly IConsumer<Ignore, string> _consumer;
    private static bool _isThereSomethingToCommit = false;

    public KafkaConsumerContext()
    {
        _consumer = new ConsumerBuilder<Ignore, string>(GetConfig(nameof(TTopic))).Build();
        
        _consumer.Subscribe(nameof(TTopic));
    }

    /// <summary>
    /// It is paramount to update offset after every "successful" consumption. Otherwise kafka will send the same message again and again. Offsets can be auto updated.
    /// </summary>
    public T GetOneMessage<T>(CancellationToken stoppingToken) where T : KafkaDto
    {
        ConsumeResult<Ignore, string> consumeResult;
        try
        {
            consumeResult = _consumer.Consume(stoppingToken);
        }
        catch (ConsumeException e)
        {
            // todo: add Serilog if you can spare time.
            // Log.Warning($"{KafkaError} Exception message: {e.Message} {Environment.NewLine}" + $"Trace: {e.StackTrace}");
            _consumer.Dispose();
            throw;
        }

        if (consumeResult == null || string.IsNullOrEmpty(consumeResult.Message.Value))
        {
            return default;
        }

        return JsonConvert.DeserializeObject<T>(consumeResult.Message.Value, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error });
    }
    
    private ConsumerConfig GetConfig(string consumerGroupName)
    {
        return new ConsumerConfig
        {
            GroupId = consumerGroupName + "Group",
            BootstrapServers = BootstrapServer,
            EnableAutoCommit = false,
            AutoOffsetReset = AutoOffsetReset.Earliest, 
            CheckCrcs = true
        };
    }

    public override void Dispose()
    {
        try
        {
            if (_isThereSomethingToCommit) _consumer.Commit();
            
            _consumer.Close();
        }
        catch (ObjectDisposedException e)
        {
            Console.WriteLine($"Dispose edilmiş Kafka'da yeniden dispose denendi. Dizayn hatası. Exception altta yakalanırsa fırlatılmaya devam edilmeli, en son business kodda yakalanmalı, dispose sadece orada tetiklenmeli. Exception: {e.Message} {Environment.NewLine}" + $"Trace: {e.StackTrace}");
        } 
        catch (Exception e)
        {
            Console.WriteLine($"Kafka commit edilirken hata oluştu, handle edildi. Muhtemelen önemli bir şey değildir. Sık geliyorsa bi bakmak lazım ama, dizayn hatası olabilir. Exception: {e.Message} {Environment.NewLine}" + $"Trace: {e.StackTrace}");
        }
    }
}
