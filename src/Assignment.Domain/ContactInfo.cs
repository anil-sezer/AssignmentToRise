using Assignment.Domain.Enums;

namespace Assignment.Domain;

public class ContactInfo // todo: relocate to somewhere else
{
    public ContactTypes ContactType { get; set; }
    public string Value { get; set; }
}
