using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployerWebApp.Models
{
    public class ServicesSettings
    {
        public const string ServicesSettingsKey = "ServicesSettings";
        public string IssueGeneratorUrl { get; set; }
        public string DescriptionProviderUrl { get; set; }
        public string ReportServiceUrl { get; set; }
    }
}
