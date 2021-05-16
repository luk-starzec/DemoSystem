using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueGenerator.Models
{
    public class IssueModel
    {
        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public string App { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Sender { get; set; }
    }
}
