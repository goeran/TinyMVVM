using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.Framework;

namespace TinyMVVM.IoC
{
	public static class RemoveAllGlobalDependencies
	{
		public static void ForAllViewModels()
		{
			Framework.ViewModelBase.RemoveAllGlobalDependencies();
		}
	}

	public static class ConfigureGlobalDependencies
	{
		public static void ForAllViewModels(Action<DependencyConfigSemantics> config)
		{
			Framework.ViewModelBase.ConfigureGlobalDependencies(config);
		}
	}

	public static class ViewModelBaseExtensions
	{
		public static void ConfigureGlobalDependencies(this Framework.ViewModelBase viewModel, Action<DependencyConfigSemantics> config)
		{
			Framework.ViewModelBase.ConfigureGlobalDependencies(config);
		}

		public static void RemoveAllGlobalDependencies(this Framework.ViewModelBase viewModel)
		{
			Framework.ViewModelBase.RemoveAllGlobalDependencies();			
		}

		public static void ConfigureDependencies(this Framework.ViewModelBase viewModel, Action<DependencyConfigSemantics> config)
		{
			viewModel.ConfigureDependencies(config);
		}

		public static T GetDependency<T>(this Framework.ViewModelBase viewModel) where T : class
		{
			return viewModel.GetDependency<T>();
		}

		public static T CreateController<T>(this Framework.ViewModelBase viewModel)
		{
			return viewModel.CreateController<T>();
		}
	}
}
