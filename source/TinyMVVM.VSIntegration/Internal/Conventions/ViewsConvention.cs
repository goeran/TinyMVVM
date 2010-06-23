using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.VSIntegration.Internal.Factories;
using TinyMVVM.VSIntegration.Internal.Model;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;
using TinyMVVM.VSIntegration.Internal.Services;
using TinyMVVM.VSIntegration.Internal.Templates;

namespace TinyMVVM.VSIntegration.Internal.Conventions
{
    /// <summary>
    /// This convention does two thing:
    /// - add 'Views' folder in the project if it not already exists
    /// - add View files for every ViewModel defined in the mvvm file if not already exists.
    ///   A view file = .xaml file
    /// </summary>
    public class ViewsConvention : IViewModelConvention
    {
    	private ICodeGeneratorService codeGeneratorService;
    	private const string Views = "Views";
    	private readonly ITemplate viewCodeGenTemplate = new ViewTemplate();
    	private readonly ITemplate viewCodeBehindCodeGenTemplate = new ViewCodeBehindTemplate();
    	private IModelFactory factory;

    	public ViewsConvention(ICodeGeneratorService codeGeneratorService, IModelFactory factory)
    	{
    		this.factory = factory;
    		this.codeGeneratorService = codeGeneratorService;
    	}

    	public void Apply(ModelSpecification mvvmDefinition, File mvvmFile)
        {
            var project = mvvmFile.Project;
            if (!project.HasFolder(Views))
            {
                project.AddFolder(factory.NewFolder(Views, project));
            }

            foreach (var viewModel in mvvmDefinition.ViewModels)
            {
                var fileName = string.Format("{0}.xaml", viewModel.Name);
            	var codeBehindFileName = string.Format("{0}.xaml.cs", viewModel.Name);

                var viewsFolder = project.GetSubFolder(Views);
                if (!viewsFolder.HasFile(fileName))
                {
					var viewFile = factory.NewFile(fileName, viewsFolder);
                	viewsFolder.AddFile(viewFile);
					codeGeneratorService.Generate(mvvmFile, viewFile, 
						new CodeGeneratorArgs(mvvmDefinition, viewModel, viewCodeGenTemplate));

                	var viewCodeBehindFile = viewFile.NewCodeBehindFile(codeBehindFileName);
					codeGeneratorService.Generate(mvvmFile, viewCodeBehindFile, 
						new CodeGeneratorArgs(mvvmDefinition, viewModel, viewCodeBehindCodeGenTemplate));
                }
            }
        }
    }
}
