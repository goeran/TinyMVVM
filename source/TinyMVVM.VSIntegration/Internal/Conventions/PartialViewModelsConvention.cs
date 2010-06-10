using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Conventions
{
    public class PartialViewModelsConvention
    {
        public void ApplyToProject(ModelSpecification mvvmDefinition, Project project)
        {
            var viewModelFolder = project.GetSubFolder("ViewModel");

            foreach (var viewModel in mvvmDefinition.ViewModels)
            {
                var name = string.Format("{0}.cs", viewModel.Name);
                if (!viewModelFolder.HasFile(name))
                    viewModelFolder.NewFile(name);    
            }
        }
    }
}
