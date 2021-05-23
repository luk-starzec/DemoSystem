using BasicIntegrationEventService;
using EmployeeConsoleApp.IntegrationEvents.EventHandlers;
using EmployeeConsoleApp.IntegrationEvents.Events;
using EventBus;
using EventBusRabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace EmployeeConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting up");

                using var host = CreateHostBuilder(args).Build();

                var eventBus = host.Services.GetRequiredService<IEventBus>();
                eventBus.Subscribe<IssuePrioritySetIntegrationEvent, IssuePrioritySetIntegrationEventHandler>();

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }


        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration((config) => config.AddEnvironmentVariables())
                .UseSerilog()
                .ConfigureServices((hostContext, services) =>
                {
                    var eventBusSettings = hostContext.Configuration.GetSection(EventBusRabbitMQSettings.EventBusSettingsKey).Get<EventBusRabbitMQSettings>();

                    services.AddEventBus(eventBusSettings)
                            .AddIntegrationEvents(new Type[]
                            {
                                typeof(IssuePrioritySetIntegrationEventHandler)
                            })
                            .Configure<ApplicationSettings>(hostContext.Configuration.GetSection(ApplicationSettings.ApplicationSettingsKey))
                            .AddTransient<IIssueService, IssueService>();
                });
    }
}
