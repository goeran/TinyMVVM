using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using Moq;
using TestGUI.Services;
using TinyMVVM.Framework.Services;
using TinyMVVM.Framework.Testing;
using TinyMVVM.Framework.Testing.Services;

namespace TestGUI.Tests
{
    [Export(typeof(ServiceLocatorForTesting))]
    public class CustomServiceLocatorForTests : ServiceLocatorForTesting
    {
        [Export(typeof(Mock<IAuthenticationService>))]
        private readonly Mock<IAuthenticationService> authServiceFake;
 
        [Export(typeof(IAuthenticationService))]
        private readonly IAuthenticationService authService;

		[Export(typeof(IUIInvoker))]
    	private readonly IUIInvoker uiInvokerForTesting;

        public CustomServiceLocatorForTests()
        {
            authServiceFake = new Mock<IAuthenticationService>();
            authService = authServiceFake.Object;
        	uiInvokerForTesting = new UIInvokerForTesting();

            aggregateCatalog.Catalogs.Add(new TypeCatalog(typeof(CustomServiceLocatorForTests)));
        }
    }
}
