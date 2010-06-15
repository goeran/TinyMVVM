using System;
using System.Dynamic;
using LinFu.AOP.Cecil;
using LinFu.AOP.Interfaces;
using LinFu.Proxy;
using LinFu.Proxy.Interfaces;
using NUnit.Framework;

namespace TinyMVVM.VSIntegration.Tests.Learning
{
    [TestFixture]
    public class LinFuLearningTests
    {
        [Test]
        public void How_to_create_a_dynamic_proxy()
        {
            var factory = new ProxyFactory();
            var proxy = new PersonProxy();
            var person = factory.CreateProxy<Person>(proxy as IInterceptor);

            person.Hello();
            person.Greeting();
            Console.WriteLine("RetValue: Foo");
        }
    }

    public class Person
    {
        public virtual void Hello()
        {
            Console.WriteLine("Hello");
        }

        public virtual void Greeting()
        {
            Console.WriteLine("Greeting");
        }

        public virtual string Foo(string retValue)
        {
            Console.WriteLine("Foo");
            return retValue;
        }
    }

    public class PersonProxy : Person, IInterceptor
    {
        public PersonProxy()
        {
        }

        public object Intercept(IInvocationInfo info)
        {
            Console.WriteLine("Intercept: " + info.TargetMethod.Name);
            return info.TargetMethod.Invoke(this, info.Arguments);
        }
    }
}
