using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Ninject.Core;
using TinyMVVM.Framework;
using TinyMVVM.Framework.Services;
using TinyMVVM.Framework.Services.Impl;

namespace TestGUI.Tests
{
    public class ServiceLocatorForTesting : IServiceLocator
    {
        protected readonly IKernel kernel;

        public ServiceLocatorForTesting()
        {
            kernel = new StandardKernel(new InlineModule(m =>
            {
                var backgroundWorkerFake = new Mock<IBackgroundWorker>();
				backgroundWorkerFake.Setup(
					w => w.Invoke(It.IsAny<Action>())).
						Callback((Action a) =>
						{
							a.Invoke();
						});

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
