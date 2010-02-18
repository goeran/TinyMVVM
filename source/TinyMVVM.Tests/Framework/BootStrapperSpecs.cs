using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Tests.Framework.TestContext;
using TinyMVVM.Framework;

namespace TinyMVVM.Tests.Framework.BootStrapperSpecs
{
    [TestFixture]
    public class When_get_from_Project : BootstrapperContext
    {
        private IBootstrapper bootstrapper;

        private BootstrapperFromProject b = new BootstrapperFromProject();

        [SetUp]
        public void Setup()
        {
            When(() =>
                bootstrapper = Bootstrapper.FromProject);
        }

        [Test]
        public void assure_Bootstrapper_from_project_is_returned()
        {
            Then(() =>
                 bootstrapper.ShouldBeInstanceOfType<BootstrapperFromProject>());   
        }

    }

    public class BootstrapperFromProject : IBootstrapper
    {
        public void Initialize(ServiceLocator serviceLocator)
        {
        }
    }

}
