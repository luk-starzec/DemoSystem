using EmployerWebApp.ViewModels;

namespace EmployerWebApp.ApiModels
{
    public class TitlePriorityApiModel : IApiModel
    {
        public string Title { get; set; }
        public int PriorityLevelId { get; set; }

        public TitlePriorityApiModel()
        { }

        public TitlePriorityApiModel(PriorityViewModel viewModel)
        {
            Title = viewModel.Name;
            PriorityLevelId = (int)viewModel.PriorityLevel;
        }

        public PriorityViewModel ToViewModel()
        {
            return new PriorityViewModel
            {
                Name = Title,
                PriorityLevel = (EnumPriorityLevel)PriorityLevelId,
            };
        }
    }
}
