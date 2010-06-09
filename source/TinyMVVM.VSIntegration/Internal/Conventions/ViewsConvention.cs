using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Conventions
{
    public class ViewsConvention
    {
        private Project project;

        public void ApplyConvention(ModelSpecification mvvmDefinition, Project project)
        {
            this.project = project;

            if (!project.HasFolder("Views"))
            {
                project.NewFolder("Views");
            }

            foreach (var viewModel in mvvmDefinition.ViewModels)
            {
                var viewsFolder = project.GetSubFolder("Views");
                if (viewsFolder.Files.Where(f => f.Name.Replace(".xaml", string.Empty) == viewModel.Name).Count() == 0)
                    viewsFolder.NewFile(viewModel.Name + ".xaml");
            }
        }
    }
}
