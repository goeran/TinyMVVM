using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.VSIntegration.Internal.Model;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;
using TinyMVVM.VSIntegration.Internal.Services;
using TinyMVVM.VSIntegration.Internal.Templates;

namespace TinyMVVM.VSIntegration.Internal.Conventions
{
    /// <summary>
    /// This convention creates a partial class for very ViewModel defined in
    /// the mvvm file if they don't already exists.
    /// </summary>
    public class PartialViewModelsConvention : IViewModelConvention
    {
    	private ICodeGeneratorService codeGeneratorService;
    	private readonly ITemplate codeGeneratorTemplate = new PartialViewModelTemplate();

    	public PartialViewModelsConvention(ICodeGeneratorService codeGeneratorService)
    	{
    		this.codeGeneratorService = codeGeneratorService;
    	}

    	public void Apply(ModelSpecification mvvmDefinition, File mvvmFile)
        {
            var viewModelFolder = mvvmFile.Parent;

            foreach (var viewModel in mvvmDefinition.ViewModels)
            {
                var name = string.Format("{0}.cs", viewModel.Name);
                if (!viewModelFolder.HasFile(name))
                {
					var newFile = viewModelFolder.NewFile(name);    

					codeGeneratorService.Generate(mvvmFile, newFile, new CodeGeneratorArgs(mvvmDefinition, viewModel, codeGeneratorTemplate));
                }
            }
        }
    }
}
