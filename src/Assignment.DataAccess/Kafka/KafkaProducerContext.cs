using Assignment.DataAccess.Configurations;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace Assignment.DataAccess.Kafka;

public sealed class KafkaProducerContext<TTopic>: KafkaContext where TTopic: KafkaDto
{
    public KafkaProducerContext()
    {
        _producer = new ProducerBuilder<Null, string>(GetConfig()).Build();
    }

    private IProducer<Null, string> _producer;
    
    public async Task<string> SendToKafkaAsync(KafkaDto obj)
    {
        var message = JsonConvert.SerializeObject(obj);
        try
        {
            var deliveryResult = await _producer.ProduceAsync(nameof(TTopic), new Message<Null, string> { Value = message });
            return $"Delivered a message to 'Offset: {deliveryResult.Offset} - Partition: {deliveryResult.Partition}'";
        }
        catch (ProduceException<Null, string> e)
        {
            Console.WriteLine(e.Message);
            return $"{KafkaError} {nameof(TTopic)}'e mesaj iletimi sırasında hata oluştu. Exception Mesajı: {e.Message}";
        }
    }

    private ProducerConfig GetConfig()
    {
        return new ProducerConfig
        {
            BootstrapServers = BootstrapServer,
            // todo: These are for batches? Maybe compression is not?
            // BatchSize = 32000, // Can be made 64000 too? // Its allocated per partition
            LingerMs = 5, // teacher said so
            CompressionType = CompressionType.Snappy // snappy is very helpful if your messages are text based, for example log lines or JSON documents. It also has a good balance of CPU / compression ratio.
        };
    }
    
    public override void Dispose()
    {
        _producer.Flush();
    }
}
