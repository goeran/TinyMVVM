using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Framework;
using TinyMVVM.Tests.Framework.TestContext;
using Moq;

namespace TinyMVVM.Tests.Framework.ServiceLocatorSpecs
{
    [TestFixture]
    public class When_SetLocator : ServiceLocatorContext
    {
        private CustomServiceLocator customServiceLocator = new CustomServiceLocator();

        [Test]
        public void assure_Locator_is_set()
        {
            When("SetLocator", () =>
                ServiceLocator.SetLocator(customServiceLocator));

            Then(() =>
                 ServiceLocator.Instance.ShouldBe(customServiceLocator));
        }

        [Test]
        public void assure_Locator_arg_is_validated()
        {
            When("SetLocator");

            Then(() =>
                this.ShouldThrowException<ArgumentNullException>(() =>
                    ServiceLocator.SetLocator(null)));
        }

        private class CustomServiceLocator : IServiceLocator
        {
            public T GetInstance<T>() where T : class
            {
                throw new NotImplementedException();
            }
        }
    }


}
