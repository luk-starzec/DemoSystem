using EventBusRabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicIntegrationEventService;
using ReportService.IntegrationEvents.EventHandlers;
using EventBus;
using ReportService.IntegrationEvents.Events;

namespace ReportService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var eventBusSettings = Configuration.GetSection(EventBusRabbitMQSettings.EventBusSettingsKey).Get<EventBusRabbitMQSettings>();
            services.AddEventBus(eventBusSettings);

            services.AddIntegrationEvents(new Type[]
            {
                typeof(IssueCreatedIntegrationEventHandler),
                typeof(IssuePrioritySetIntegrationEventHandler),
                typeof(IssueCompletedIntegrationEventHandler),
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReportService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReportService v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<IssueCreatedIntegrationEvent, IssueCreatedIntegrationEventHandler>();
            eventBus.Subscribe<IssuePrioritySetIntegrationEvent, IssuePrioritySetIntegrationEventHandler>();
            eventBus.Subscribe<IssueCompletedIntegrationEvent, IssueCompletedIntegrationEventHandler>();
        }

    }
}
