namespace EmployeeConsoleApp
{
    public class ApplicationSettings
    {
        public const string ApplicationSettingsKey = "ApplicationSettings";
        public string UserName { get; set; }
        public int WordsPerMinute { get; set; }
        public int DefaultWordsPerMinute { get; } = 10;
    }
}
