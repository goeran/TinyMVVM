using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.VSIntegration.Internal.Model;

namespace TinyMVVM.VSIntegration.Internal.Conventions
{
    public interface IViewModelConvention
    {
        void Apply(ModelSpecification viewModelSpecification, File mvvmFile);
    }
}
