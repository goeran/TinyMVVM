using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public void OnInitialize()
        {
            backgroundWorker = GetInstance<IBackgroundWorker>();
            authService = GetInstance<IAuthenticationService>();

            this.PropertyChanged += propertyChanged;
            IsVisible = true;
        }

        void propertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Username" ||
                e.PropertyName == "Password")
                Login.TriggerCanExecuteChanged();
        }

        public void OnLogin()
        {
            backgroundWorker.Invoke(() =>
            {
                ReadOnly = true;
                var isAuthenticated = authService.Authenticate(
                    Username, Password);

                if (isAuthenticated == false)
                    Status = "Invalid credentials";
                else
                {
                    IsVisible = false;
                }
                ReadOnly = false;
            });
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

        public void OnCancel()
        {
            Username = null;
            Password = null;
        }

        public bool CanCancel()
        {
            if (Username != null || Password != null)
                return true;
            else
                return false;
        }
    }
}
