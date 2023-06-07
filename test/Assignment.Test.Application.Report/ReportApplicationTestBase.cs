using Assignment.DataAccess.PostgreSQL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.Test.Application.Report;

public class ReportApplicationTestBase
{
    protected readonly ReportDbContext DefaultTestDbContext;

    public ReportApplicationTestBase()
    {
        DefaultTestDbContext = GetDefaultTestDbContext();
    }

    /// <summary>
    /// This method can be used for new scoped (same database) of default dbContext
    /// </summary>
    /// <returns>New scoped instance (same database) of default dbContext</returns>
    protected ReportDbContext GetDefaultTestDbContext()
    {
        var provider = GetNewHostServiceProvider().CreateScope().ServiceProvider;

        return provider.GetRequiredService<ReportDbContext>();
    }

    /// <summary>
    /// This method can be used for create a new dbContext (different database) with different name in run-time.
    /// It is useful when you want to work on an empty database.
    /// </summary>
    /// <param name="dbContextName"></param>
    /// <returns>Creates new dbContext (new database) with different name</returns>
    protected ReportDbContext GetNewTestDbContext(string dbContextName)
    {
        var provider = GetNewHostServiceProvider().CreateScope().ServiceProvider;

        var dbContextOptionBuilder = new DbContextOptionsBuilder<ReportDbContext>();
        dbContextOptionBuilder.UseInMemoryDatabase(dbContextName)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();

        return new ReportDbContext(dbContextOptionBuilder.Options);
    }

    protected IServiceProvider GetNewHostServiceProvider()
    {
        return GetTestServer().Services;
    }

    protected TestServer GetTestServer()
    {
        return new TestServer(
            new WebHostBuilder()
                .UseStartup<ReportApplicationTestStartup>()
                .UseEnvironment("Test")
        );
    }
}
