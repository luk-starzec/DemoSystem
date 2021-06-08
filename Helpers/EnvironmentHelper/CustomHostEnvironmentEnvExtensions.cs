using Microsoft.Extensions.Hosting;

namespace EnvironmentHelper
{
    public static class CustomHostEnvironmentEnvExtensions
    {
        public static bool IsDockerCompose(this IHostEnvironment hostEnvironment)
        {
            return hostEnvironment.IsEnvironment("Compose");
        }
    }
}
