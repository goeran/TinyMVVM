using System.Linq;
using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Conventions
{
    /// <summary>
    /// This convention does two thing:
    /// - add 'Views' folder in the project if it not already exists
    /// - add View files for every ViewModel defined in the mvvm file if not already exists.
    ///   A view file = .xaml file
    /// </summary>
    public class ViewsConvention
    {
        private const string viewsFolderName = "Views";

        public void Apply(ModelSpecification mvvmDefinition, File mvvmFile)
        {
            var project = mvvmFile.Project;
            if (!project.HasFolder(viewsFolderName))
            {
                project.NewFolder(viewsFolderName);
            }

            foreach (var viewModel in mvvmDefinition.ViewModels)
            {
                var viewsFolder = project.GetSubFolder(viewsFolderName);
                if (!viewsFolder.HasFile(viewModel.Name + ".xaml"))
                    viewsFolder.NewFile(viewModel.Name + ".xaml");
            }
        }
    }
}
