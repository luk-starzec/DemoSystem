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

        public static ErrorPriority[] GetErrorPriorities()
        {
            return new ErrorPriority[] {
                new ErrorPriority{
                    Error =   "NullReferenceException",
                    PriorityLevel = EnumPriorityLevel.High,
                },
                new ErrorPriority{
                    Error =   "ArgumentNullException",
                    PriorityLevel = EnumPriorityLevel.High,
                },
                new ErrorPriority{
                    Error =   "OutOfMemoryException",
                    PriorityLevel = EnumPriorityLevel.High,
                },
                new ErrorPriority{
                    Error =   "MissingFieldException",
                    PriorityLevel = EnumPriorityLevel.Low,
                },
                new ErrorPriority{
                    Error =   "MissingMemberException",
                    PriorityLevel = EnumPriorityLevel.Low,
                },
                new ErrorPriority{
                    Error =   "MissingMethodException",
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
