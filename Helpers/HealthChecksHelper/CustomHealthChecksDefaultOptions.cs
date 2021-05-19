namespace HealthChecksHelper
{
    public static class CustomHealthChecksDefaultOptions
    {
        public const string StartupUrl = "/health/startup";
        public const string StartupTag = "startup";

        public const string LivenessUrl = "/health/live";
        public const string LivenessTag = "live";

        public const string ReadinessUrl = "/health/ready";
        public const string ReadinessTag = "ready";

        public const string UIUrl = "/healthz";
    }
}
