using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrioritySetter.Data
{
    public class Priority
    {
        public EnumPriorityLevel PriorityLevel { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
