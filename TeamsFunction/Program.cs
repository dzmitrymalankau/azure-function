using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model.Service;


var host = new HostBuilder()
    .ConfigureAppConfiguration((config) =>
    {
        config.AddJsonFile("local.settings.json");
    })
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddHealthChecks();
        services.ConfigureCustomServices();
    })
    .Build();

host.Run();
