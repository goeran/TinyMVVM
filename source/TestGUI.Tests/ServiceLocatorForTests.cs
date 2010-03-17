using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Ninject.Core;
using TinyMVVM.Framework;
using TestGUI.Services;
using TinyMVVM.Framework.Services;
using TinyMVVM.Framework.Services.Impl;

namespace TestGUI.Tests
{
    internal class ServiceLocatorForTests : IServiceLocator
    {
        private IKernel kernel;

        public ServiceLocatorForTests()
        {
            kernel = new StandardKernel(new InlineModule(m =>
            {
                var authServiceFake = new Mock<IAuthenticationService>();
                m.Bind<Mock<IAuthenticationService>>().ToConstant(authServiceFake);
                m.Bind<IAuthenticationService>().ToConstant(authServiceFake.Object);
                        
                var backgroundWorkerFake = new Mock<IBackgroundWorker>();
                m.Bind<Mock<IBackgroundWorker>>().ToConstant(backgroundWorkerFake);
                m.Bind<IBackgroundWorker>().ToConstant(backgroundWorkerFake.Object);
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
