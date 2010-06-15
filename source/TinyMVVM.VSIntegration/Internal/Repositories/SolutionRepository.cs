using System;
using System.Collections.Generic;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Factories;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Repositories
{
    internal class SolutionRepository 
    {
        private EnvDTE.DTE dte;
        private IModelFactory factory;

        public SolutionRepository(EnvDTE.DTE dte, IModelFactory factory)
        {
            this.factory = factory;
            this.dte = dte;
        }

        public Solution Get(string solutionFilePath)
        {
            var solution = factory.NewSolution();
            solution.Path = dte.Solution.FullName;

            foreach (var proj in solution.Projects)
            {
                var project = factory.NewProject();
                project.Path = proj.Path;
                proj.Name = proj.Name;
            }

            return solution;
        }
    }
}
