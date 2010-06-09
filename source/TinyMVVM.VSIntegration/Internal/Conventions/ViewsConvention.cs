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

            if (!ViewsFolderExists())
            {
                AddViewsFolder();
            }

            foreach (var viewModel in mvvmDefinition.ViewModels)
            {
                var viewsFolder = project.Folders.Where(f => f.Name == "Views").Single();
                if (viewsFolder.Files.Where(f => f.Name.Replace(".xaml", string.Empty) == viewModel.Name).Count() == 0)
                    viewsFolder.Items.Add(new File(){ Name = viewModel.Name + ".xaml" });
            }
        }

        private void AddViewsFolder()
        {
            project.Items.Add(new Folder()
            {
                Name = "Views"
            });
        }

        private bool ViewsFolderExists()
        {
            ProjectItem viewsFolder = TryGetViewsFolder();

            return viewsFolder != null;
        }

        private ProjectItem TryGetViewsFolder()
        {
            return project.Items.Where(i => i is Folder && i.Name == "Views").SingleOrDefault();
        }
    }
}
