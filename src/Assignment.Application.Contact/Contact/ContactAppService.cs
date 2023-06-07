using System.Net;
using Assignment.Application.Contact.Contact.Dto;
using Assignment.Application.Shared;
using Assignment.Application.Shared.Dto;
using Assignment.DataAccess;
using Assignment.Domain;
using Assignment.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Assignment.Application.Contact.Contact;

public class ContactAppService : AppServiceBase, IContactAppService
{
    private readonly ContactDbContext _dbContext;

    public ContactAppService(ContactDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<AppServiceDataResult<Contacts>> AddContactAsync(AddContactInput input)
    {
        var newContact = new Contacts
        {
            Name = input.Name,
            Surname = input.Surname,
            Company = input.Company,
            ContactInfo = input.ContactInfo.Select(x => new ContactInfo
            {
                ContactType = x.ContactType,
                Value = x.Value
            }).ToArray()
        };
        await _dbContext.Contacts.AddAsync(newContact);
        await _dbContext.SaveChangesAsync();

        return DataSuccess(newContact);
    }
    
    public async Task<AppServiceDataResult<Contacts>> GetContactAsync(GetContactInput input)
    {
        var result = await _dbContext.Contacts.FindAsync(input.Id);
        if (result == null) return DataError<Contacts>("Contact not found", HttpStatusCode.BadRequest);

        return DataSuccess(result);
    }
    
    public async Task<AppServiceResult> DeleteContactAsync(DeleteContactInput input)
    {
        var result = await _dbContext.Contacts.FindAsync(input.Id);
        if (result == null) return Error("Contact not found", HttpStatusCode.BadRequest);
        
        _dbContext.Contacts.Remove(result);
        await _dbContext.SaveChangesAsync();

        return Success();
    }

    public async Task<AppServiceDataListResult<CommonDataOutput, Contacts>> GetContactListAsync()
    {
        var list = await _dbContext.Contacts.OrderBy(x => x.Id).ToListAsync();

        return DataListSuccess(new CommonDataOutput(list.Count), list);
    }
    
    public async Task<AppServiceDataResult<Contacts>> AddContactConnectionAsync(AddContactConnectionInput input)
    {
        var result = await _dbContext.Contacts.FindAsync(input.Id);
        if (result == null) return DataError<Contacts>("Contact not found", HttpStatusCode.BadRequest);

        result.ContactInfo = result.ContactInfo.Append(input.ContactInfo).ToArray();
        await _dbContext.SaveChangesAsync();

        return DataSuccess(result);
    }
    
    public async Task<AppServiceDataResult<Contacts>> RemoveContactConnectionAsync(RemoveContactConnectionInput input)
    {
        var contact = await _dbContext.Contacts.FindAsync(input.Id);
        if (contact == null) return DataError<Contacts>("Contact not found", HttpStatusCode.BadRequest);

        var index = Array.FindIndex(contact.ContactInfo, x => x.ContactType == input.ContactInfo.ContactType && x.Value == input.ContactInfo.Value);
        if (index == -1)
            return DataError<Contacts>("Info not found in contact", HttpStatusCode.BadRequest);
        
        var list = contact.ContactInfo.ToList();
        list.RemoveAt(index);
        contact.ContactInfo = list.ToArray();
        await _dbContext.SaveChangesAsync();

        return DataSuccess(contact);
    }
}
