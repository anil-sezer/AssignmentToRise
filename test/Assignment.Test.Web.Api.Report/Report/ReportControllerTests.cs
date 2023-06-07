using System.Net;
using Assignment.Application.Report.Report;
using Assignment.Application.Report.Report.Dto;
using Assignment.Application.Shared.Dto;
using Assignment.Domain.Entities;
using Assignment.Test.Shared;
using Assignment.Web.Api.Report.Controllers;
using Assignment.Web.Core.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Assignment.Test.Web.Api.Report.Report;

public class ReportControllerTests : WebApiTestBase
{
    [Fact]
    public async Task Should_Create_Report_Async()
    {
        var mock = new Mock<IReportAppService>();
        mock.Setup(x => x.CreateReportAsync()).ReturnsAsync(new AppServiceResult
        {
            Succeed = true,
            HttpStatusCode = HttpStatusCode.OK
        });

        var actionResult = await new ReportController(mock.Object).Report();
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var apiResult = Assert.IsType<ApiResult>(okObjectResult.Value);

        okObjectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        apiResult.ResultCode.Should().Be(1);
    }
    
    [Fact]
    public async Task Should_Get_Report_Async()
    {
        var mock = new Mock<IReportAppService>();
        mock.Setup(x => x.GetReportAsync(It.IsAny<GetReportInput>())).ReturnsAsync(new AppServiceDataResult<Reports>
        {
            Succeed = true,
            HttpStatusCode = HttpStatusCode.OK
        });

        var actionResult = await new ReportController(mock.Object).Report(new GetReportInput());
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var apiDataResult = Assert.IsType<ApiDataResult<Reports>>(okObjectResult.Value);

        okObjectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        apiDataResult.ResultCode.Should().Be(1);
    }

    [Fact]
    public async Task Should_Get_All_Reports_Async()
    {
        var mock = new Mock<IReportAppService>();
        mock.Setup(x => x.GetAllReportsAsync()).ReturnsAsync(new AppServiceDataListResult<CommonDataOutput, Reports>
        {
            Succeed = true,
            HttpStatusCode = HttpStatusCode.OK
        });

        var actionResult = await new ReportController(mock.Object).Reports();
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var apiDataListResult = Assert.IsType<ApiDataListResult<CommonDataOutput, Reports>>(okObjectResult.Value);

        okObjectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        apiDataListResult.ResultCode.Should().Be(1);
    }
}
