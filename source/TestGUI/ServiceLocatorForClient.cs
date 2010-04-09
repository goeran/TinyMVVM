//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Ninject;
//using TinyMVVM.Framework;
//using TestGUI.Services;
//using TestGUI.Services.Impl;
//using TinyMVVM.Framework.Services;
//using TinyMVVM.Framework.Services.Impl;

//namespace TestGUI
//{
//    public class ServiceLocatorForClient : IServiceLocator
//    {
//        private IKernel kernel;

//        public ServiceLocatorForClient()
//        {
//            kernel = new StandardKernel();
//            kernel.Bind<IAuthenticationService>().To<AuthenticationService>();
//            kernel.Bind<IBackgroundWorker>().To<BackgroundWorker>();
//        }

//        #region IServiceLocator Members

//        public T GetInstance<T>() where T : class
//        {
//            return kernel.Get<T>();
//        }

//        #endregion
//    }
//}
