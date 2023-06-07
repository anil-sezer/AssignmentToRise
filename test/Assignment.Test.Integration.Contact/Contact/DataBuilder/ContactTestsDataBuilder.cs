using Assignment.DataAccess;
using Assignment.Domain;
using Assignment.Domain.Entities;
using Assignment.Domain.Enums;

namespace Assignment.Test.Integration.Contact.Contact.DataBuilder;

public class ContactTestsDataBuilder
{
    private readonly ContactDbContext _dbContext;
    public static Contacts ForGetContact;
    public static Contacts ForDeleteContact;
    public static List<Contacts> ForGetContactList;
    public static Contacts ForAddContactConnection;
        
    public ContactTestsDataBuilder(ContactDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual void SeedData()
    {
        BuildTestData();
    }

    private void BuildTestData()
    {
        ForGetContact = CreateCommonContact(nameof(ForGetContact), Array.Empty<ContactInfo>());
        ForDeleteContact = CreateCommonContact(nameof(ForDeleteContact), Array.Empty<ContactInfo>());
        CreateForGetContactListService();
        ForAddContactConnection = CreateCommonContact(nameof(ForAddContactConnection), new []{new ContactInfo { ContactType = ContactTypes.Location, Value = "Çanakkale" }});
    }

    private void CreateForGetContactListService()
    {
        ForGetContactList = new List<Contacts>();
        
        for (var i = 0; i < 5; i++)
        {
            ForGetContactList.Add(CreateCommonContact(nameof(CreateForGetContactListService), Array.Empty<ContactInfo>()));
        }
    }
    
    private Contacts CreateCommonContact(string postfix, ContactInfo[] infos)
    {
        var c = new Contacts
        {
            Name = $"Name_{postfix}",
            Surname = $"Surname_{postfix}",
            Company = $"Company_{postfix}",
            ContactInfo = infos
        };
        _dbContext.Contacts.Add(c);
        _dbContext.SaveChanges();
        
        return c;
    }
}