using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using TinyBDD.Specification.NUnit;

namespace TinyMVVM.Tests.Learning
{
    [TestFixture]
    public class MoqLearningTests
    {
        [Test]
        public void Create_mock_with_using_generics()
        {
            var mock = GetMock<IHelloWorld>();
            mock.ShouldBeInstanceOfType<Mock<IHelloWorld>>();
        }

        private Mock<T> GetMock<T>() where T : class
        {
            var m = new Mock<T>();

            return m;
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
