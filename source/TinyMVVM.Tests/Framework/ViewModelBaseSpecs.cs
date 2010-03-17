using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TinyBDD.Specification.NUnit;
using TinyMVVM.Framework;
using TinyMVVM.Framework.Conventions;
using TinyMVVM.Tests.Framework.TestContext;
using System.ComponentModel;
using Moq;

namespace TinyMVVM.Tests.Framework
{
	class ViewModelBaseSpecs
	{
		[TestFixture]
		public class When_spawned : ViewModelBaseContext
		{
			[SetUp]
			public void Setup()
			{
				When(ClassThatImplements_ViewModelBase_is_spawned);
			}

			[Test]
			public void assure_it_isa_INotifyPropertyChanged()
			{
				Then(() =>
					(viewModel is INotifyPropertyChanged).ShouldBeTrue());
			}

			[Test]
			public void assure_it_has_a_PropertyChangeRecorder()
			{
				Then(() =>
					 viewModel.PropertyChangeRecorder.ShouldNotBeNull());
			}

			[Test]
			public void assure_it_has_a_CommandStateChangeRecorder()
			{
				Then(() =>
					 viewModel.CmdStateChangeRecorder.ShouldNotBeNull());
			}
		}

		[TestFixture]
		public class When_Default_conventions_are_applied : ViewModelBaseContext
		{
			[SetUp]
			public void Setup()
			{
				Given(ClassThatImplments_ViewModelBase_is_created);

				When("default conventions are applied (in constructor)");
			}

			[Test]
			public void assure_BindCommandsDelegatesToMethods_convention_is_applied()
			{
				Then(() =>
					 viewModel.Conventions.Where(c => c is BindCommandsDelegatesToMethods).Count().ShouldBe(1));
			}

			[Test]
			public void assure_InvokeOnInitialize_convention_is_applied()
			{
				Then(() =>
				     viewModel.Conventions.Where(c => c is InvokeOnInitialize).Count().ShouldBe(1));
			}
		}

		[TestFixture]
		public class When_applying_convention : ViewModelBaseContext
		{
			[SetUp]
			public void Setup()
			{
				Given(ClassThatImplments_ViewModelBase_is_created);

				When("applying convention");
			}

			[Test]
			public void assure_convention_args_is_validated()
			{
				Then(() =>
					this.ShouldThrowException<ArgumentNullException>(() =>
						viewModel.ApplyConvention(null)));
			}
		}

		[TestFixture]
		public class When_convention_is_applied : ViewModelBaseContext
		{
			[SetUp]
			public void Setup()
			{
				Given(ClassThatImplments_ViewModelBase_is_created);
				And(ConventionMock_is_created);

				When("convention is applied", () =>
					viewModel.ApplyConvention(conventionMock.Object));
			}

			[Test]
			public void assure_convention_is_applied()
			{
				Then(() =>
					conventionMock.Verify(c =>
						c.ApplyTo(It.Is<ViewModelBase>((vm) => vm == viewModel)), Times.Once()));
			}
		}
	}
}
