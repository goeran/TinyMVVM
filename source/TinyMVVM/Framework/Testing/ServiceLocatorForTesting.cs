using System;
using System.Linq;
using System.Reflection;
using Moq;
using Ninject.Core;
using TinyMVVM.Framework.Services;

namespace TinyMVVM.Framework.Testing
{
    public class ServiceLocatorForTesting : IServiceLocator
    {
        protected readonly IKernel kernel;

        protected ServiceLocatorForTesting()
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

    	public static ServiceLocatorForTesting GetServiceLocator()
    	{
    		ServiceLocatorForTesting serviceLocatorForTesting = null;

			var serviceLocators = Assembly.GetCallingAssembly().GetTypes().
					Where(t => t.IsSubclassOf(typeof(ServiceLocatorForTesting))).ToList();

			if (serviceLocators.Count > 0)
			{
				serviceLocatorForTesting = Activator.CreateInstance(serviceLocators.First()) as ServiceLocatorForTesting;
			}
			else
			{
				serviceLocatorForTesting = new ServiceLocatorForTesting();
			}

			return serviceLocatorForTesting;
    	}
    }
}
