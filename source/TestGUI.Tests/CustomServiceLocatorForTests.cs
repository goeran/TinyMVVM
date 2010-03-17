﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Ninject.Core;
using TestGUI.Services;
using TinyMVVM.Framework.Testing;

namespace TestGUI.Tests
{
    internal class CustomServiceLocatorForTests : ServiceLocatorForTesting
    {
        public CustomServiceLocatorForTests()
        {
			kernel.Load(new InlineModule(m =>
            {
                var authServiceFake = new Mock<IAuthenticationService>();
                m.Bind<Mock<IAuthenticationService>>().ToConstant(authServiceFake);
                m.Bind<IAuthenticationService>().ToConstant(authServiceFake.Object);
            }));
        }
    }
}