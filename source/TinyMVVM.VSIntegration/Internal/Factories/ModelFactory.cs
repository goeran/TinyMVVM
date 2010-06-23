using System;
using TinyMVVM.VSIntegration.Internal.Model;
using TinyMVVM.VSIntegration.Internal.Model.VsSolution;

namespace TinyMVVM.VSIntegration.Internal.Factories
{
    public class ModelFactory : IModelFactory
    {
        public Solution NewSolution(string path)
        {
            return new Solution(path);
        }

		public Folder NewFolder(string name, Folder parentFolder)
		{
			return new Folder(name, parentFolder);
		}

		public File NewFile(string name, Folder parentFolder)
		{
			return new File(parentFolder){ Name = name };
		}
    }
}
