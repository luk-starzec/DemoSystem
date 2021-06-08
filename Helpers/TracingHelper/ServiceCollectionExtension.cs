using Microsoft.Extensions.DependencyInjection;
using System;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Collections.Generic;
using System.Linq;

namespace TracingHelper
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddZipkinTracing(this IServiceCollection services, string serviceName, List<string> ignoredPaths = null)
        {
            services.AddOpenTelemetryTracing(builder =>
            {
                builder
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
                    .AddAspNetCoreInstrumentation(options
                    => options.Filter = (httpContext) =>
                    {
                        var paths = ignoredPaths ?? defaultIgnoredPaths;
                        var ignore = paths.Any(r => httpContext.Request.Path.Value.StartsWith(r));
                        return !ignore;
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

        private static List<string> defaultIgnoredPaths = new()
        {
            "/health",
            "/swagger"
        };
    }
}
