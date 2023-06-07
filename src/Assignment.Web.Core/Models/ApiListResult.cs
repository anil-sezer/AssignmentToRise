namespace Assignment.Web.Core.Models;

public class ApiListResult<TList> : ApiResult
{
    public List<TList> Items { get; set; }
}