using System.Collections.Generic;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model.Internal;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Model
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
        public virtual string Path { get; set; }

        public Project Project
        {
            get
            {
                return TreeWalker.FindProject(this);
            }
        }
    }
}
