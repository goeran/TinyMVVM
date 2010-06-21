using System;
using TinyMVVM.VSIntegration.Internal.Model;

namespace TinyMVVM.VSIntegration.Internal.Factories
{
    public class ModelFactory : IModelFactory
    {
        public Solution NewSolution(string path)
        {
            return new Solution(path);
        }
    }
}
