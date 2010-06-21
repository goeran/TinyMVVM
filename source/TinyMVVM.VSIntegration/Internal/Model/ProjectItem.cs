using System.Collections.Generic;
using TinyMVVM.VSIntegration.Internal.Model.Internal;

namespace TinyMVVM.VSIntegration.Internal.Model
{
    public class ProjectItem
    {
        protected ProjectItem()
        {
            Items = new List<ProjectItem>();
        }

        public virtual List<ProjectItem> Items { get; private set; }
        public virtual string Name { get; set; }
        public virtual Folder Parent { get; internal set; }
        public virtual string DirectoryPath { get; set; }
        public virtual string Path
        { 
            get
            {
                return System.IO.Path.Combine(DirectoryPath, Name);        
            }
        }

        public Project Project
        {
            get
            {
                return TreeWalker.FindProject(this);
            }
        }
    }
}
