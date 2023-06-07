using Assignment.Application.Contact;
using Assignment.Application.Shared;
using Assignment.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

// todo: Add Serilog If you can spare the time

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") != null ? 
    configuration.GetConnectionString("DefaultConnection") :
    configuration.GetConnectionString("DefaultConnectionLocal");

builder.Services.AddDbContext<AssignmentDbContext>(options =>
    options
        .UseNpgsql(connectionString)
        .UseSnakeCaseNamingConvention()
        .EnableSensitiveDataLogging() // todo: Only for development
        .EnableDetailedErrors() // todo: Only for development
    );

builder.Services.ConfigureApplicationServicesForContact();

// // todo: This is not ideal in two ways. Improve it
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AssignmentDbContext>();
    dbContext.Database.Migrate();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


// Make the implicit Program class public so test integration test project can access it
public partial class Program { }