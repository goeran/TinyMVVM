using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Model
{
    public class Solution
    {
        public virtual string Name { get; set; }
        public virtual List<Project> Projects { get; private set; }
    
        public Solution()
        {
            Projects = new List<Project>();
        }
    }
}
