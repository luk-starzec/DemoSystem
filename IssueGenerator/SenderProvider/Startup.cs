using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SenderProvider.Models;
using SenderProvider.Repo;
using SenderProvider.Services;
using SenderProvider.Services.Interfaces;
using TracingHelper;

namespace SenderProvider
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
            services.AddSingleton<INameRepo, NameRepo>();
            services.AddTransient<INameService, NameService>();
            services.AddTransient<IJobTitleFirstPartService, JobTitleFirstPartService>();
            services.AddTransient<IJobTitleSecondPartService, JobTitleSecondPartService>();
            services.AddTransient<IJobTitleThirdPartService, JobTitleThirdPartService>();
            services.AddTransient<ISenderService, SenderService>();

            services.AddHttpClient<JobTitleFirstPartService>();
            services.AddHttpClient<JobTitleSecondPartService>();
            services.AddHttpClient<JobTitleThirdPartService>();

            services.Configure<JobTitleSettings>(Configuration.GetSection(JobTitleSettings.JobTitleSettingsKey));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SenderProvider", Version = "v1" });
            });

            services.AddHealthChecks();

            services.AddZipkinTracing(typeof(Startup).Assembly.GetName().Name);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SenderProvider v1"));
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
        }
    }
}
