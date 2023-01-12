using System;
using auth.Application;

namespace auth.Controllers
{
    public class UserLoginCredentials
    {
        public UserLoginCredentials()
        {
        }

        public string Password { get; set; }

        public string UserName { get; set; }
    }

}

