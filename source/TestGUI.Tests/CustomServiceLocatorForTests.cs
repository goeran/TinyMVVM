using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Ninject;
using TestGUI.Services;
using TinyMVVM.Framework.Testing;

namespace TestGUI.Tests
{
    public class CustomServiceLocatorForTests : ServiceLocatorForTesting
    {
        public CustomServiceLocatorForTests()
        {
            var authServiceFake = new Mock<IAuthenticationService>();
            kernel.Bind<Mock<IAuthenticationService>>().ToConstant(authServiceFake);
            kernel.Bind<IAuthenticationService>().ToConstant(authServiceFake.Object);
        }
    }
}
