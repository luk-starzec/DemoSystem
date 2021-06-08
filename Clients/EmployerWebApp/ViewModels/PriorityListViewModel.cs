using System.Collections.Generic;

namespace EmployerWebApp.ViewModels
{
    public class PriorityListViewModel
    {
        public PriorityGroupViewModel Group { get; set; }
        public List<PriorityViewModel> Items { get; set; }
    }
}
