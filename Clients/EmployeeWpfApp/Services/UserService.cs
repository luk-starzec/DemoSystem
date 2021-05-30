using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWpfApp.Services
{
    public class UserService : IUserService
    {
        private bool isLoggedIn;
        private string userName;

        public bool IsLoggedIn => isLoggedIn;
        public string UserName => userName;

        public event Action LoggedIn;

        public void LogIn(string userName)
        {
            this.userName = userName;
            isLoggedIn = true;

            LoggedIn?.Invoke();
        }
    }
}
