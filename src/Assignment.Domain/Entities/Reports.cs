using Assignment.Domain.Enums;

namespace Assignment.Domain.Entities;

public partial class Reports : EntityBase
{
    public DateTime RequestedAt { get; set; }
    public ReportStatuses ReportStatus { get; set; }
    public Dictionary<string, int> ReportContents { get; set; }
}

