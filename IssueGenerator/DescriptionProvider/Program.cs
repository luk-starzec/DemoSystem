using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Net;

namespace DescriptionProvider
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder( args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder( string[] args)
        {
            var configuration = GetConfiguration();

            return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(options =>
                {
                    var ports = GetDefinedPorts(configuration);
                    options.Listen(IPAddress.Any, ports.httpPort, listenOptions => listenOptions.Protocols = HttpProtocols.Http1AndHttp2);
                    options.Listen(IPAddress.Any, ports.grpcPort, listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
                });
                webBuilder.UseStartup<Startup>();
            });
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        private static (int httpPort, int grpcPort) GetDefinedPorts(IConfiguration configuration)
        {
            var grpcPort = configuration.GetValue("GRPC_PORT", 81);
            var port = configuration.GetValue("PORT", 80);
            return (port, grpcPort);
        }

    }
}
