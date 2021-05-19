using EventBus;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;

namespace EventBusRabbitMQ
{
    public static class AddEventBusExtension
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, EventBusRabbitMQSettings settings)
        {
            services.AddSingleton<IEventBusSubscriptionsManager, EventBusSubscriptionsManager>();

            var retryCount = settings.RetryCount > 0 ? settings.RetryCount : 5;

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    Uri = new Uri(settings.Connection),
                    DispatchConsumersAsync = true
                };
                return new RabbitMQPersistentConnection(factory, retryCount);
            });

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, eventBusSubcriptionsManager, serviceScopeFactory, settings.SubscriptionClientName, retryCount);
            });

            return services;
        }
    }
}
