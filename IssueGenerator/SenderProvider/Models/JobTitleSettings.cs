using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenderProvider.Models
{
    public class JobTitleSettings
    {
        public const string JobTitleSettingsKey = "JobTitleSettings";

        public string FirstPartApiUrl { get; set; }
        public string SecondPartApiUrl { get; set; }
        public string ThirdPartApiUrl { get; set; }

        public string FirstPartDefaultValue { get; set; }
        public string SecondPartDefaultValue { get; set; }
        public string ThirdPartDefaultValue { get; set; }
    }
}
