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

        public DependencyConfigScopingSemantics To<T>()
        {
            dependencyBinding.ToType = typeof (T);
            return new DependencyConfigScopingSemantics(dependencyBinding);
        }

        public DependencyConfigScopingSemantics ToInstance(Object instnace)
        {
            dependencyBinding.ToInstance = instnace;
            return new DependencyConfigScopingSemantics(dependencyBinding);
        }
    }

    public class DependencyConfigScopingSemantics
    {
        private readonly DependencyBinding dependencyBinding;

        public DependencyConfigScopingSemantics(DependencyBinding dependencyBinding)
        {
            this.dependencyBinding = dependencyBinding;
        }

        public void InSingletonScope()
        {
            dependencyBinding.ObjectScope = ObjectScopeEnum.Singleton;
        }

        public void InTransientScope()
        {
            dependencyBinding.ObjectScope = ObjectScopeEnum.Transient;
        }
    }
}
