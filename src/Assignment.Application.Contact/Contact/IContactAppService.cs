using Assignment.Application.Contact.Contact.Dto;
using Assignment.Application.Shared.Dto;
using Assignment.Domain.Entities;

namespace Assignment.Application.Contact.Contact;

public interface IContactAppService
{
    Task<AppServiceDataResult<Contacts>> AddContactAsync(AddContactInput input);
    Task<AppServiceDataResult<Contacts>> GetContactAsync(GetContactInput input);
    Task<AppServiceResult> DeleteContactAsync(DeleteContactInput input);
    Task<AppServiceDataListResult<CommonDataOutput, Contacts>> GetContactListAsync();
    Task<AppServiceDataResult<Contacts>> AddContactConnectionAsync(AddContactConnectionInput input);
    Task<AppServiceDataResult<Contacts>> RemoveContactConnectionAsync(RemoveContactConnectionInput input);
}
