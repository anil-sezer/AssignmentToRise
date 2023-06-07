using Assignment.Application.Contact.Contact.Dto;
using Assignment.Application.Contact.Contact;
using Assignment.Domain.Entities;
using Assignment.Domain.Enums;
using Assignment.DataAccess;
using Assignment.Domain;
using FluentAssertions;
using Xunit;

namespace Assignment.Test.Application.Contact.Contact;

public class ContactAppServiceTests : AppServiceTestBase
{
    private readonly ContactDbContext _dbContext;
    private readonly IContactAppService _appService;

    public ContactAppServiceTests()
    {
        _dbContext = _dbContext = GetDefaultTestDbContext();
        _appService = new ContactAppService(_dbContext);
    }

    [Fact]
    public async Task Should_Get_Contact_Async()
    {
        // Initialize
        var ci = new[]
        {
            new ContactInfo { ContactType = ContactTypes.Email, Value = nameof(Should_Get_Contact_Async) + "@att.com" },
            new ContactInfo { ContactType = ContactTypes.Phone, Value = "905316275329" },
            new ContactInfo { ContactType = ContactTypes.Location, Value = "Istanbul" }
        };
        var c = await CreateCommonContactAsync(nameof(Should_Get_Contact_Async), ci);

        // Execute
        var result = await _appService.GetContactAsync(new GetContactInput { Id = c.Id });

        // Assert
        result.Succeed.Should().BeTrue();
        result.Data.Id.Should().Be(c.Id);
        result.Data.Name.Should().Be(c.Name);
        result.Data.ContactInfo.Length.Should().Be(ci.Length);
    }

    [Fact]
    public async Task Should_Delete_Contact_Async()
    {
        // Initialize
        var c = await CreateCommonContactAsync(nameof(Should_Delete_Contact_Async), Array.Empty<ContactInfo>());

        // Execute
        var result = await _appService.DeleteContactAsync(new DeleteContactInput { Id = c.Id });

        // Assert
        result.Succeed.Should().BeTrue();
        (await _dbContext.Contacts.FindAsync(c.Id)).Should().BeNull();
    }
    
    [Fact]
    public async Task Should_Get_Contact_List_Async()
    {
        // Initialize
        var cList = new List<Contacts>();
        for (var i = 0; i < 10; i++)
        {
            cList.Add(await CreateCommonContactAsync(nameof(Should_Delete_Contact_Async), Array.Empty<ContactInfo>()));
        }

        // Execute
        var result = await _appService.GetContactListAsync();

        // Assert
        result.Succeed.Should().BeTrue();
        result.Data.ResultCount.Should().BeGreaterOrEqualTo(cList.Count);
        result.Items.Any(x => x.Id == result.Items[0].Id).Should().BeTrue();
        result.Items.Any(x => x.Id == result.Items[9].Id).Should().BeTrue();
    }
    
    [Fact]
    public async Task Should_Add_Contact_Connection_Async()
    {
        // Initialize
        var c = await CreateCommonContactAsync(nameof(Should_Delete_Contact_Async), Array.Empty<ContactInfo>());
        var cInfo = new ContactInfo { ContactType = ContactTypes.Location, Value = "Edirne" };

        // Execute
        var result = await _appService.AddContactConnectionAsync(new AddContactConnectionInput { Id = c.Id, ContactInfo = cInfo });

        // Assert
        result.Succeed.Should().BeTrue();
        result.Data.ContactInfo.Length.Should().Be(1);
        result.Data.ContactInfo[0].ContactType.Should().Be(cInfo.ContactType);
        
        // Initialize, execute and assert Again // todo: Maybe use Theory on this instead of Fact?
        cInfo = new ContactInfo { ContactType = ContactTypes.Email, Value = $"{nameof(Should_Add_Contact_Connection_Async)}@att.com" };
        
        result = await _appService.AddContactConnectionAsync(new AddContactConnectionInput { Id = c.Id, ContactInfo = cInfo });
        
        result.Data.ContactInfo.Length.Should().Be(2);
        result.Data.ContactInfo.Any(x => x == cInfo).Should().BeTrue();
    }
    
    [Fact]
    public async Task Should_Remove_Contact_Connection_Async()
    {
        // Initialize
        var cInfo = new[]
        {
            new ContactInfo { ContactType = ContactTypes.Email, Value = nameof(Should_Remove_Contact_Connection_Async) + "@att.com" },
            new ContactInfo { ContactType = ContactTypes.Phone, Value = "905326275329" },
            new ContactInfo { ContactType = ContactTypes.Location, Value = "Muğla" }
        };
        var c = await CreateCommonContactAsync(nameof(Should_Remove_Contact_Connection_Async), cInfo);

        // Execute
        var result = await _appService.RemoveContactConnectionAsync(new RemoveContactConnectionInput { Id = c.Id, ContactInfo = cInfo[0] });

        // Assert
        result.Succeed.Should().BeTrue();
        result.Data.ContactInfo.Length.Should().Be(cInfo.Length-1);
        result.Data.ContactInfo.Any(x => x.Value == cInfo[0].Value).Should().BeFalse();
        
        // Execute and assert Again // todo: Maybe use Theory on this instead of Fact?
        result = await _appService.RemoveContactConnectionAsync(new RemoveContactConnectionInput { Id = c.Id, ContactInfo = cInfo[2] });

        result.Data.ContactInfo.Length.Should().Be(cInfo.Length-2);
        result.Data.ContactInfo.Any(x => x.Value == cInfo[1].Value).Should().BeTrue();
        result.Data.ContactInfo.Any(x => x.Value == cInfo[2].Value).Should().BeFalse();
    }
    
    [Fact]
    public async Task Should_Add_Contact_Async()
    {
        // Initialize
        var input = new AddContactInput
        {
            Name = $"Name_{nameof(Should_Add_Contact_Async)}",
            Surname = $"Surname_{nameof(Should_Add_Contact_Async)}",
            Company = $"Company_{nameof(Should_Add_Contact_Async)}",
            ContactInfo = new[]
            {
                new ContactInfo { ContactType = ContactTypes.Email, Value = nameof(Should_Add_Contact_Async) + "@att.com" },
                new ContactInfo { ContactType = ContactTypes.Phone, Value = "905316275330" },
                new ContactInfo { ContactType = ContactTypes.Location, Value = "Van" }
            }
        };

        // Execute
        var result = await _appService.AddContactAsync(input);

        // Assert
        result.Succeed.Should().BeTrue();
        var c = await _dbContext.Contacts.FindAsync(result.Data.Id);
        c.Should().NotBeNull();
        c.Name.Should().Be(input.Name);
        c.Surname.Should().Be(input.Surname);
        c.Company.Should().Be(input.Company);
        c.ContactInfo.Length.Should().Be(input.ContactInfo.Length);
    }

    private async Task<Contacts> CreateCommonContactAsync(string postfix, ContactInfo[] infos)
    {
        var c = new Contacts
        {
            Name = $"Name_{postfix}",
            Surname = $"Surname_{postfix}",
            Company = $"Company_{postfix}",
            ContactInfo = infos
        };
        _dbContext.Contacts.Add(c);
        await _dbContext.SaveChangesAsync();
        
        return c;
    }
}
