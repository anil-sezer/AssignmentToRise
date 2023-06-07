using System.Net;
using Assignment.Application.Shared.Dto;
using Assignment.Web.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Web.Core;

[ApiController]
[Route("[action]")]
[Produces("application/json")]
public class ApiControllerBase : ControllerBase
{
    protected ActionResult<ApiResult> ApiResponse(AppServiceResult appServiceResult)
    {
        var apiResult = new ApiResult
        {
            ResultCode = appServiceResult.Succeed ? 1 : 0,
            ErrorMessage = appServiceResult.ErrorMessage
        };

        return GetActionResult(appServiceResult.Succeed, appServiceResult.HttpStatusCode, apiResult);
    }
    
    protected ActionResult<ApiDataResult<TOutput>> ApiDataResponse<TOutput>(AppServiceDataResult<TOutput> appServiceDataResult) where TOutput : class
    {
        var apiResult = new ApiDataResult<TOutput>
        {
            ResultCode = appServiceDataResult.Succeed ? 1 : 0,
            Data = appServiceDataResult.Data,
            ErrorMessage = appServiceDataResult.ErrorMessage
        };

        return GetActionResult(appServiceDataResult.Succeed, appServiceDataResult.HttpStatusCode, apiResult);
    }
    
    protected ActionResult<ApiListResult<TListOutput>> ApiListResponse<TListOutput>(AppServiceListResult<TListOutput> appServiceListResult) where TListOutput : class
    {
        var apiResult = new ApiListResult<TListOutput>
        {
            ResultCode = appServiceListResult.Succeed ? 1 : 0,
            Items = appServiceListResult.Items,
            ErrorMessage = appServiceListResult.ErrorMessage
        };

        return GetActionResult(appServiceListResult.Succeed, appServiceListResult.HttpStatusCode, apiResult);
    }
    
    protected ActionResult<ApiDataListResult<TDataOutput, TListOutput>> ApiDataListResponse<TDataOutput, TListOutput>(AppServiceDataListResult<TDataOutput, TListOutput> appServiceDataListResult) where TListOutput : class where TDataOutput : class
    {
        var apiResult = new ApiDataListResult<TDataOutput, TListOutput>
        {
            ResultCode = appServiceDataListResult.Succeed ? 1 : 0,
            Data = appServiceDataListResult.Data,
            Items = appServiceDataListResult.Items,
            ErrorMessage = appServiceDataListResult.ErrorMessage
        };

        return GetActionResult(appServiceDataListResult.Succeed, appServiceDataListResult.HttpStatusCode, apiResult);
    }

    private ActionResult<TApiResult> GetActionResult<TApiResult>(bool succeed, HttpStatusCode httpStatusCode, TApiResult apiResult)
    {
        if (succeed) return Ok(apiResult);

        return httpStatusCode switch
        {
            HttpStatusCode.NotFound => NotFound(apiResult),
            HttpStatusCode.BadRequest => BadRequest(apiResult),
            HttpStatusCode.Conflict => Conflict(apiResult),
            HttpStatusCode.Unauthorized => Unauthorized(apiResult),
            _ => NoContent()
        };
    }
}