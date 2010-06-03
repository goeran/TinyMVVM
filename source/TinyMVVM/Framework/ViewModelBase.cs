#region Copyright
// <copyright>
//  This library is free software; you can redistribute it and/or
//  modify it under the terms of the GNU Lesser General Public
//  License as published by the Free Software Foundation; either
//  version 2.1 of the License, or (at your option) any later version.
//  
//  This library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//  Lesser General Public License for more details.
//  
//  You should have received a copy of the GNU Lesser General Public
//  License along with this library; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
// </copyright> 
// 
// <contactinfo>
//  The project webpage is located at http://tinymvvm.googlecode.com which contains all the  neccessary information. You might also find more information on Gøran's blog:  http://blog.goeran.no.
// </contactinfo>
// 
// <author>Gøran Hansen</author>
// <email>mail@goeran.no</email>
// <date>2010-02-01</date>
// 
#endregion

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;
using TinyMVVM.Framework.Conventions;
using System.Collections.Generic;
using TinyMVVM.Framework.Services;
using TinyMVVM.Framework.Services.Impl;
using TinyMVVM.SemanticModel.DependencyConfig;
using TinyMVVM.SemanticModel.MVVM;

namespace TinyMVVM.Framework
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private static IKernel globalKernel = new StandardKernel();
        private bool sharedModuleLoaded = false;
        private IKernel instanceKernel = new StandardKernel();
    	private readonly List<IViewModelConvention> appliedConventions = new List<IViewModelConvention>();
        private readonly List<Object> controllers = new List<object>();
        private ActivationException activationException;
        private Configuration instanceDependenciesConfig = new Configuration();
        private static Configuration globalDependenciesConfig = new Configuration();

		public event PropertyChangedEventHandler PropertyChanged;

        [Display(AutoGenerateField = false)]
        [Editable(false)]
        protected IUIInvoker UIInvoker
        {
            get
            {
                var uiInvokerBinding = instanceDependenciesConfig.Bindings.Where(b => b.FromType == typeof(IUIInvoker)).FirstOrDefault();
                if (uiInvokerBinding != null)
                    return instanceKernel.Get<IUIInvoker>();

                var uiInvokerBindingInGlobal = globalDependenciesConfig.Bindings.Where(b => b.FromType == typeof(IUIInvoker)).FirstOrDefault();
                if (uiInvokerBindingInGlobal != null)
                    return globalKernel.Get<IUIInvoker>();

                ConfigureDependencies(config =>
                    config.Bind<IUIInvoker>().To<UIInvoker>());

                return instanceKernel.Get<IUIInvoker>();
            }
        }

        [Display(AutoGenerateField = false)]
        [Editable(false)]
		public PropertyChangeRecorder PropertyChangeRecorder { get; protected set; }

        [Display(AutoGenerateField = false)]
        [Editable(false)]
        public Object CmdStateChangeRecorder { get; protected set; }

        [Display(AutoGenerateField = false)]
        [Editable(false)]
        protected ReadOnlyCollection<IViewModelConvention> AppliedConventions
		{
			get { return new ReadOnlyCollection<IViewModelConvention>(appliedConventions); }
		}

        [Display(AutoGenerateField = false)]
        [Editable(false)]
        public ReadOnlyCollection<Object> Controllers
        {
            get
            {
                return new ReadOnlyCollection<Object>(controllers);
            }
        }

    	protected ViewModelBase()
        {
            PropertyChangeRecorder = new PropertyChangeRecorder(this);
            CmdStateChangeRecorder = new object();

            instanceKernel.Bind(this.GetType()).ToConstant(this);
    	    globalKernel.Bind(this.GetType()).ToConstant(this);
        }

    	protected void ApplyDefaultConventions()
    	{
			ApplyConvention(new InvokeOnInitialize());
			ApplyConvention(new BindCommandsDelegatesToMethods());
    	}

    	public void ApplyConvention(IViewModelConvention convention)
		{
			if (convention == null) throw new ArgumentNullException();

			convention.ApplyTo(this);
			appliedConventions.Add(convention);
		}

        protected virtual void TriggerPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual T GetInstance<T>() where T: class
        {
            return ServiceLocator.Instance.GetInstance<T>();
        }

        public void RegisterController<T>()
        {
            var typeToBeCreated = typeof(T);

            try
            {
                instanceKernel.Bind(typeToBeCreated).ToSelf();

                Object controller = null;

                controller = TryGetFromInstanceKernel(typeToBeCreated);
                if (controller == null)
                {
                    controller = TryGetFromGlobalKernel(typeToBeCreated);
                }

                if (controller == null) throw activationException;

                controllers.Add(controller);
            }
            catch (Exception ex)
            {
                throw new ViewModelException("Dependencies for Controller was not found. Add dependencies using the SharedNinjectModule static property. See inner Exception for more info", ex);
            }
        }

        private object TryGetFromInstanceKernel(Type typeToBeCreated)
        {
            Object result = null;
            try
            {
                result = instanceKernel.Get(typeToBeCreated);
            }
            catch (ActivationException ex)
            {
                activationException = ex;
            }

            return result;
        }

        private object TryGetFromGlobalKernel(Type typeToBeCreated)
        {
            Object result = null;
            try
            {
                result = globalKernel.Get(typeToBeCreated);
            }
            catch (ActivationException ex)
            {
                activationException = ex;
            }

            return result;
        }

        public void ConfigureDependencies(Action<DependencyConfigSemantics> configAction)
        {
            configAction.Invoke(new DependencyConfigSemantics(instanceDependenciesConfig));

            foreach (var dependencyBinding in instanceDependenciesConfig.Bindings)
            {
                if (dependencyBinding.ToInstance != null)
                    instanceKernel.Bind(dependencyBinding.FromType).ToConstant(dependencyBinding.ToInstance);
                else
                    instanceKernel.Bind(dependencyBinding.FromType).To(dependencyBinding.ToType);
            }
        }

        public static void ConfigureGlobalDependencies(Action<DependencyConfigSemantics> configAction)
        {
            configAction.Invoke(new DependencyConfigSemantics(globalDependenciesConfig));

            foreach (var dependencyBinding in globalDependenciesConfig.Bindings)
            {
                //var name = MethodInfo.GetCurrentMethod().DeclaringType.FullName;
                if (dependencyBinding.ToInstance != null)
                    globalKernel.Bind(dependencyBinding.FromType).ToConstant(dependencyBinding.ToInstance).InSingletonScope();
                else
                    globalKernel.Bind(dependencyBinding.FromType).To(dependencyBinding.ToType).InSingletonScope();
            }
        }

        public static void RemoveAllGlobalDependencies()
        {
            globalDependenciesConfig.Bindings.Clear();
            globalKernel.Dispose();
            globalKernel = new StandardKernel();
        }
    }
}
