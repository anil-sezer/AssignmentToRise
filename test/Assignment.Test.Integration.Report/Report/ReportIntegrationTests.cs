using System.Net;
using Assignment.Application.Report.Report.Dto;
using Assignment.Application.Shared.Dto;
using Assignment.Domain.Entities;
using Assignment.Domain.Extensions;
using Assignment.Test.Integration.Report.CustomWebApplicationFactories;
using Assignment.Test.Integration.Report.Report.DataBuilder;
using Assignment.Web.Core.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Assignment.Test.Integration.Report.Report;

public class ReportIntegrationTests : IntegrationTestBaseForReport, IClassFixture<ReportWebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public ReportIntegrationTests(ReportWebApplicationFactory<Program> factory):base(factory)
    {
        _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    [Fact(Skip = "I have no idea about how to test Kafka producer in this integration test.")]
    public async Task Should_Post_Report_Async()
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Report");
        var responseApiResult = await _httpClient.SendAsync(requestMessage);
        responseApiResult.StatusCode.Should().Be(HttpStatusCode.OK);

        var apiResult = (await responseApiResult.Content.ReadAsStringAsync()).FromStringToObject<ApiResult>();
        apiResult.ResultCode.Should().Be(1);
    }
    
    [Fact]
    public async Task Should_Get_Report_Async()
    {
        var input = new GetReportInput { Id = ReportTestsDataBuilder.ForGetReport.Id };

        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/Report?{input.ToQueryString()}");
        var responseApiResult = await _httpClient.SendAsync(requestMessage);
        responseApiResult.StatusCode.Should().Be(HttpStatusCode.OK);

        var apiDataResult = (await responseApiResult.Content.ReadAsStringAsync()).FromStringToObject<ApiDataResult<Reports>>();
        apiDataResult.ResultCode.Should().Be(1);
        apiDataResult.Data.Id.Should().Be(input.Id);
    }
    
    [Fact]
    public async Task Should_Get_All_Reports_Async()
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/Reports");
        var responseApiResult = await _httpClient.SendAsync(requestMessage);
        responseApiResult.StatusCode.Should().Be(HttpStatusCode.OK);

        var apiDataListResult = (await responseApiResult.Content.ReadAsStringAsync()).FromStringToObject<ApiDataListResult<CommonDataOutput, Reports>>();
        apiDataListResult.ResultCode.Should().Be(1);
        apiDataListResult.Data.ResultCount.Should().BeGreaterOrEqualTo(ReportTestsDataBuilder.ForGetAllReports.Count);
        apiDataListResult.Items.Any(x => x.Id == ReportTestsDataBuilder.ForGetAllReports.First().Id).Should().BeTrue();
    }
}
