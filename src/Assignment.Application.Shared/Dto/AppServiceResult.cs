using System.Net;

namespace Assignment.Application.Shared.Dto;

public class AppServiceResult
{
    public AppServiceResult()
    {

    }

    public AppServiceResult(bool succeed)
    {
        Succeed = succeed;
    }

    public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;
    public string ErrorMessage { get; set; }
    public bool Succeed { get; set; }
}