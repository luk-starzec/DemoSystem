using EmployerWebApp.Models;

namespace EmployerWebApp.Helpers
{
    public static class PriorityModelExtensions
    {
        public static PriorityViewModel ToPriorityModel(this TitlePriorityApiModel errorPriority)
        {
            return new PriorityViewModel
            {
                Name = errorPriority.Title,
                PriorityLevel = (EnumPriorityLevel)errorPriority.PriorityLevelId,
            };
        }

        public static TitlePriorityApiModel ToErrorPriorityModel(this PriorityViewModel priority)
        {
            return new TitlePriorityApiModel
            {
                Title = priority.Name,
                PriorityLevelId = (int)priority.PriorityLevel,
            };
        }

        public static PriorityViewModel ToPriorityModel(this AppPriorityApiModel appPriority)
        {
            return new PriorityViewModel
            {
                Name = appPriority.App,
                PriorityLevel = (EnumPriorityLevel)appPriority.PriorityLevelId,
            };
        }

        public static AppPriorityApiModel ToAppPriorityModel(this PriorityViewModel priority)
        {
            return new AppPriorityApiModel
            {
                App = priority.Name,
                PriorityLevelId = (int)priority.PriorityLevel,
            };
        }
    }
}
