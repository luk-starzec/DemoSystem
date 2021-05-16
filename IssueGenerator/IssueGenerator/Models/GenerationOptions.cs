using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueGenerator.Models
{
    public class GenerationOptions
    {
        public int Quantity { get; set; }
        public int? WordsLimit { get; set; }
        public int? TextSetId { get; set; }
    }
}
