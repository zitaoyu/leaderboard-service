using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Cosmos;
using LeaderboardServiceFunction.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        // Add CosmosDB client
        services.AddSingleton((s) =>
        {
            var cosmosDbConnectionString = System.Environment.GetEnvironmentVariable("CosmosDbConnectionSetting");
            return new CosmosClient(cosmosDbConnectionString);
        });
        services.AddScoped<ILeaderboardService, LeaderboardService>();
    })
    .Build();

host.Run();
