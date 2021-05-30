namespace EmployeeConsoleApp
{
    public class ApplicationSettings
    {
        public const string ApplicationSettingsKey = "ApplicationSettings";
        public string UserName { get; set; }
        public int WordsPerSecond { get; set; }
        public int DefaultWordsPerSecond { get; } = 10;
    }
}
