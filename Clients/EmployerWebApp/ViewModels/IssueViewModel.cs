using System;
using System.Linq;

namespace EmployerWebApp.ViewModels
{
    public class IssueViewModel
    {
        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public string App { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Sender { get; set; }
        public string Priority { get; set; }
        public string Employee { get; set; }
        public IssueLogViewModel[] Logs { get; set; }

        public bool DetailsVisible { get; set; }

        public IssueLogViewModel[] SortedLogs => Logs.OrderByDescending(r => r.Date).ToArray();

        public EnumPriorityLevel PriorityLevel => Priority is null ? EnumPriorityLevel.None : (EnumPriorityLevel)Enum.Parse(typeof(EnumPriorityLevel), Priority);
    }
}
