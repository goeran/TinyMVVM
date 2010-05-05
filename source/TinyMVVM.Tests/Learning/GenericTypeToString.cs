using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;

namespace TinyMVVM.Tests.Learning
{
    [TestFixture]
    public class GenericTypeToString
    {
        private List<string> names = new List<string>();
        private ObservableCollection<int> numbers = new ObservableCollection<int>();

        [Test]
        [Ignore]
        public void how_is_a_generic_Type_represented_as_string()
        {
            var t = names.GetType();

            t.FullName.ShouldBe("System.Collections.Generic.List`1[[System.String, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]");
        }

    }
}
