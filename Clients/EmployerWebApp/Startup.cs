using EmployerWebApp.Helpers;
using EmployerWebApp.Models;
using EmployerWebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployerWebApp
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
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.Configure<ServiceUrls>(Configuration.GetSection(ServiceUrls.ServiceUrlsKey));
            var serviceUrls = Configuration.GetSection(ServiceUrls.ServiceUrlsKey).Get<ServiceUrls>();

            services.AddHttpClient(HttpClientNames.PrioritySetterClient, c => c.BaseAddress = new Uri(serviceUrls.PrioritySetterUrl));
            services.AddHttpClient(HttpClientNames.HeaderProviderClient, c => c.BaseAddress = new Uri(serviceUrls.HeaderProviderUrl));
            services.AddHttpClient(HttpClientNames.IssueGeneratorClient, c => c.BaseAddress = new Uri(serviceUrls.IssueGeneratorUrl));
            services.AddHttpClient(HttpClientNames.ReportServerClient, c => c.BaseAddress = new Uri(serviceUrls.ReportServiceUrl));

            services.AddTransient<IIssueService, IssueService>();
            services.AddTransient<IDescriptionService, DescriptionService>();
            services.AddTransient<IPriorityService, PriorityService>();
            services.AddTransient<IReportService, ReportService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Compose"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
