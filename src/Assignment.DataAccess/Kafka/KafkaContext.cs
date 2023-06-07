namespace Assignment.DataAccess.Kafka;

public abstract class KafkaContext: IDisposable
{
    protected readonly string BootstrapServer = "kafka0:9092";
    
    // todo: relocate
    public readonly string KafkaError = "KafkaError:"; 

    public virtual void Dispose()
    {
    }
}
