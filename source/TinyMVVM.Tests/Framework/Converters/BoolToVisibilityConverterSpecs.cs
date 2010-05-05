using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Framework.Converter;
using TinyMVVM.Tests.Framework.Converters.TestContext;

namespace TinyMVVM.Tests.Framework.Converters
{
    class BoolToVisibilityConverterSpecs
    {
        [TestFixture]
        public class When_convert_an_invalid_value : ValueConverterTestContext
        {
            [SetUp]
            public void Setup()
            {
                Given(new_ValueConverter_is_created(new BoolToVisibilityConverter()));

                When("convert value");
            }

            [Test]
            public void assure_arg_is_validated()
            {
                Then(() =>
                    this.ShouldThrowException<ArgumentException>(() =>
                    Convert("bah")));
            }

        }


        [TestFixture]
        public class When_convert_True : ValueConverterTestContext
        {
            [SetUp]
            public void Setup()
            {
                Given(new_ValueConverter_is_created(new BoolToVisibilityConverter()));

                When(convert_value(true));
            }

            [Test]
            public void assure_result_equals_Visible()
            {
                Then(() =>
                     result.ShouldBe(Visibility.Visible));
            }
        }

        [TestFixture]
        public class When_convert_False : ValueConverterTestContext
        {
            [SetUp]
            public void Setup()
            {
                Given(new_ValueConverter_is_created(new BoolToVisibilityConverter()));

                When(convert_value(false));
            }

            [Test]
            public void assure_result_equals_Collapsed()
            {
                Then(() =>
                     result.ShouldBe(Visibility.Collapsed));
            }


        }

        [TestFixture]
        public class When_invert : ValueConverterTestContext
        {
            [SetUp]
            public void Setup()
            {
                Given(new_ValueConverter_is_created(new BoolToVisibilityConverter()
                {
                    Invert = true
                }));
                And("it's set to invert boolean values");
            }

            [Test]
            public void assure_True_equals_Collapsed()
            {
                When(convert_value(true));

                Then(() =>
                    result.ShouldBe(Visibility.Collapsed));
            }

            [Test]
            public void assure_False_equals_Visibile()
            {
                When(convert_value(false));

                Then(() =>
                    result.ShouldBe(Visibility.Visible));
            }

        }

    }
}
