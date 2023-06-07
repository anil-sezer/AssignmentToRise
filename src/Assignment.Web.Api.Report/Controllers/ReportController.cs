using Assignment.Application.Report.Report;
using Assignment.Application.Report.Report.Dto;
using Assignment.Application.Shared.Dto;
using Assignment.Domain.Entities;
using Assignment.Web.Core;
using Assignment.Web.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Web.Api.Report.Controllers;

public class ReportController : ApiControllerBase
{
    private readonly IReportAppService _appService;

    public ReportController(IReportAppService appService)
    {
        _appService = appService;
    }
    
    [HttpPost]
    public async Task<ActionResult<ApiResult>> Report()
    {
        var result = await _appService.CreateReportAsync();
    
        return ApiResponse(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<ApiDataResult<Reports>>> Report([FromQuery] GetReportInput input)
    {
        var result = await _appService.GetReportAsync(input);
    
        return ApiDataResponse(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<ApiDataListResult<CommonDataOutput,Reports>>> Reports()
    {
        var result = await _appService.GetAllReportsAsync();
    
        return ApiDataListResponse(result);
    }
}
