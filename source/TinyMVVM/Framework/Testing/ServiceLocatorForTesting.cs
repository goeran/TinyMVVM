using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using Moq;
using TinyMVVM.Framework.Services;

namespace TinyMVVM.Framework.Testing
{
    public class ServiceLocatorForTesting : DefaultServiceLocator
    {
        [Export(typeof(Mock<IBackgroundWorker>))]
        private readonly Mock<IBackgroundWorker> backgroundWorkerFake;

        [Export(typeof(IBackgroundWorker))]
        private readonly IBackgroundWorker backgroundWorker;

        public ServiceLocatorForTesting()
        {
            backgroundWorkerFake = new Mock<IBackgroundWorker>();
			backgroundWorkerFake.Setup(
				w => w.Invoke(It.IsAny<Action>())).
					Callback((Action a) =>
					{
						a.Invoke();
					});
            backgroundWorker = backgroundWorkerFake.Object;

            aggregateCatalog.Catalogs.Add(new TypeCatalog(typeof(ServiceLocatorForTesting)));
        }

    	public static ServiceLocatorForTesting GetServiceLocator()
    	{
    	    var container = new CompositionContainer(new AssemblyCatalog(Assembly.GetCallingAssembly()));
    	    return container.GetExportedValues<ServiceLocatorForTesting>().FirstOrDefault();
    	}
    }
}
