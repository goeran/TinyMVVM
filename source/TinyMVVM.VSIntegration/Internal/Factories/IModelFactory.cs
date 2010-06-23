using TinyMVVM.VSIntegration.Internal.Model;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;

namespace TinyMVVM.VSIntegration.Internal.Factories
{
    public interface IModelFactory
    {
        Solution NewSolution(string path);

    	Folder NewFolder(string name, Folder parentFolder);

    	File NewFile(string name, Folder parentFolder);
    }
}
