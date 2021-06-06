using BasicIntegrationEventService;
using EventBus;
using EventBusRabbitMQ;
using Google.Protobuf.WellKnownTypes;
using HealthChecks.UI.Client;
using HealthChecksHelper;
using IssueGenerator.IntegrationEvents;
using IssueGenerator.IntegrationEvents.EventHandlers;
using IssueGenerator.IntegrationEvents.Events;
using IssueGenerator.Models;
using IssueGenerator.Services;
using IssueGenerator.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueGenerator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ServicesSettings>(Configuration.GetSection(ServicesSettings.ServicesSettingsKey));
            services.Configure<EventBusRabbitMQSettings>(Configuration.GetSection(EventBusRabbitMQSettings.EventBusSettingsKey));

            services.AddTransient<IHeaderService, HeaderService>();
            services.AddTransient<IDescriptionService, DescriptionService>();
            services.AddTransient<ISenderService, SenderService>();
            services.AddTransient<IIssueService, IssueService>();

            services.AddHttpClient<HeaderService>();
            services.AddHttpClient<SenderService>();

            var eventBusSettings = Configuration.GetSection(EventBusRabbitMQSettings.EventBusSettingsKey).Get<EventBusRabbitMQSettings>();
            services.AddEventBus(eventBusSettings);

            services.AddIntegrationEvents(new System.Type[]
            {
                typeof(IssueCreatedIntegrationEventHandler)
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IssueGenerator", Version = "v1" });
            });

            services.AddCustomHealthChecks(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Compose"))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IssueGenerator v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCustomHealthChecks();

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            //eventBus.Subscribe<IssueCreatedIntegrationEvent, IssueCreatedIntegrationEventHandler>();
        }

    }

    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy(),
                tags: new string[]
                {
                    CustomHealthChecksDefaultOptions.StartupTag,
                    CustomHealthChecksDefaultOptions.LivenessTag,
                    CustomHealthChecksDefaultOptions.ReadinessTag,
                });

            var connection =
                configuration[$"{EventBusRabbitMQSettings.EventBusSettingsKey}:{nameof(EventBusRabbitMQSettings.Connection)}"];
            hcBuilder
                .AddRabbitMQ(connection, name: "rabbitmq-check",
                    tags: new string[] {
                        CustomHealthChecksDefaultOptions.StartupTag,
                        CustomHealthChecksDefaultOptions.ReadinessTag,
                    });

            return services;
        }
    }
}
