using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Factories
{
    public interface IModelFactory
    {
        Solution NewSolution();
        Project NewProject();
    }
}
