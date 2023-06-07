using Assignment.DataAccess.PostgreSQL;
using Assignment.Domain.Entities;
using Assignment.Domain.Enums;

namespace Assignment.Test.Integration.Report.Report.DataBuilder;

public class ReportTestsDataBuilder
{
    private readonly ReportDbContext _dbContext;
    public static Reports ForGetReport;
    public static List<Reports> ForGetAllReports;
        
    public ReportTestsDataBuilder(ReportDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual void SeedData()
    {
        BuildTestData();
    }

    private void BuildTestData()
    {
        CreateForGetReportService();
        CreateForGetContactListService();
    }
    
    private void CreateForGetReportService()
    {
        ForGetReport = CreateCommonReport(ReportStatuses.Ready, new Dictionary<string, int>
        {
            {"Istanbul", 55},
            {"Bursa", 4}
        });
    }

    private void CreateForGetContactListService()
    {
        ForGetAllReports = new List<Reports>();
        var dict = new Dictionary<string, int>();
        
        for (var i = 0; i < 5; i++)
        {
            var status = i % 2 == 0 ? ReportStatuses.Preparing : i % 3 == 0 ? ReportStatuses.Error : ReportStatuses.Ready;
            ForGetAllReports.Add(CreateCommonReport(status, dict));
            dict.Add($"Test{i}", i);
        }
    }
    
    private Reports CreateCommonReport(ReportStatuses status, Dictionary<string, int> reportContents)
    {
        var r = new Reports
        {
            ReportStatus = status,
            RequestedAt = DateTime.UtcNow,
            ReportContents = reportContents
        };
        _dbContext.Reports.Add(r);
        _dbContext.SaveChanges();
        
        return r;
    }
}
