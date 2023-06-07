using System.ComponentModel.DataAnnotations;
using Assignment.Domain;

namespace Assignment.Application.Contact.Contact.Dto;

public class RemoveContactConnectionInput
{
    [Required(ErrorMessage = "{0} is required!")]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "{0} is required!")]
    public ContactInfo ContactInfo { get; set; }
}
