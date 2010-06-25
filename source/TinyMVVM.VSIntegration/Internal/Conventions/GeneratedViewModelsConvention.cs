using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.VSIntegration.Internal.Model;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;
using TinyMVVM.VSIntegration.Internal.Services;
using TinyMVVM.VSIntegration.Internal.Templates;

namespace TinyMVVM.VSIntegration.Internal.Conventions
{
    public class GeneratedViewModelsConvention : IViewModelConvention
    {
    	private ModelSpecification viewModelSpecification;
    	private File mvvmFile;
    	private ICodeGeneratorService codeGeneratorService;
		private readonly ITemplate codeGenTemplate = new GeneratedViewModelTemplate();

    	public GeneratedViewModelsConvention(ICodeGeneratorService codeGeneratorService)
    	{
    		this.codeGeneratorService = codeGeneratorService;
    	}

    	public void Apply(ModelSpecification viewModelSpecification, File mvvmFile)
        {
    		this.mvvmFile = mvvmFile;
    		this.viewModelSpecification = viewModelSpecification;

    		RemoveDeletedViewModelsFromCodeBehind();

    		AddNewViewModelsToCodeBehind();
        }

    	private void RemoveDeletedViewModelsFromCodeBehind()
    	{
    		foreach (var codeBehindFile in mvvmFile.CodeBehindFiles.ToList())
    		{
				if (!FindViewModelForFile(codeBehindFile.Name))
					mvvmFile.DeleteCodeBehindFile(codeBehindFile.Name);
    		}
    	}

    	private bool FindViewModelForFile(string codeBehindFileName)
		{
			foreach (var viewModel in viewModelSpecification.ViewModels)
			{
				var fileName = string.Format("{0}.mvvm.cs", viewModel.Name);
				if (codeBehindFileName == fileName)
					return true;
			}

			return false;
		}

		private void AddNewViewModelsToCodeBehind()
		{
			foreach (var viewModel in viewModelSpecification.ViewModels)
			{
				var fileName = string.Format("{0}.mvvm.cs", viewModel.Name);
				if (!mvvmFile.HasCodeBehindFile(fileName))
					mvvmFile.NewCodeBehindFile(fileName);
				codeGeneratorService.Generate(mvvmFile, mvvmFile.GetCodeBehindFile(fileName), 
					new CodeGeneratorArgs(viewModelSpecification, viewModel, codeGenTemplate));
			}
		}
    }
}
