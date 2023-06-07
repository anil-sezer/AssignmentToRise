using System.Net;
using Assignment.Application.Contact.Contact;
using Assignment.Application.Contact.Contact.Dto;
using Assignment.Application.Shared.Dto;
using Assignment.Domain.Entities;
using Assignment.Test.Shared;
using Assignment.Web.Api.Contact.Controllers;
using Assignment.Web.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Assignment.Test.Web.Api.Contact.Contact;

public class ContactControllerTests : WebApiTestBase
{
    [Fact]
    public async Task Should_Add_Contact_Async()
    {
        var mock = new Mock<IContactAppService>();
        mock.Setup(x => x.AddContactAsync(It.IsAny<AddContactInput>())).ReturnsAsync(new AppServiceDataResult<Contacts>
        {
            Succeed = true,
            HttpStatusCode = HttpStatusCode.OK
        });

        var actionResult = await new ContactController(mock.Object).Contact(new AddContactInput());
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var apiResult = Assert.IsType<ApiDataResult<Contacts>>(okObjectResult.Value);

        Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
        Assert.Equal(1, apiResult.ResultCode);
    }
    
    [Fact]
    public async Task Should_Get_Contact_Async()
    {
        var mock = new Mock<IContactAppService>();
        mock.Setup(x => x.GetContactAsync(It.IsAny<GetContactInput>())).ReturnsAsync(new AppServiceDataResult<Contacts>
        {
            Succeed = true,
            HttpStatusCode = HttpStatusCode.OK
        });

        var actionResult = await new ContactController(mock.Object).Contact(new GetContactInput());
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var apiResult = Assert.IsType<ApiDataResult<Contacts>>(okObjectResult.Value);

        Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
        Assert.Equal(1, apiResult.ResultCode);
    }
    
    [Fact]
    public async Task Should_Delete_Contact_Async()
    {
        var mock = new Mock<IContactAppService>();
        mock.Setup(x => x.DeleteContactAsync(It.IsAny<DeleteContactInput>())).ReturnsAsync(new AppServiceResult
        {
            Succeed = true,
            HttpStatusCode = HttpStatusCode.OK
        });

        var actionResult = await new ContactController(mock.Object).Contact(new DeleteContactInput());
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var apiResult = Assert.IsType<ApiResult>(okObjectResult.Value);

        Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
        Assert.Equal(1, apiResult.ResultCode);
    }
    
    [Fact]
    public async Task Should_Get_Contact_List_Async()
    {
        var mock = new Mock<IContactAppService>();
        mock.Setup(x => x.GetContactListAsync()).ReturnsAsync(new AppServiceDataListResult<CommonDataOutput, Contacts>
        {
            Succeed = true,
            HttpStatusCode = HttpStatusCode.OK
        });

        var actionResult = await new ContactController(mock.Object).Contacts();
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var apiResult = Assert.IsType<ApiDataListResult<CommonDataOutput, Contacts>>(okObjectResult.Value);

        Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
        Assert.Equal(1, apiResult.ResultCode);
    }
    
    [Fact]
    public async Task Should_Add_Contact_Connection_Async()
    {
        var mock = new Mock<IContactAppService>();
        mock.Setup(x => x.AddContactConnectionAsync(It.IsAny<AddContactConnectionInput>())).ReturnsAsync(new AppServiceDataResult<Contacts>
        {
            Succeed = true,
            HttpStatusCode = HttpStatusCode.OK
        });

        var actionResult = await new ContactController(mock.Object).ContactConnection(new AddContactConnectionInput());
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var apiResult = Assert.IsType<ApiDataResult<Contacts>>(okObjectResult.Value);

        Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
        Assert.Equal(1, apiResult.ResultCode);
    }
    
    [Fact]
    public async Task Should_Remove_Contact_Connection_Async()
    {
        var mock = new Mock<IContactAppService>();
        mock.Setup(x => x.RemoveContactConnectionAsync(It.IsAny<RemoveContactConnectionInput>())).ReturnsAsync(new AppServiceDataResult<Contacts>
        {
            Succeed = true,
            HttpStatusCode = HttpStatusCode.OK
        });

        var actionResult = await new ContactController(mock.Object).ContactConnection(new RemoveContactConnectionInput());
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var apiResult = Assert.IsType<ApiDataResult<Contacts>>(okObjectResult.Value);

        Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
        Assert.Equal(1, apiResult.ResultCode);
    }
}
