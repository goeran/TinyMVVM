using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;
using TinyBDD.Dsl.GivenWhenThen;
using TinyMVVM.Framework;
using Moq;
using TinyMVVM.Framework.Conventions;
using TinyMVVM.Framework.Services;

namespace TinyMVVM.Tests.Framework.TestContext
{
    public class ViewModelBaseContext : NUnitScenarioClass
    {
        protected static CustomViewModel viewModel;
        protected static CustomViewModel viewModel2;
        protected static AnotherCustomViewModel anotherViewModel;
    	protected static Mock<IViewModelConvention> conventionMock;
        protected CustomBackgroundWorker customBackgroundWorkerInstance = new CustomBackgroundWorker();

    	protected Context ClassThatImplments_ViewModelBase_is_created = () =>
    	{
    	    CustomViewModel.RemoveAllGlobalDependencies();
    		viewModel = new CustomViewModel();
    	};

        protected Context yet_another_ViewModel_is_created = () =>
        {
            anotherViewModel = new AnotherCustomViewModel();
        };

    	protected Context ConventionMock_is_created = () =>
    	{
    		conventionMock = new Mock<IViewModelConvention>();
    	};


        protected When ClassThatImplements_ViewModelBase_is_spawned = () =>
        {
            viewModel = new CustomViewModel();
        };

        public class CustomViewModel : ViewModelBase
        {
			public CustomViewModel()
			{
				ApplyDefaultConventions();
			}

        	public ReadOnlyCollection<IViewModelConvention> Conventions
        	{
				get { return AppliedConventions; }
        	}
        }

        public class AnotherCustomViewModel : ViewModelBase
        {
            
        }

        protected TestController GetTestControllerInViewModel()
        {
            var controller = viewModel.Controllers.Where(t => t is TestController).SingleOrDefault();
            return controller as TestController;
        }

        protected YetAnotherController GetYetAnotherControllerInViewModel()
        {
            var controller = viewModel.Controllers.Where(t => t is YetAnotherController).SingleOrDefault();
            return controller as YetAnotherController;
        }

        protected class TestController
        {
            public ViewModelBase ViewModel { get; set; }
            public IBackgroundWorker BackgroundWorker { get; set; }

            public TestController(CustomViewModel customViewModel, IBackgroundWorker backgroundWorker)
            {
                ViewModel = customViewModel;
                BackgroundWorker = backgroundWorker;
            }
        }

        protected class YetAnotherController
        {
            public ViewModelBase ViewModel { get; set; }
            public IBackgroundWorker BackgroundWorker { get; set; }

            public YetAnotherController(CustomViewModel customViewModel, IBackgroundWorker backgroundWorker)
            {
                ViewModel = customViewModel;
                BackgroundWorker = backgroundWorker;
            }
        }
        protected class AnotherController
        {
            public ViewModelBase ViewModel { get; set; }

            public AnotherController(CustomViewModel customViewModel)
            {
                ViewModel = customViewModel;
            }
        }

        protected class SharedModule : NinjectModule
        {
            public override void Load()
            {
                Kernel.Bind<IBackgroundWorker>().To<TinyMVVM.Framework.Services.Impl.BackgroundWorker>();
            }
        }

        protected class CustomBackgroundWorker : IBackgroundWorker
        {
            public void Invoke(Action a)
            {
            }
        }
    }
}
