using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using TinyMVVM.Framework;
using TestGUI.Services;
using TestGUI.Services.Impl;
using TinyMVVM.Framework.Services;
using TinyMVVM.Framework.Services.Impl;

namespace TestGUI
{
    [Export(typeof(IServiceLocator))]
    public class ServiceLocatorForClient : DefaultServiceLocator
    {
        public ServiceLocatorForClient()
        {
            aggregateCatalog.Catalogs.Add(new TypeCatalog(
                typeof(BackgroundWorker),
                typeof(AuthenticationService)));
        }
    }
}
