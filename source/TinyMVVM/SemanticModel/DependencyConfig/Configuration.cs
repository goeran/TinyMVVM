using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyMVVM.SemanticModel.DependencyConfig
{
    public class Configuration
    {
        public List<DependencyBinding> Bindings { get; private set; }
    	public bool MergeInGlobalDependenciesConfig { get; set; }

        public Configuration()
        {
            Bindings = new List<DependencyBinding>();
        }
    }
}
