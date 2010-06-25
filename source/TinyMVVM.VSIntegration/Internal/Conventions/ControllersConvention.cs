using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.VSIntegration.Internal.Factories;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;
using TinyMVVM.VSIntegration.Internal.Services;
using TinyMVVM.VSIntegration.Internal.Templates;

namespace TinyMVVM.VSIntegration.Internal.Conventions
{
	public class ControllersConvention : IViewModelConvention
	{
		private readonly ITemplate codeGenTemplate = new ControllerTemplate();
		private ICodeGeneratorService codeGeneratorService;
		private IModelFactory factory;
		private const string Controllers = "Controllers";

		public ControllersConvention(ICodeGeneratorService codeGeneratorService, IModelFactory factory)
		{
			this.codeGeneratorService = codeGeneratorService;
			this.factory = factory;
		}

		public void Apply(ModelSpecification viewModelSpecification, File mvvmFile)
		{
			var project = mvvmFile.Project as Project;

			if (!project.HasFolder(Controllers))
				project.AddFolder(factory.NewFolder(Controllers, project));

			var controllersFolder = project.GetSubFolder(Controllers);

			foreach (var viewModel in viewModelSpecification.ViewModels)
			{
				var fileName = string.Format("{0}Controller.cs", viewModel.Name);

				if (!controllersFolder.HasFile(fileName))
				{
					controllersFolder.NewFile(fileName);

					var controllerFile = controllersFolder.GetFile(fileName);

					codeGeneratorService.Generate(mvvmFile, controllerFile, new CodeGeneratorArgs(viewModelSpecification, viewModel, codeGenTemplate));
				}
			}
		}
	}
}
