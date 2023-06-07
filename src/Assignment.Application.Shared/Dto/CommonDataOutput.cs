namespace Assignment.Application.Shared.Dto;

public class CommonDataOutput
{
    public CommonDataOutput(int resultCount)
    {
        ResultCount = resultCount;
    }

    public int ResultCount { get; set; }
}