using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrioritySetter.Data
{
    public class TitlePriority
    {
        public string Title { get; set; }
        public EnumPriorityLevel PriorityLevel { get; set; }
        public Priority PriorityRelation { get; set; }
    }
}
