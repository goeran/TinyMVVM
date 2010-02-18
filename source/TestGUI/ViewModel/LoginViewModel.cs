using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGUI.Services;
using TinyMVVM.Framework.Services;

namespace TestGUI.ViewModel
{
    public partial class LoginViewModel
    {
        private IBackgroundWorker backgroundWorker;
        private IAuthenticationService authService;

        private void OnInitialize()
        {
            backgroundWorker = GetInstance<IBackgroundWorker>();
            authService = GetInstance<IAuthenticationService>();

            IsVisible = true;
        }

        private void OnLogin()
        {
            backgroundWorker.Invoke(() =>
            {
            });

            var isAuthenticated = authService.Authenticate(
                Username, Password);

            if (isAuthenticated == false)
                Status = "Invalid credentials";
            else
            {
                IsVisible = false;

            }
        }

        private bool CanLogin()
        {
            if (UsernameAndPasswordIsEntered())
                return true;
            else
                return false;
        }

        private bool UsernameAndPasswordIsEntered()
        {
            return Username != null && Password != null;
        }

        private void OnCancel()
        {
            Username = null;
            Password = null;
        }

        private bool CanCancel()
        {
            if (Username != null || Password != null)
                return true;
            else
                return false;
        }
    }
}
