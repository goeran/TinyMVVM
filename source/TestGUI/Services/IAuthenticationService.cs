using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGUI.Services
{
    public interface IAuthenticationService
    {
        bool Authenticate(string username, string password);
    }
}
