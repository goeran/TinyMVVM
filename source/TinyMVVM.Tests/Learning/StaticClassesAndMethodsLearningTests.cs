using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;

namespace TinyMVVM.Tests.Learning
{
    [TestFixture]
    public class StaticClassesAndMethodsLearningTests
    {
        [Test]
        [Ignore("Fails because I havent managed to find a Solution yet")]
        public void How_to_get_the_declared_class_name_when_calling_a_Static_method()
        {
            var className = TestClass.GetClassNameWhenCallingStaticMethod();

            var subClassName = TestSubClass.GetClassNameWhenCallingStaticMethod();

            className.ShouldBe(typeof (TestClass).Name);
            subClassName.ShouldBe(typeof (TestSubClass).Name);
        }


        class TestClass
        {
            public static string GetClassNameWhenCallingStaticMethod()
            {
                var st = new StackTrace();
                //return st.GetFrames().First().GetMethod().DeclaringType.Name;
                return MethodInfo.GetCurrentMethod().DeclaringType.Name;
            }
        }

        class TestSubClass : TestClass
        {
            
        }
    }
}
