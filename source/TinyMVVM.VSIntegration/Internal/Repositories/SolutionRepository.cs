using System;
using System.Collections.Generic;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Factories;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Repositories
{
    internal class SolutionRepository 
    {
        private IModelFactory factory;

        public SolutionRepository(IModelFactory factory)
        {
            this.factory = factory;
        }

        public Solution Get()
        {
            return factory.NewSolution();
        }
    }
}
