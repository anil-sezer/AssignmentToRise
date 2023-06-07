using Assignment.DataAccess.Configurations;
using Assignment.Domain.Extensions;

namespace Assignment.DataAccess.Kafka.Configurations;

public class CompileReportCommandTopic: KafkaDto
{
    public CompileReportCommandTopic(string message)
    {
        Message = message;
        SentDateTimeString = DateTime.UtcNow.ToTurkishShortTimeString();
    }
    public string Message { get; set; }
    public string SentDateTimeString { get;}
}
