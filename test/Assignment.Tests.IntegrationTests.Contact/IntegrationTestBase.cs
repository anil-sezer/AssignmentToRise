using Microsoft.AspNetCore.Mvc.Testing;

namespace Assignment.Tests.IntegrationTests;

public class IntegrationTestBase
{
    private readonly WebApplicationFactory<Program> _factory;

    public IntegrationTestBase(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        // var configuration = new ConfigurationBuilder()
        //         .AddJsonFile("appsettings.json")
        //         .Build();
    }
}