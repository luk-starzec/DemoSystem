namespace PrioritySetter.Data
{
    public static class InitialData
    {
        public static Priority[] GetPriorities()
        {
            return new Priority[]
            {
                new Priority
                {
                    PriorityLevel = EnumPriorityLevel.Normal,
                    Name = EnumPriorityLevel.Normal.ToString(),
                    Description = "Do it",
                },
                new Priority
                {
                    PriorityLevel = EnumPriorityLevel.High,
                    Name = EnumPriorityLevel.High.ToString(),
                    Description = "Do it now!",
                },
                new Priority
                {
                    PriorityLevel = EnumPriorityLevel.Low,
                    Name = EnumPriorityLevel.Low.ToString(),
                    Description = "Maybe later",
                },
            };
        }

        public static TitlePriority[] GetTitlePriorities()
        {
            return new TitlePriority[] {
                new TitlePriority{
                    Title = "NullReferenceException",
                    PriorityLevel = EnumPriorityLevel.High,
                },
                new TitlePriority{
                    Title = "ArgumentNullException",
                    PriorityLevel = EnumPriorityLevel.High,
                },
                new TitlePriority{
                    Title = "OutOfMemoryException",
                    PriorityLevel = EnumPriorityLevel.High,
                },
                new TitlePriority{
                    Title = "MissingFieldException",
                    PriorityLevel = EnumPriorityLevel.Low,
                },
                new TitlePriority{
                    Title = "MissingMemberException",
                    PriorityLevel = EnumPriorityLevel.Low,
                },
                new TitlePriority{
                    Title = "MissingMethodException",
                    PriorityLevel = EnumPriorityLevel.Low,
                },
            };
        }

        public static AppPriority[] GetAppPriorities()
        {
            return new AppPriority[]
            {
                new AppPriority
                {
                    App = "Glob",
                    PriorityLevel=EnumPriorityLevel.Low,
                },
            };
        }
    }
}
