namespace Assignment.Domain.Entities;

public partial class Contacts : EntityBase
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Company { get; set; }
    public ContactInfo[] ContactInfo { get; set; }
}

