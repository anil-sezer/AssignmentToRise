using Assignment.Application.Contact;
using Assignment.Application.Report;
using Assignment.DataAccess.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.Test.Shared;

public class TestStartup
{
    public TestStartup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
        
    public IConfiguration Configuration { get; }
        
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ContactDbContext>(options =>
        {
            options.UseInMemoryDatabase("AssignmentDbContextTest")
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .ConfigureWarnings(x=>x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        });
        
        services.ConfigureApplicationServicesForContact();
        services.ConfigureApplicationServicesForReport();
    }

    public void Configure()
    {

    }
}