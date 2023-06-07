using Assignment.DataAccess.Kafka;
using Assignment.DataAccess.Kafka.Configurations;
using Assignment.Domain.Entities;
using Assignment.Domain.Extensions;
using Assignment.WorkerService.Report.WorkaroundModels;

namespace Assignment.WorkerService.Report;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly HttpClient _httpClient;

    public Worker(ILogger<Worker> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine(await GetContactsAsync());
        using var kafkaConsumerContext = new KafkaConsumerContext<CompileReportCommandTopic>();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            var m = kafkaConsumerContext.GetOneMessage<CompileReportCommandTopic>(stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            Console.WriteLine("Another loop:");
            Console.WriteLine(await GetContactsAsync());
        }
    }
    
    private async Task<object> GetContactsAsync()
    {
        var requestUri = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") != null ? 
            "http://api-contact:5000/Contacts" :
            "http://localhost:5000/Contacts";
        try
        {
            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody.FromStringToObject<ApiDataListResult<CommonDataOutput, Contacts>>();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"Error: {ex.Message}");
            throw;
        }
    }
}