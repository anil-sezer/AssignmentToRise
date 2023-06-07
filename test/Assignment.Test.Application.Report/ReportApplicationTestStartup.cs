using Assignment.Application.Report;
using Assignment.DataAccess.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.Test.Application.Report;

public class ReportApplicationTestStartup
{
    public ReportApplicationTestStartup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
        
    public IConfiguration Configuration { get; }
        
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ReportDbContext>(options =>
        {
            options.UseInMemoryDatabase(nameof(ReportDbContext) + "Test")
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .ConfigureWarnings(x=>x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        });
        
        services.ConfigureApplicationServicesForReport();
    }

    public void Configure()
    {
    }
}