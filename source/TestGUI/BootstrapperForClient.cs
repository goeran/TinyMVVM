using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Core;
using TinyMVVM.Framework;
using TestGUI.Services;
using TestGUI.Services.Impl;
using TinyMVVM.Framework.Services;
using TinyMVVM.Framework.Services.Impl;

namespace TestGUI
{
    public class BootstrapperForClient : IBootstrapper
    {
        #region IBootstrapper Members

        public void Initialize(ServiceLocator serviceLocator)
        {
            ServiceLocator.SetLocator(new ServiceLocatorForClient());
        }

        #endregion
    }

    public class ServiceLocatorForClient : IServiceLocator
    {
        private IKernel kernel;

        public ServiceLocatorForClient()
        {
            kernel = new StandardKernel(
                new InlineModule(m =>
                {
                    m.Bind<IAuthenticationService>().To<AuthenticationService>();
                    m.Bind<IBackgroundWorker>().To<BackgroundWorker>();
                }));
        }

        #region IServiceLocator Members

        public T GetInstance<T>() where T : class
        {
            return kernel.Get<T>();
        }

        #endregion
    }
}
