using System;

namespace EmployeeWpfApp.Services
{
    public interface IUserService
    {
        bool IsLoggedIn { get; }
        string UserName { get; }

        event Action LoggedIn;

        void LogIn(string userName);

    }
}