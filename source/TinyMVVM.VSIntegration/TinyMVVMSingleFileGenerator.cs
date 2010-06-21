﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using TinyMVVM.DSL.TextParser;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Conventions;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Factories;
using VSLangProj80;
using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace TinyMVVM.TinyMVVM_VSIntegration
{
    [ComVisible(true)]
    [Guid("7A1FEC42-0F41-4B81-955F-87878ECDC30E")]
    [CodeGeneratorRegistration(
        typeof(TinyMVVMSingleFileGenerator), 
        "TinyMVVM DSL Generator",
        vsContextGuids.vsContextGuidVCSProject,
        GeneratesDesignTimeSource = true
    )]
    [ProvideObject(typeof(TinyMVVMSingleFileGenerator))]
    public class TinyMVVMSingleFileGenerator : IVsSingleFileGenerator, IObjectWithSite
    {
        private Object site;
        private ServiceProvider serviceProvider;
        private const string defaultExtension = ".mvvm.cs";

        public int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = defaultExtension;

            return VSConstants.S_OK;
        }

        public int Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace, IntPtr[] rgbOutputFileContents, out uint pcbOutput, IVsGeneratorProgress pGenerateProgress)
        {
            pcbOutput = 1;

            var projectItem = GetService(typeof (EnvDTE.ProjectItem)) as EnvDTE.ProjectItem;
            var dte = projectItem.DTE;
            //var dte = GetService(typeof (EnvDTE.DTE)) as EnvDTE.DTE;

            var factory = new VsIntegrationModelFactory(dte);
            var solution = factory.NewSolution(dte.FullName);

        	var p = new Parser();
			var spec = p.Parse(Code.Inline(bstrInputFileContents));

			//TODO: grab this file based on the path (wszInputFilePath)
        	var mvvmFile = solution.Projects.First().GetSubFolder("ViewModel").GetFile("viewmodel.mvvm");

			var viewModelsConvention = new ViewModelsConvention();
			viewModelsConvention.Apply(spec, mvvmFile);

			var viewsConvention = new ViewsConvention();
			viewsConvention.Apply(spec, mvvmFile);

        	var partialViewModelsConvention = new PartialViewModelsConvention();
			partialViewModelsConvention.Apply(spec, mvvmFile);

			//TODO: create a controller convention
            
            /*var projectItem = GetService(typeof (EnvDTE.ProjectItem)) as EnvDTE.ProjectItem;
            var project = projectItem.ContainingProject;

            var dir = new FileInfo(project.FullName).Directory.FullName;

            EnvDTE.ProjectItem item = null;

            for (int i = 1; i <= project.ProjectItems.Count; i++)
            {
                var name = project.ProjectItems.Item(i).Name;
                var fileName = project.ProjectItems.Item(i).get_FileNames(0);
                
                if (name == "viewmodel.mvvm")
                {
                    item = project.ProjectItems.Item(i);
                    break;
                }
            }

            var files = new List<string>()
            {
                Path.Combine(dir, "1" + defaultExtension), 
                Path.Combine(dir, "2" + defaultExtension) 
            };

            if (item != null)
            {
                files.ForEach(f =>
                {
                    using (var newFile = File.Create(f));
                    item.ProjectItems.AddFromFile(f);
                });
            }*/
            

            /*byte[] data = null;
            using (var sw = new StringWriter())
            {
                sw.WriteLine("hello world");

                data = Encoding.UTF8.GetBytes(sw.ToString());
            }

            int outputLength = data.Length;
            rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(outputLength);
            Marshal.Copy(data, 0, rgbOutputFileContents[0], outputLength);
            Marshal.Copy(data, 0, rgbOutputFileContents[1], outputLength);
            pcbOutput = (uint)outputLength;*/;
            
            return VSConstants.S_OK;
        }

        public void SetSite(object pUnkSite)
        {
            site = pUnkSite;
            serviceProvider = null;
        }

        public void GetSite(ref Guid riid, out IntPtr ppvSite)
        {
            if (site == null)
            {
                throw new COMException("object is not sited", VSConstants.E_FAIL);
            }

            IntPtr pUnknownPointer = Marshal.GetIUnknownForObject(site);
            IntPtr intPointer = IntPtr.Zero;
            Marshal.QueryInterface(pUnknownPointer, ref riid, out intPointer);

            if (intPointer == IntPtr.Zero)
            {
                throw new COMException("site does not support requested interface", VSConstants.E_NOINTERFACE);
            }

            ppvSite = intPointer;
        }

        /// <summary>
        /// Demand-creates a ServiceProvider
        /// </summary>
        private ServiceProvider SiteServiceProvider
        {
            get
            {
                if (serviceProvider == null)
                {
                    serviceProvider = new ServiceProvider(site as IServiceProvider);
                    Debug.Assert(serviceProvider != null, "Unable to get ServiceProvider from site object.");
                }
                return serviceProvider;
            }
        }

        /// <summary>
        /// Method to get a service by its Type
        /// </summary>
        /// <param name="serviceType">Type of service to retrieve</param>
        /// <returns>An object that implements the requested service</returns>
        protected object GetService(Type serviceType)
        {
            return SiteServiceProvider.GetService(serviceType);
        }
    }
}
