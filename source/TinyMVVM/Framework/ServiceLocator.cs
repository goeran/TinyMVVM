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
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;

namespace TinyMVVM.Framework
{
    public class ServiceLocator
    {
        private static IServiceLocator instance = null;

        public static IServiceLocator Instance
        {
            get{ return instance; }
        }

        private ServiceLocator()
        {
        }

        static ServiceLocator()
        {
        }

		public static IServiceLocator GetServiceLocator()
		{
			IServiceLocator serviceLocator = null;

			var serviceLocators = Assembly.GetCallingAssembly().GetTypes().
					Where(t => t.GetInterfaces().Where(i => i == typeof(IServiceLocator)).Count() > 0).ToList();

			if (serviceLocators.Count > 0)
			{
				serviceLocator = Activator.CreateInstance(serviceLocators.First()) as IServiceLocator;
			}
			else
			{
				serviceLocator = new DefaultServiceLocator();
			}

			return serviceLocator;
		}

        public static void SetLocator(IServiceLocator locator)
        {
            if (locator == null)
                throw new ArgumentNullException();

            instance = locator;
        }

        public class DefaultServiceLocator : IServiceLocator
		{
			protected CompositionContainer container;
		    protected AggregateCatalog aggregateCatalog;

			public DefaultServiceLocator()
			{
                aggregateCatalog = new AggregateCatalog();

                container = new CompositionContainer(aggregateCatalog);
			}

			public virtual T GetInstance<T>() where T : class
			{
			    return container.GetExportedValues<T>().FirstOrDefault();
			}
		}

    	public static void SetLocatorIfNotSet(Func<IServiceLocator> func)
    	{
			if (func == null)
				throw new ArgumentNullException();

			if (instance == null)
				instance = func.Invoke();
    	}

		public static void Reset()
		{
			instance = null;
		}
    }
}
