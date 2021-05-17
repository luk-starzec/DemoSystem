using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueGenerator.Models
{
    public class EventBusSettings
    {
        public const string EventBusSettingsKey = "EventBusSettings";

        public string Connection { get; set; }
        //public string UserName { get; set; }
        //public string Password { get; set; }
        public int RetryCount { get; set; }

        public string SubscriptionClientName { get; set; }
    }
}
