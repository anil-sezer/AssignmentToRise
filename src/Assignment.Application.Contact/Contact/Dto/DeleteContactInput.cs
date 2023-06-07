using System.ComponentModel.DataAnnotations;

namespace Assignment.Application.Contact.Contact.Dto;

public class DeleteContactInput
{
    [Required(ErrorMessage = "{0} is required!")]
    public Guid Id { get; set; }
}
