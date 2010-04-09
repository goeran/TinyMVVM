using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using Moq;
using Ninject;
using TestGUI.Services;
using TinyMVVM.Framework.Testing;

namespace TestGUI.Tests
{
    [Export(typeof(ServiceLocatorForTesting))]
    public class CustomServiceLocatorForTests : ServiceLocatorForTesting
    {
        [Export(typeof(Mock<IAuthenticationService>))]
        private readonly Mock<IAuthenticationService> authServiceFake;
 
        [Export(typeof(IAuthenticationService))]
        private readonly IAuthenticationService authService;

        public CustomServiceLocatorForTests()
        {
            authServiceFake = new Mock<IAuthenticationService>();
            authService = authServiceFake.Object;

            aggregateCatalog.Catalogs.Add(new TypeCatalog(typeof(CustomServiceLocatorForTests)));
        }
    }
}
