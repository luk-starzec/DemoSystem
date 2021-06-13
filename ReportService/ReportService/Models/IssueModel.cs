using System;
using System.Collections.Generic;

namespace ReportService.Models
{
    public class IssueModel
    {
        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public string App { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Sender { get; set; }
        public string Priority { get; set; }
        public string Employee { get; set; }
        public List<EventLogModel> Logs { get; set; } = new List<EventLogModel>();
    }
}
