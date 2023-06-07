using Assignment.Application.Report.Report;
using Assignment.Application.Report.Report.Dto;
using Assignment.DataAccess.PostgreSQL;
using Assignment.Domain;
using Assignment.Domain.Entities;
using Assignment.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace Assignment.Test.Application.Report.Report;

public class ReportApplicationTests : ReportApplicationTestBase
{
    private readonly ReportDbContext _dbContext;
    private readonly IReportAppService _appService;

    public ReportApplicationTests()
    {
        _dbContext = _dbContext = GetDefaultTestDbContext();
        _appService = new ReportAppService(_dbContext);
    }
    
    [Fact(Skip = "I have no idea about how to test Kafka producer in this integration test.")]
    public async Task Should_Post_Report_Async()
    {
        var result = await _appService.CreateReportAsync();
    
        result.Succeed.Should().BeTrue();
    }

    [Fact]
    public async Task Should_Get_Report_Async()
    {
        var r =  CreateCommonReport(ReportStatuses.Ready, new Dictionary<string, int>
        {
            {"Istanbul", 55},
            {"Bursa", 4}
        });
    
        // Execute
        var result = await _appService.GetReportAsync(new GetReportInput { Id = r.Id });
    
        // Assert
        result.Succeed.Should().BeTrue();
        result.Data.Id.Should().Be(r.Id);
        result.Data.ReportStatus.Should().Be(r.ReportStatus);
        result.Data.ReportContents.Count.Should().Be(r.ReportContents.Count);
    }
    
    [Fact]
    public async Task Should_Get_All_Reports_Async()
    {
        var reportsList = new List<Reports>();
        var dict = new Dictionary<string, int>();
        
        for (var i = 0; i < 5; i++)
        {
            var status = i % 2 == 0 ? ReportStatuses.Preparing : i % 3 == 0 ? ReportStatuses.Error : ReportStatuses.Ready;
            reportsList.Add(CreateCommonReport(status, dict));
            dict.Add($"Test{i}", i);
        }
    
        // Execute
        var result = await _appService.GetAllReportsAsync();
    
        // Assert
        result.Succeed.Should().BeTrue();
        result.Data.ResultCount.Should().Be(result.Items.Count);
        result.Data.ResultCount.Should().BeGreaterOrEqualTo(reportsList.Count);
        result.Items.Any(x => x.Id == reportsList.First().Id).Should().BeTrue();
        
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
