using PrioritySetter.Data;
using PrioritySetter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrioritySetter.Helpers
{
    public static class ModelExtensions
    {
        public static TitlePriorityModel ToModel(this TitlePriority entity)
        {
            return new TitlePriorityModel
            {
                Title = entity.Title,
                PriorityLevelId = (int)entity.PriorityLevel,
            };
        }

        public static TitlePriority ToEntity(this TitlePriorityModel model)
        {
            return new TitlePriority
            {
                Title = model.Title,
                PriorityLevel = (EnumPriorityLevel)model.PriorityLevelId,
            };
        }


        public static AppPriorityModel ToModel(this AppPriority entity)
        {
            return new AppPriorityModel
            {
                App = entity.App,
                PriorityLevelId = (int)entity.PriorityLevel,
            };
        }

        public static AppPriority ToEntity(this AppPriorityModel model)
        {
            return new AppPriority
            {
                App = model.App,
                PriorityLevel = (EnumPriorityLevel)model.PriorityLevelId,
            };
        }

    }
}
