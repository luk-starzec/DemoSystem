using EmployerWebApp.ViewModels;

namespace EmployerWebApp.ApiModels
{
    public interface IApiModel
    {
        PriorityViewModel ToViewModel();
    }
}
