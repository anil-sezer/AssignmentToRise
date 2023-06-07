namespace Assignment.WorkerService.Report.WorkaroundModels;

public class CommonDataOutput // todo: Remove this workaround and sort the references
{
    public CommonDataOutput(int resultCount)
    {
        ResultCount = resultCount;
    }

    public int ResultCount { get; set; }
}