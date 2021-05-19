namespace HealthChecksHelper
{
    public class CustomHealthChecksOptions
    {
        public bool UseStartup { get; set; } = true;
        public string StartupUrl { get; set; } = CustomHealthChecksDefaultOptions.StartupUrl;
        public string StartupTag { get; set; } = CustomHealthChecksDefaultOptions.StartupTag;

        public bool UseLiveness { get; set; } = true;
        public string LivenessUrl { get; set; } = CustomHealthChecksDefaultOptions.LivenessUrl;
        public string LivenessTag { get; set; } = CustomHealthChecksDefaultOptions.LivenessTag;

        public bool UseReadiness { get; set; } = true;
        public string ReadinessUrl { get; set; } = CustomHealthChecksDefaultOptions.ReadinessUrl;
        public string ReadinessTag { get; set; } = CustomHealthChecksDefaultOptions.ReadinessTag;

        public bool UseUI { get; set; } = true;
        public string UIUrl { get; set; } = CustomHealthChecksDefaultOptions.UIUrl;
    }
}
