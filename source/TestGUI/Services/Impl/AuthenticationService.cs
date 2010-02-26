using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGUI.Services.Impl
{
    public class AuthenticationService : IAuthenticationService
    {
        #region IAuthenticationService Members

        public bool Authenticate(string username, string password)
        {
            if (username == "goeran" &&
                password == "hansen")
                return true;
            else
                return false;
        }

        #endregion
    }
}
