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
using System.Linq.Expressions;
using Ninject;
using Ninject.Activation;
using Ninject.Planning.Bindings;
using TinyMVVM.Framework.Conventions;
using System.Collections.Generic;
using TinyMVVM.Framework.Services;
using TinyMVVM.Framework.Services.Impl;
using TinyMVVM.SemanticModel.DependencyConfig;

namespace TinyMVVM.Framework
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private IKernel instanceKernel = new StandardKernel();
        private static IKernel globalKernel = new StandardKernel();
        private Configuration instanceDependenciesConfig = new Configuration();
        private static Configuration globalDependenciesConfig = new Configuration();

    	private readonly List<IViewModelConvention> appliedConventions = 
            new List<IViewModelConvention>();

        private readonly List<Object> controllers = new List<object>();

        private ActivationException activationException;

        protected ViewModelBase()
        {
            PropertyChangeRecorder = new PropertyChangeRecorder(this);
            CmdStateChangeRecorder = new object();

            instanceKernel.Bind(this.GetType()).ToConstant(this);
            globalKernel.Bind(this.GetType()).ToConstant(this);
        }

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

		protected virtual void TriggerPropertyChanged<T>(Expression<Func<T, Object>> exp)
		{
			string propertyName;
			if (exp.Body is UnaryExpression)
				propertyName = ((MemberExpression)((UnaryExpression)exp.Body).Operand).Member.Name;
			else
				propertyName = ((MemberExpression)exp.Body).Member.Name;

			if (PropertyChanged != null)
			{
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

        public virtual T GetDependency<T>() where T: class
        {
        	var instance = TryGetFromInstanceKernel(typeof (T));
			if (instance == null) instance = TryGetFromGlobalKernel(typeof (T));
        	return instance as T;
        }

		public virtual T GetInstance<T>() where T: class
		{
			return GetDependency<T>();
		}

        /// <summary>
        /// Create a new instance of the specified Controller inside the ViewModel. 
        /// The Controller object can be of any type. The only coupling between the 
        /// ViewModel and the Controller is that the ViewModel must know of the 
        /// type of Controller that’s about to be created. The ViewModel doesn’t 
        /// have to call anything on it. The Controlelr object will have the same 
        /// life span as the ViewModel object.
        /// 
        /// If the controller has any dependencies, they will be automatically 
        /// injected. In order to inject dependencies you’ll have to configure 
        /// them first. Dependencies are injected through the constructor on 
        /// the Controller object. The ViewModel instance itself will be 
        /// automatically configured as a dependency.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public T CreateController<T>()
        {
            try
            {
                object controller = TryCreateController(typeof(T));

                if (controller == null) throw activationException;

                controllers.Add(controller);

            	return (T) controller;
            }
            catch (Exception ex)
            {
                throw new ViewModelException("Dependencies for Controller was not found. Add dependencies using the SharedNinjectModule static property. See inner Exception for more info", ex);
            }
        }

        private object TryCreateController(Type typeToBeCreated)
        {
            Object controller = null;

            controller = TryGetFromInstanceKernel(typeToBeCreated);

            if (controller == null)
            {
                controller = TryGetFromGlobalKernel(typeToBeCreated);
            }

            return controller;
        }

        private object TryGetFromInstanceKernel(Type typeToBeCreated)
        {
            return TryGetObjectFromKernel(typeToBeCreated, instanceKernel);
        }

        private object TryGetObjectFromKernel(Type typeToBeCreated, IKernel kernel)
        {
            Object result = null;

            try
            {
                result = kernel.Get(typeToBeCreated);
            }
            catch (ActivationException ex)
            {
                activationException = ex;
            }

            return result;
        }

        private object TryGetFromGlobalKernel(Type typeToBeCreated)
        {
            return TryGetObjectFromKernel(typeToBeCreated, globalKernel);
        }

        public void ConfigureDependencies(Action<DependencyConfigSemantics> configAction)
        {
            configAction.Invoke(new DependencyConfigSemantics(instanceDependenciesConfig));

			ConfigureKernel(instanceKernel, instanceDependenciesConfig);

			if (instanceDependenciesConfig.MergeInGlobalDependenciesConfig)
			{
				ConfigureKernel(instanceKernel, globalDependenciesConfig);
			}
        }

        public static void ConfigureGlobalDependencies(Action<DependencyConfigSemantics> configAction)
        {
        	configAction.Invoke(new DependencyConfigSemantics(globalDependenciesConfig));

        	ConfigureKernel(globalKernel, globalDependenciesConfig);
        }

    	private static void ConfigureKernel(IKernel kernel, Configuration configuration)
    	{
    		foreach (var dependencyBinding in configuration.Bindings)
    		{
    			if (dependencyBinding.ToInstance != null)
    			{
    				if (dependencyBinding.ObjectScope == ObjectScopeEnum.Singleton)
    					kernel.Bind(dependencyBinding.FromType).ToConstant(dependencyBinding.ToInstance).InSingletonScope();
    				else
    					kernel.Bind(dependencyBinding.FromType).ToConstant(dependencyBinding.ToInstance).InTransientScope();
    			}
    			else
    			{
    				if (dependencyBinding.ObjectScope == ObjectScopeEnum.Singleton)
    					kernel.Bind(dependencyBinding.FromType).To(dependencyBinding.ToType).InSingletonScope();
    				else
    					kernel.Bind(dependencyBinding.FromType).To(dependencyBinding.ToType);
    			}
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
