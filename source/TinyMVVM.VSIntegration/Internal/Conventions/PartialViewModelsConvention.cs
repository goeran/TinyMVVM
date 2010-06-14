using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Conventions
{
    /// <summary>
    /// This convention creates a partial class for very ViewModel defined in
    /// the mvvm file if they don't already exists.
    /// </summary>
    public class PartialViewModelsConvention : IViewModelConvention
    {
        public void Apply(ModelSpecification mvvmDefinition, File mvvmFile)
        {
            var viewModelFolder = mvvmFile.Parent;

            foreach (var viewModel in mvvmDefinition.ViewModels)
            {
                var name = string.Format("{0}.cs", viewModel.Name);
                if (!viewModelFolder.HasFile(name))
                    viewModelFolder.NewFile(name);    
            }
        }
    }
}
