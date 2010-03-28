using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;

namespace TinyMVVM.Tests.Learning
{
    [TestFixture]
    public class NinjectLearningTests
    {
        [Test]
        public void How_to_add_obj_to_container_programantically()
        {
            IKernel kernel = new StandardKernel();

            kernel.Bind<IHelloWorld>().To(typeof(HelloWorld));
            kernel.Inject(new HelloWorld());

            var helloWorld = kernel.Get<IHelloWorld>();
            helloWorld.ShouldNotBeNull();
        }

        private interface IHelloWorld
        {
            void Hello();
        }

        private class HelloWorld : IHelloWorld
        {
            public void Hello()
            {
                Console.WriteLine("hello");
            }
        }
    }
}
