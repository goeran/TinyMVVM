using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TinyMVVM.Framework
{
    public class Bootstrapper
    {
        public static IBootstrapper FromProject
        {
            get
            {
                var bootstrappers = Assembly.GetCallingAssembly().GetTypes().
                    Where(t => t.GetInterfaces().Where(i => i == typeof(IBootstrapper)).Count() > 0).ToList();
                if (bootstrappers.Count == 0)
                    throw new Exception("No Bootstrapper found in Project");
                return Activator.CreateInstance(bootstrappers.First()) as IBootstrapper;
            }
        }

        private Bootstrapper()
        {
            
        }
    }
}
