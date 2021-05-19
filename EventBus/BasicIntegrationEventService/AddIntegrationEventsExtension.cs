using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace BasicIntegrationEventService
{
    public static class AddIntegrationEventsExtension
    {
        public static IServiceCollection AddIntegrationEvents(this IServiceCollection services,
            IEnumerable<Type> eventHandlers = null)
        {
            services.AddTransient<IIntegrationEventService, IntegrationEventService>();

            if (eventHandlers != null)
            {
                foreach (var eventHandler in eventHandlers)
                    services.AddTransient(eventHandler);
            }

            return services;
        }

    }
}
