using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.Tests.SemanticModel.TestContext;

namespace TinyMVVM.Tests.SemanticModel.ModelSpecificationSpecs
{
    [TestFixture]
    public class When_spawned : ModelSpecificationContext
    {
        [SetUp]
        public void Setup()
        {
            When(spawned);
        }

        [Test]
        public void assure_it_has_ViewModels()
        {
            Then(() =>
                 modelSpecification.ViewModels.ShouldNotBeNull());
        }

        [Test]
        public void assure_it_has_Usings() //Namespace includes
        {
            Then(() =>
                modelSpecification.Usings.ShouldNotBeNull());
        }

    }

    [TestFixture]
    public class When_adding_ViewModel : ModelSpecificationContext
    {
        [SetUp]
        public void Setup()
        {
            Given(ModelSpecification_is_created);

            When("adding ViewModel");
        }

        [Test]
        public void assure_ViewModel_arg_is_validated()
        {
            Then(() =>
                this.ShouldThrowException<ArgumentNullException>(() =>
                    modelSpecification.AddViewModel(null)));
        }
    }

    [TestFixture]
    public class When_ViewModel_is_added : ModelSpecificationContext
    {
        private ViewModel viewModel = new ViewModel("LoginViewModel");

        [SetUp]
        public void Setup()
        {
            Given(ModelSpecification_is_created);

            When("ViewModel is added", () =>
                modelSpecification.AddViewModel(viewModel));
        }

        [Test]
        public void assure_ViewModel_is_added()
        {
            Then(() =>
                modelSpecification.ViewModels.ShouldContain(viewModel));
        }
    }
}
