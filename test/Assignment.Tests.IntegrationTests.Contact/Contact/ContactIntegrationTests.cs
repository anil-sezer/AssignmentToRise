using Xunit;
using System.Net;
using System.Text;
using Assignment.Application.Contact.Contact.Dto;
using Assignment.Application.Shared.Dto;
using FluentAssertions;
using Assignment.Domain;
using Assignment.Domain.Enums;
using Assignment.Domain.Entities;
using Assignment.Web.Core.Models;
using Assignment.Domain.Extensions;
using Microsoft.AspNetCore.Mvc.Testing;
using Assignment.Tests.IntegrationTests.Contact.DataBuilder;
using Assignment.Tests.IntegrationTests.CustomWebApplicationFactories;

namespace Assignment.Tests.IntegrationTests.Contact;

public class ContactIntegrationTests : IntegrationTestBase, IClassFixture<ContactWebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public ContactIntegrationTests(ContactWebApplicationFactory<Program> factory):base(factory)
    {
        _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    [Fact]
    public async Task Should_Add_Contact_Async()
    {
        var input = new AddContactInput
        {
            Name = $"Name_{nameof(Should_Add_Contact_Async)}",
            Surname = $"Surname_{nameof(Should_Add_Contact_Async)}",
            Company = $"Company_{nameof(Should_Add_Contact_Async)}",
            ContactInfo = new[]
            {
                new ContactInfo { ContactType = ContactTypes.Email, Value = nameof(Should_Add_Contact_Async) + "@att.com" },
                new ContactInfo { ContactType = ContactTypes.Phone, Value = "905316275320" },
                new ContactInfo { ContactType = ContactTypes.Location, Value = "Adana" }
            }
        };

        var requestMessage = new HttpRequestMessage(HttpMethod.Post,  "/Contact");
        requestMessage.Content = input.ToStringContent(Encoding.UTF8, "application/json");

        var responseApiResult = await _httpClient.SendAsync(requestMessage);
        responseApiResult.StatusCode.Should().Be(HttpStatusCode.OK);

        var apiDataResult = (await responseApiResult.Content.ReadAsStringAsync()).FromStringToObject<ApiDataResult<Contacts>>();
        apiDataResult.ResultCode.Should().Be(1);
        apiDataResult.Data.Name.Should().Be(input.Name);
    }

    [Fact]
    public async Task Should_Get_Contact_Async()
    {
        var input = new GetContactInput { Id = ContactTestsDataBuilder.ForGetContact.Id };

        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/Contact?{input.ToQueryString()}");
        var responseApiResult = await _httpClient.SendAsync(requestMessage);
        responseApiResult.StatusCode.Should().Be(HttpStatusCode.OK);

        var apiDataResult = (await responseApiResult.Content.ReadAsStringAsync()).FromStringToObject<ApiDataResult<Contacts>>();
        apiDataResult.ResultCode.Should().Be(1);
        Assert.True(apiDataResult.Data.Id == input.Id);
    }
    
    [Fact]
    public async Task Should_Delete_Contact_Async()
    {
        var input = new DeleteContactInput { Id = ContactTestsDataBuilder.ForDeleteContact.Id };

        var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"/Contact");
        requestMessage.Content = input.ToStringContent(Encoding.UTF8, "application/json");

        var responseApiResult = await _httpClient.SendAsync(requestMessage);
        responseApiResult.StatusCode.Should().Be(HttpStatusCode.OK);

        var apiDataResult = (await responseApiResult.Content.ReadAsStringAsync()).FromStringToObject<ApiResult>();
        apiDataResult.ResultCode.Should().Be(1);
    }
    
    [Fact]
    public async Task Should_Get_Contact_List_Async()
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/Contacts");
        var responseApiResult = await _httpClient.SendAsync(requestMessage);
        responseApiResult.StatusCode.Should().Be(HttpStatusCode.OK);

        var apiDataListResult = (await responseApiResult.Content.ReadAsStringAsync()).FromStringToObject<ApiDataListResult<CommonDataOutput, Contacts>>();
        apiDataListResult.ResultCode.Should().Be(1);
        apiDataListResult.Items.Count.Should().BeGreaterOrEqualTo(ContactTestsDataBuilder.ForGetContactList.Count);
        apiDataListResult.Items.Any(x => x.Id == ContactTestsDataBuilder.ForGetContactList.First().Id).Should();
    }
    
    [Fact]
    public async Task Should_Add_Contact_Connection_Async()
    {
        var input = new AddContactConnectionInput
        {
            Id = ContactTestsDataBuilder.ForAddContactConnection.Id,
            ContactInfo = new ContactInfo { ContactType = ContactTypes.Email, Value = nameof(Should_Add_Contact_Connection_Async) + "@att.com" }
        };

        var requestMessage = new HttpRequestMessage(HttpMethod.Put,  "/ContactConnection");
        requestMessage.Content = input.ToStringContent(Encoding.UTF8, "application/json");

        var responseApiResult = await _httpClient.SendAsync(requestMessage);
        responseApiResult.StatusCode.Should().Be(HttpStatusCode.OK);

        var apiDataResult = (await responseApiResult.Content.ReadAsStringAsync()).FromStringToObject<ApiDataResult<Contacts>>();
        apiDataResult.ResultCode.Should().Be(1);
        apiDataResult.Data.ContactInfo.Any(x => x == input.ContactInfo).Should();
    }
    
    [Fact]
    public async Task Should_Remove_Contact_Connection_Async()
    {
        var input = new AddContactConnectionInput
        {
            Id = ContactTestsDataBuilder.ForAddContactConnection.Id,
            ContactInfo = new ContactInfo { ContactType = ContactTypes.Email, Value = nameof(Should_Add_Contact_Connection_Async) + "@att.com" }
        };

        var requestMessage = new HttpRequestMessage(HttpMethod.Put,  "/ContactConnection");
        requestMessage.Content = input.ToStringContent(Encoding.UTF8, "application/json");

        var responseApiResult = await _httpClient.SendAsync(requestMessage);
        responseApiResult.StatusCode.Should().Be(HttpStatusCode.OK);

        var apiDataResult = (await responseApiResult.Content.ReadAsStringAsync()).FromStringToObject<ApiDataResult<Contacts>>();
        apiDataResult.ResultCode.Should().Be(1);
        apiDataResult.Data.ContactInfo.Any(x => x == input.ContactInfo).Should();
    }
}
