using Assignment.Application.Report;
using Assignment.DataAccess;
using Assignment.Web.Core.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.ConfigureApplicationServicesForReport();

builder.DoGenericSteps();

var connectionString = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") != null ? 
    configuration.GetConnectionString("DefaultConnection") :
    configuration.GetConnectionString("DefaultConnectionLocal");

builder.Services.AddDbContext<ReportDbContext>(options =>
        options
            .UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging() // todo: Only for development
            .EnableDetailedErrors() // todo: Only for development
);
        
// // todo: This is not ideal in two ways. Improve it
using var scope = builder.Services.BuildServiceProvider().CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ReportDbContext>();
dbContext.Database.Migrate();

var app = builder.Build();
app.RunTheApp();

// Make the implicit Program class public so test integration test project can access it
public partial class Program { }