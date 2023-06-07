namespace Assignment.Application.Shared.Dto;

public class AppServiceDataListResult<TDataOutput, TListOutput> : AppServiceResult where TDataOutput : class where TListOutput : class
{
    public TDataOutput Data { get; set; }

    public List<TListOutput> Items { get; set; }

    public AppServiceDataListResult()
    {
            
    }
}