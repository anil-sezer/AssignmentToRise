using System.ComponentModel.DataAnnotations;
using Assignment.Domain;

namespace Assignment.Application.Contact.Contact.Dto;

public class AddContactInput
{
    [Required(ErrorMessage = "{0} is required!")]
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Company { get; set; }
    public ContactInfo[] ContactInfo { get; set; }
}