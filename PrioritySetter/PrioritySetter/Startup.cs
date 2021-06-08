using BasicIntegrationEventService;
using EnvironmentHelper;
using EventBus;
using EventBusRabbitMQ;
using HealthChecksHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PrioritySetter.Data;
using PrioritySetter.IntegrationEvents;
using PrioritySetter.IntegrationEvents.EventHandlers;
using PrioritySetter.IntegrationEvents.Events;
using PrioritySetter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TracingHelper;

namespace PrioritySetter
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
            services.AddDbContext<PrioritySetterContext>(options =>
            {
                options.UseSqlite("Data Source = prioritySetterDb.db");
            });

            services.AddTransient<IPriorityService, PriorityService>();

            var eventBusSettings = Configuration.GetSection(EventBusRabbitMQSettings.EventBusSettingsKey).Get<EventBusRabbitMQSettings>();
            services.AddEventBus(eventBusSettings);

            services.AddIntegrationEvents(new Type[]
            {
                typeof(IssueCreatedIntegrationEventHandler)
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PrioritySetter", Version = "v1" });
            });

            services.AddCustomHealthChecks(Configuration);

            services.AddZipkinTracing(typeof(Startup).Assembly.GetName().Name);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsDockerCompose())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PrioritySetter v1"));
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
            eventBus.Subscribe<IssueCreatedIntegrationEvent, IssueCreatedIntegrationEventHandler>();
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
