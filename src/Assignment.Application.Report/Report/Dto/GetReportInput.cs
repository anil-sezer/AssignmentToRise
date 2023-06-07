using System.ComponentModel.DataAnnotations;

namespace Assignment.Application.Report.Report.Dto;

public class GetReportInput
{
    [Required(ErrorMessage = "{0} is required!")]
    public Guid Id { get; set; }
}
