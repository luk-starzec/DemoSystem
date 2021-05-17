using Autofac;
using EventBus;
using EventBusRabbitMQ;
using HealthChecks.UI.Client;
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
            services.Configure<EventBusSettings>(Configuration.GetSection(EventBusSettings.EventBusSettingsKey));

            services.AddTransient<IHeaderService, HeaderService>();
            services.AddTransient<IDescriptionService, DescriptionService>();
            services.AddTransient<ISenderService, SenderService>();
            services.AddTransient<IIssueService, IssueService>();

            services.AddHttpClient<HeaderService>();
            services.AddHttpClient<SenderService>();

            services.AddTransient<IIntegrationEventService, IntegrationEventService>();
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<EventBusSettings>>().Value;

                var factory = new ConnectionFactory()
                {
                    Uri = new Uri(settings.Connection),
                    //UserName = settings.UserName,
                    //Password = settings.Password,
                    DispatchConsumersAsync = true
                };

                var retryCount = settings.RetryCount > 0 ? settings.RetryCount : 5;

                return new RabbitMQPersistentConnection(factory, retryCount);
            });

            services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var settings = sp.GetRequiredService<IOptions<EventBusSettings>>().Value;
                var retryCount = settings.RetryCount > 0 ? settings.RetryCount : 5;

                return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, eventBusSubcriptionsManager, sp, settings.SubscriptionClientName, retryCount);
            });
            services.AddSingleton<IEventBusSubscriptionsManager, EventBusSubscriptionsManager>();

            services.AddTransient<IssueCreatedIntegrationEventHandler>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IssueGenerator", Version = "v1" });
            });

            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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

            app.UseHealthChecks("/health");
            app.UseHealthChecks("/healthz", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<IssueCreatedIntegrationEvent, IssueCreatedIntegrationEventHandler>();
        }

    }
}
