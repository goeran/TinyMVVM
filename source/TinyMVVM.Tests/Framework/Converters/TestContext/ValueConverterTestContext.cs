using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Windows.Data;
using TinyBDD.Dsl.GivenWhenThen;

namespace TinyMVVM.Tests.Framework.Converters.TestContext
{
    internal class ValueConverterTestContext : NUnitScenarioClass
    {
        protected static IValueConverter converter;
        protected static Object result;

        protected GivenSemantics new_ValueConverter_is_created(IValueConverter o)
        {
            return Given("ValueConverter is created", () =>
                converter = o);
        }

        protected WhenSemantics convert_value(Object val)
        {
            return When("convert value", () =>
                result = Convert(val));
        }

        protected static object Convert(object val)
        {
            return converter.Convert(val, null, null, CultureInfo.CurrentCulture);
        }
    }
}
