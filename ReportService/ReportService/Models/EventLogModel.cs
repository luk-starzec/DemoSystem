using System;

namespace ReportService.Models
{
    public class EventLogModel
    {
        public DateTimeOffset Date { get; set; }
        public string Description { get; set; }
    }
}
