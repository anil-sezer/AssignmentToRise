namespace Assignment.Web.Core.Models;

public class ApiDataListResult<TData, TList> : ApiResult
{
    public TData Data { get; set; }

    public List<TList> Items { get; set; }
}