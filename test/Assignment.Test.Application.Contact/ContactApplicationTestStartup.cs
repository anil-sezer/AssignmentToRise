using Assignment.Application.Contact;
using Assignment.DataAccess.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.Test.Application.Contact;

public class ContactApplicationTestStartup
{
    public ContactApplicationTestStartup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
        
    public IConfiguration Configuration { get; }
        
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ContactDbContext>(options =>
        {
            options.UseInMemoryDatabase(nameof(ContactDbContext) + "Test")
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .ConfigureWarnings(x=>x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        });
        
        services.ConfigureApplicationServicesForContact();
    }

    public void Configure()
    {
    }
}