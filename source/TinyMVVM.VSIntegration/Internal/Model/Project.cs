using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Model
{
    public class Project
    {
        public virtual string Name { get; set; }
        public virtual List<ProjectItem> Items { get; private set; }
        public virtual string Type { get; set; }

        public IEnumerable<Folder> Folders
        {
            get { return Items.Where(i => i is Folder).Cast<Folder>(); }
        }

        public Project()
        {
            Items = new List<ProjectItem>();
        }
    }
}
