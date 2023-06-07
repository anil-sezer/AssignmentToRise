using Microsoft.AspNetCore.Mvc.Testing;

namespace Assignment.Test.Integration.Report;

public class IntegrationTestBaseForReport
{
    private readonly WebApplicationFactory<Program> _factory;

    public IntegrationTestBaseForReport(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        // var configuration = new ConfigurationBuilder()
        //         .AddJsonFile("appsettings.json")
        //         .Build();
    }
}