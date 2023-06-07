namespace Assignment.DataAccess.Kafka;

public abstract class KafkaContext: IDisposable
{
    protected readonly string BootstrapServer = "kafka-0";
    
    // todo: relocate
    public readonly string KafkaError = "KafkaError:"; 
    protected readonly string DestinationFileConsumerGroupPrefix = "Group_";

    public virtual void Dispose()
    {
    }
}
