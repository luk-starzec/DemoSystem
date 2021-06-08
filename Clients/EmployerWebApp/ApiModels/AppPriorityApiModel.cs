using EmployerWebApp.ViewModels;

namespace EmployerWebApp.ApiModels
{
    public class AppPriorityApiModel : IApiModel
    {
        public string App { get; set; }
        public int PriorityLevelId { get; set; }

        public AppPriorityApiModel()
        { }

        public AppPriorityApiModel(PriorityViewModel viewModel)
        {
            App = viewModel.Name;
            PriorityLevelId = (int)viewModel.PriorityLevel;
        }

        public PriorityViewModel ToViewModel()
        {
            return new PriorityViewModel
            {
                Name = App,
                PriorityLevel = (EnumPriorityLevel)PriorityLevelId,
            };
        }
    }
}
