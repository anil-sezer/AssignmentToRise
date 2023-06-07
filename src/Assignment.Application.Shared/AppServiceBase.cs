using System.Net;
using Assignment.Application.Shared.Dto;

namespace Assignment.Application.Shared;

public class AppServiceBase : IAppServiceBase
{
    protected AppServiceResult Success()
    {
        return new AppServiceResult
        {
            ErrorMessage = null,
            Succeed = true
        };
    }

    protected AppServiceResult Error(string errorMessage, HttpStatusCode httpStatusCode)
    {
        return new AppServiceResult
        {
            ErrorMessage = errorMessage,
            HttpStatusCode = httpStatusCode,
            Succeed = false
        };
    }

    protected AppServiceDataResult<TOutput> DataSuccess<TOutput>(TOutput output) where TOutput : class
    {
        return new AppServiceDataResult<TOutput>
        {
            Data = output,
            Succeed = true,
            ErrorMessage = null
        };
    }

    protected AppServiceDataResult<TOutput> DataError<TOutput>(string errorMessage, HttpStatusCode httpStatusCode) where TOutput : class
    {
        return new AppServiceDataResult<TOutput>
        {
            Data = null,
            Succeed = false,
            ErrorMessage = errorMessage,
            HttpStatusCode = httpStatusCode
        };
    }

    protected AppServiceListResult<TListOutput> ListSuccess<TListOutput>(List<TListOutput> output) where TListOutput : class
    {
        return new AppServiceListResult<TListOutput>
        {
            Items = output,
            Succeed = true,
            ErrorMessage = null
        };
    }

    protected AppServiceListResult<TListOutput> ListError<TListOutput>(string errorMessage, HttpStatusCode httpStatusCode) where TListOutput : class
    {
        return new AppServiceListResult<TListOutput>
        {
            Items = null,
            Succeed = false,
            ErrorMessage = errorMessage,
            HttpStatusCode = httpStatusCode
        };
    }

    protected AppServiceDataListResult<TDataOutput, TListOutput> DataListSuccess<TDataOutput, TListOutput>(TDataOutput dataOutput, List<TListOutput> listOutput) where TListOutput : class where TDataOutput : class
    {
        return new AppServiceDataListResult<TDataOutput, TListOutput>
        {
            Items = listOutput,
            Data = dataOutput,
            Succeed = true,
            ErrorMessage = null
        };
    }

    protected AppServiceDataListResult<TDataOutput, TListOutput> DataListError<TDataOutput, TListOutput>(string errorMessage, HttpStatusCode httpStatusCode) where TListOutput : class where TDataOutput : class
    {
        return new AppServiceDataListResult<TDataOutput, TListOutput>
        {
            Items = null,
            Data = null,
            Succeed = false,
            ErrorMessage = errorMessage,
            HttpStatusCode = httpStatusCode
        };
    }
}