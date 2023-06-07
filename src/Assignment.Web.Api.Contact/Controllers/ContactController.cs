using Assignment.Application.Contact.Contact;
using Assignment.Application.Contact.Contact.Dto;
using Assignment.Application.Shared.Dto;
using Assignment.Domain.Entities;
using Assignment.Web.Core;
using Assignment.Web.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Web.Api.Contact.Controllers;

public class ContactController : ApiControllerBase
{
    private readonly IContactAppService _appService;

    public ContactController(IContactAppService appService)
    {
        _appService = appService;
    }
    
    [HttpGet]
    public async Task<ActionResult<ApiDataResult<Contacts>>> Contact([FromQuery] GetContactInput input)
    {
        var result = await _appService.GetContactAsync(input);
    
        return ApiDataResponse(result);
    }
    
    [HttpPost]
    public async Task<ActionResult<ApiDataResult<Contacts>>> Contact([FromBody] AddContactInput input)
    {
        var result = await _appService.AddContactAsync(input);
    
        return ApiDataResponse(result);
    }
    
    [HttpDelete]
    public async Task<ActionResult<ApiResult>> Contact([FromBody] DeleteContactInput input)
    {
        var result = await _appService.DeleteContactAsync(input);
    
        return ApiResponse(result);
    }

    [HttpGet]
    public async Task<ActionResult<ApiDataListResult<CommonDataOutput, Contacts>>> Contacts()
    {
        var result = await _appService.GetContactListAsync();

        return ApiDataListResponse(result);
    }

    [HttpPut]
    public async Task<ActionResult<ApiDataResult<Contacts>>> ContactConnection([FromBody] AddContactConnectionInput input)
    {
        var result = await _appService.AddContactConnectionAsync(input);

        return ApiDataResponse(result);
    }
    
    [HttpDelete]
    public async Task<ActionResult<ApiDataResult<Contacts>>> ContactConnection([FromBody] RemoveContactConnectionInput input)
    {
        var result = await _appService.RemoveContactConnectionAsync(input);

        return ApiDataResponse(result);
    }
}
