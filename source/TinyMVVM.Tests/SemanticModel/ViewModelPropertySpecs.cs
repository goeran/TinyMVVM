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
    public class When_spawning : ViewModelDataContext
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
    public class When_spawned : ViewModelDataContext
    {
        [SetUp]
        public void Setup()
        {
            When(ViewModelData_is_spawned);
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
}
