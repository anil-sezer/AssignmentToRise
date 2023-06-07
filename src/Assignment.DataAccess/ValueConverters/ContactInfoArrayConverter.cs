using Assignment.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Assignment.DataAccess.ValueConverters;

public class ContactInfoArrayConverter : ValueConverter<ContactInfo[], string>
{
    public ContactInfoArrayConverter()
        : base(
            contacts => JsonConvert.SerializeObject(contacts),
            json => JsonConvert.DeserializeObject<ContactInfo[]>(json))
    {
    }
}