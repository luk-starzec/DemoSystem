using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueGenerator.Models
{
    public class ServicesSettings
    {
        public const string ServicesSettingsKey = "ServicesSettings";

        public string HeaderProviderUrl { get; set; }
        public string DescriptionProviderUrl { get; set; }
        public string SenderProviderUrl { get; set; }
    }
}
