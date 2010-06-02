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
using Ninject;
using Ninject.Modules;
using TinyMVVM.Framework.Conventions;
using System.Collections.Generic;
using TinyMVVM.Framework.Services;

namespace TinyMVVM.Framework
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private bool sharedModuleLoaded = false;
        private IKernel kernel = new StandardKernel();
    	private readonly List<IViewModelConvention> appliedConventions = new List<IViewModelConvention>();
        private readonly List<Type> controllersToCreate = new List<Type>();
        private readonly List<Object> controllers = new List<object>();

		public event PropertyChangedEventHandler PropertyChanged;

        [Display(AutoGenerateField = false)]
        [Editable(false)]
        public static INinjectModule SharedNinjectModule { get; set; }

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

        protected void DescribeControllerToBeCreated(Type typeToBeCreated)
        {
            if (typeToBeCreated == null) throw new ArgumentNullException();

            TryLoadSharedNinjectModuleIntoKernel();

            kernel.Bind(this.GetType()).ToConstant(this);

            try
            {
                kernel.Bind(typeToBeCreated).ToSelf();
                controllers.Add(kernel.Get(typeToBeCreated));
            }
            catch (Exception ex)
            {
                throw new ViewModelException("Dependencies for Controller was not found. Add dependencies using the SharedNinjectModule static property. See inner Exception for more info", ex);
            }
        }

        private void TryLoadSharedNinjectModuleIntoKernel()
        {
            if (IsSharedNinjectModuleSpecified() && !IsSharedNinjectModuleLoaded())
            {
                kernel.Load(SharedNinjectModule);
                sharedModuleLoaded = true;
            }
        }

        private bool IsSharedNinjectModuleSpecified()
        {
            return SharedNinjectModule != null;
        }

        private bool IsSharedNinjectModuleLoaded()
        {
            return sharedModuleLoaded == true;
        }
    }
}
