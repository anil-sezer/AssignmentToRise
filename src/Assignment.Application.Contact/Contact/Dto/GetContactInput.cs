using System.ComponentModel.DataAnnotations;

namespace Assignment.Application.Contact.Contact.Dto;

public class GetContactInput
{
    [Required(ErrorMessage = "{0} is required!")]
    public Guid Id { get; set; }
}
