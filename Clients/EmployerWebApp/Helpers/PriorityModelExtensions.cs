using EmployerWebApp.Models;

namespace EmployerWebApp.Helpers
{
    public static class PriorityModelExtensions
    {
        public static PriorityModel ToPriorityModel(this TitlePriorityModel errorPriority)
        {
            return new PriorityModel
            {
                Name = errorPriority.Title,
                Priority = (EnumPriority)errorPriority.PriorityLevelId,
            };
        }

        public static TitlePriorityModel ToErrorPriorityModel(this PriorityModel priority)
        {
            return new TitlePriorityModel
            {
                Title = priority.Name,
                PriorityLevelId = (int)priority.Priority,
            };
        }

        public static PriorityModel ToPriorityModel(this AppPriorityModel appPriority)
        {
            return new PriorityModel
            {
                Name = appPriority.App,
                Priority = (EnumPriority)appPriority.PriorityLevelId,
            };
        }

        public static AppPriorityModel ToAppPriorityModel(this PriorityModel priority)
        {
            return new AppPriorityModel
            {
                App = priority.Name,
                PriorityLevelId = (int)priority.Priority,
            };
        }
    }
}
