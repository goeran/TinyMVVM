using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;

namespace TinyMVVM.Framework
{
    public class DefaultServiceLocator : IServiceLocator
    {
        protected CompositionContainer container;
        protected AggregateCatalog aggregateCatalog;

        public DefaultServiceLocator()
        {
            aggregateCatalog = new AggregateCatalog();

            container = new CompositionContainer(aggregateCatalog);
        }

        public virtual T GetInstance<T>() where T : class
        {
            return container.GetExportedValues<T>().FirstOrDefault();
        }
    }
}
