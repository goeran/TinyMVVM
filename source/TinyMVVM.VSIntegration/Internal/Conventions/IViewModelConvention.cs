using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.VSIntegration.Internal.Model;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;

namespace TinyMVVM.VSIntegration.Internal.Conventions
{
    public interface IViewModelConvention
    {
        void Apply(ModelSpecification viewModelSpecification, File mvvmFile);
    }
}
