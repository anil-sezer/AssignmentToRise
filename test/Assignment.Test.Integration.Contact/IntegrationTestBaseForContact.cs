using Microsoft.AspNetCore.Mvc.Testing;

namespace Assignment.Test.Integration.Contact;

public class IntegrationTestBaseForContact
{
    private readonly WebApplicationFactory<Program> _factory;

    public IntegrationTestBaseForContact(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        // var configuration = new ConfigurationBuilder()
        //         .AddJsonFile("appsettings.json")
        //         .Build();
    }
}