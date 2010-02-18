using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
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

        public static void SetLocator(IServiceLocator locator)
        {
            if (locator == null)
                throw new ArgumentNullException();

            instance = locator;
        }
    }
}
