using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Model.Internal
{
    internal class TreeWalker
    {
        public static Project FindProject(ProjectItem item)
        {
            if (item.GetType() == typeof(Project))
                return item as Project;
            else if (item.Parent == null)
                return null;
            else if (item.Parent.GetType() == typeof(Project))
                return item.Parent as Project;
            else
                return FindProject(item.Parent);
        }
    }
}
