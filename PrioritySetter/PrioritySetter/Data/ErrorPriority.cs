using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrioritySetter.Data
{
    public class ErrorPriority
    {
        public string Error { get; set; }
        public EnumPriorityLevel PriorityLevel { get; set; }
        public Priority PriorityRelation { get; set; }
    }
}
