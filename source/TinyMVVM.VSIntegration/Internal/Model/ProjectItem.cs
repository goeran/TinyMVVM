using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model.Internal;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Model
{
    public class ProjectItem
    {
        public virtual string Name { get; set; }
        public virtual Folder Parent { get; internal set; }

        public Project Project
        {
            get
            {
                return TreeWalker.FindProject(this);
            }
        }
    }
}
