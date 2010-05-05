using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.SemanticModel;
using TinyMVVM.Tests.SemanticModel.TestContext;

namespace TinyMVVM.Tests.SemanticModel.ViewModelDataSpec
{
    [TestFixture]
    public class When_spawning : ViewModelPropertyContext
    {
        [SetUp]
        public void Setup()
        {
            When("spawning");
        }

        [Test]
        public void assure_Name_arg_is_validated()
        {
            Then(() =>
                this.ShouldThrowException<ArgumentNullException>(() =>
                    new ViewModelProperty(null, typeof(string).Name, false)));
        }

        [Test]
        public void assure_Type_arg_is_validated()
        {
            Then(() =>
                this.ShouldThrowException<ArgumentNullException>(() =>
                    new ViewModelProperty("Username", null, false)));
        }
    }

    [TestFixture]
    public class When_spawned : ViewModelPropertyContext
    {
        [SetUp]
        public void Setup()
        {
            When(ViewModelProperty_is_spawned);
        }

        [Test]
        public void assure_it_has_a_Name()
        {
            Then(() =>
                 viewModelProperty.Name.ShouldNotBeNull());
        }

        [Test]
        public void assure_it_has_a_Type()
        {
            Then(() =>
                viewModelProperty.Type.ShouldBe(typeof(string).Name));
        }

        [Test]
        public void assure_it_has_a_IsObservable_flag()
        {
            Then(() =>
                viewModelProperty.IsObservable.ShouldBeFalse());
        }

        [Test]
        public void assure_it_has_Attributes()
        {
            Then(() =>
                 viewModelProperty.Attributes.ShouldNotBeNull());
        }

    }

	[TestFixture]
	public class When_eval_IsPrimitiveType : ViewModelPropertyContext
	{
		[Test]
		public void assure_Class_is_not_eval_as_primitive_type()
		{
			Given(ViewModelProperty_is_created_and_its_a_type_of("Customer"));

			When(eval);

			Then(() =>
			     viewModelProperty.IsPrimitiveType.ShouldBeFalse());
		}

		[Test]
		public void assure_types_are_correctly_evaluated()
		{
			var types = new Dictionary<string, bool>();
			types.Add("string", true);
			types.Add("int", true);
			types.Add("int16", true);
			types.Add("short", true);
			types.Add("ushort", true);
			types.Add("byte", true);
			types.Add("float", true);
			types.Add("double", true);
			types.Add("uint", true);
			types.Add("sbyte", true);
			types.Add("long", true);
			types.Add("ulong", true);
			types.Add("char", true);

			foreach (var type in types)
			{
				Scenario("When eval IsPrimitiveType (" + type.Key + ")");
				Given(ViewModelProperty_is_created_and_its_a_type_of(type.Key));
				When(eval);

				Then(() =>
					 viewModelProperty.IsPrimitiveType.ShouldBe(type.Value));
				StartScenario();
			}
		}
	}
}
