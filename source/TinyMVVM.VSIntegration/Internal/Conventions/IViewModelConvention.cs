using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Conventions
{
    public interface IViewModelConvention
    {
        void Apply(ModelSpecification viewModelSpecification, File mvvmFile);
    }
}
