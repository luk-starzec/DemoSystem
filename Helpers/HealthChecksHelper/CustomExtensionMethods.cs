using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System;

namespace HealthChecksHelper
{
    public static class CustomExtensionMethods
    {
        public static IApplicationBuilder UseCustomHealthChecks(this IApplicationBuilder app, CustomHealthChecksOptions options = null)
        {
            if (options is null)
                options = new CustomHealthChecksOptions();

            if (options.UseStartup)
                app.UseHealthChecks(options.StartupUrl, new HealthCheckOptions
                {
                    Predicate = (check) => check.Tags.Contains(options.StartupTag)
                });

            if (options.UseLiveness)
                app.UseHealthChecks(options.LivenessUrl, new HealthCheckOptions
                {
                    Predicate = (check) => check.Tags.Contains(options.LivenessTag)
                });

            if (options.UseReadiness)
                app.UseHealthChecks(options.ReadinessUrl, new HealthCheckOptions
                {
                    Predicate = (check) => check.Tags.Contains(options.ReadinessTag)
                });

            if (options.UseUI)
                app.UseHealthChecks(options.UIUrl, new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            return app;
        }
    }
}
