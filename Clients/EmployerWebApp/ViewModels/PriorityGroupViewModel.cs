using System;
using System.Collections.Generic;

namespace EmployerWebApp.ViewModels
{
    public class PriorityGroupViewModel
    {
        public string Header { get; set; }
        public List<string> Keys { get; set; }
        public string ApiPath { get; set; }
        public Type ApiModelType { get; set; }
    }
}
