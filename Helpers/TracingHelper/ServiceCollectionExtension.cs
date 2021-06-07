using Microsoft.Extensions.DependencyInjection;
using System;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace TracingHelper
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddZipkinTracing(this IServiceCollection services, string serviceName)
        {
            services.AddOpenTelemetryTracing(builder =>
            {
                builder
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
                    .AddAspNetCoreInstrumentation(options
                    => options.Filter = (httpContext) =>
                    {
                        return !httpContext.Request.Path.Value.StartsWith("/health");
                    })
                    .SetSampler(new AlwaysOnSampler())
                    .AddZipkinExporter(options =>
                    {
                        var zipkinHostName = Environment.GetEnvironmentVariable("ZIPKIN_HOSTNAME") ?? "localhost";
                        options.Endpoint = new Uri($"http://{zipkinHostName}:9411/api/v2/spans");
                    });
            });

            return services;
        }
    }
}
