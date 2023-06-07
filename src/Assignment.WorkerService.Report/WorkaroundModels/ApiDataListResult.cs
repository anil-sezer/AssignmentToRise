namespace Assignment.WorkerService.Report.WorkaroundModels;

public class ApiDataListResult<TData, TList> : ApiResult // todo: Remove this workaround and sort the references
{
    public TData Data { get; set; }

    public List<TList> Items { get; set; }
}