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
        public static ErrorPriorityModel ToModel(this ErrorPriority entity)
        {
            return new ErrorPriorityModel
            {
                Error = entity.Error,
                PriorityLevelId = (int)entity.PriorityLevel,
            };
        }

        public static ErrorPriority ToEntity(this ErrorPriorityModel model)
        {
            return new ErrorPriority
            {
                Error = model.Error,
                PriorityLevel = (EnumPriorityLevel)model.PriorityLevelId,
            };
        }

    }
}
