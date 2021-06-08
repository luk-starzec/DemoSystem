using System;
using System.ComponentModel.DataAnnotations;

namespace EmployerWebApp.ViewModels
{
    public class IssueGenerationViewModel
    {
        [Range(0, 100)]
        public int IssuesCount { get; set; }
        public bool LimitWordsCount { get; set; }
        [Range(0, 1000)]
        public int? MaxWordsCount { get; set; }
        public bool RandomizeWordsCount { get; set; }
        public int TextSourceId { get; set; }
    }
}
