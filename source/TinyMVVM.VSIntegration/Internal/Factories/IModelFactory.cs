using TinyMVVM.VSIntegration.Internal.Model;

namespace TinyMVVM.VSIntegration.Internal.Factories
{
    public interface IModelFactory
    {
        Solution NewSolution(string path);
    }
}
