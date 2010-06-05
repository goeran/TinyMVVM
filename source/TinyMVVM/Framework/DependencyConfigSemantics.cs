using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.SemanticModel.DependencyConfig;

namespace TinyMVVM.Framework
{
    public class DependencyConfigSemantics
    {
        private Configuration semanticModel;

        public DependencyConfigSemantics(Configuration semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        public DependencyConfigToSemantics Bind<T>()
        {
            var dependencyBinding = new DependencyBinding()
            {
                FromType = typeof (T)
            };
            semanticModel.Bindings.Add(dependencyBinding);

            return new DependencyConfigToSemantics(dependencyBinding);
        }
    }

    public class DependencyConfigToSemantics
    {
        private readonly DependencyBinding dependencyBinding;

        public DependencyConfigToSemantics(DependencyBinding dependencyBinding)
        {
            this.dependencyBinding = dependencyBinding;
        }

        public void To<T>()
        {
            dependencyBinding.ToType = typeof (T);
        }

        public void ToInstance(Object instnace)
        {
            dependencyBinding.ToInstance = instnace;
        }
    }
}
