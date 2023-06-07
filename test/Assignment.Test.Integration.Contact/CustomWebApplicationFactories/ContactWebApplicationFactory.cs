using Assignment.DataAccess.PostgreSQL;
using Assignment.Test.Integration.Contact.Contact.DataBuilder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Assignment.Test.Integration.Contact.CustomWebApplicationFactories;

public class ContactWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram: class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<ContactDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ContactDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForContactServiceTesting").ConfigureWarnings(x=>x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                options.EnableSensitiveDataLogging();
            });

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ContactDbContext>();
                var logger = scopedServices.GetRequiredService<ILogger<ContactWebApplicationFactory<TProgram>>>();

                db.Database.EnsureCreated();

                try
                {
                    new ContactTestsDataBuilder(db).SeedData();
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the " +
                                        "database with test messages. Error: {Message}", ex.Message);
                }
            }
        });
    }
}