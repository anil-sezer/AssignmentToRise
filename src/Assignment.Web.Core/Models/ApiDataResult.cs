namespace Assignment.Web.Core.Models;

public class ApiDataResult<TData> : ApiResult
{
    public TData Data { get; set; }
}