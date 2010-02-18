using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Core;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using Ninject.Core.Binding;

namespace TinyMVVM.Tests.Learning
{
    [TestFixture]
    public class NinjectLearningTests
    {
        [Test]
        public void How_to_add_obj_to_container_programantically()
        {
            IKernel kernel = new StandardKernel(new InlineModule(m =>
            {
                m.Bind<IHelloWorld>().To(typeof(HelloWorld));
            }));
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
