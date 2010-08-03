using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using TinyMVVM.DSL.Framework;
using TinyMVVM.DSL.TextParser;
using TinyMVVM.VSIntegration.Internal.Conventions;
using TinyMVVM.VSIntegration.Internal.Factories;
using TinyMVVM.VSIntegration.Internal.Model.Internal;
using TinyMVVM.VSIntegration.Internal.Services.Impl;
using VSLangProj80;
using File = TinyMVVM.VSIntegration.Internal.Model.VsSolution.File;
using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;
using IVsGeneratorProgress = Microsoft.VisualStudio.Shell.Interop.IVsGeneratorProgress;
using IVsSingleFileGenerator = Microsoft.VisualStudio.Shell.Interop.IVsSingleFileGenerator;
using CodeGeneratorConfig = TinyMVVM.DSL.CodeGeneratorConfig;

namespace TinyMVVM.VSIntegration
{
    [ComVisible(true)]
    [Guid("7A1FEC42-0F41-4B81-955F-87878ECDC30E")]
    [CodeGeneratorRegistration(
        typeof(MVVMGenerator), 
        "TinyMVVM DSL Generator",
        vsContextGuids.vsContextGuidVCSProject,
        GeneratesDesignTimeSource = true
    )]
    [ProvideObject(typeof(MVVMGenerator))]
    public class MVVMGenerator : IVsSingleFileGenerator, IObjectWithSite
    {
        private Object site;
        private ServiceProvider serviceProvider;
        private const string defaultExtension = ".log";
    	private const string statusBarStringTemplate = "Model-View-ViewModel DSL: {0}";

        public int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = defaultExtension;

            return VSConstants.S_OK;
        }

        public int Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace, IntPtr[] rgbOutputFileContents, out uint pcbOutput, IVsGeneratorProgress pGenerateProgress)
        {
            pcbOutput = 0;

			var t4CodeGenerator = new T4CodeGeneratorService();
			var statusBarService = new VsStatusBarService();

            var projectItem = GetService(typeof (EnvDTE.ProjectItem)) as EnvDTE.ProjectItem;
            var dte = projectItem.DTE;

            var factory = new VsIntegrationModelFactory(dte);
            var solution = factory.NewSolution(dte.FullName);

			statusBarService.SetText(string.Format(statusBarStringTemplate, "Code generation begins"));

			var mvvmParser = new Parser();
			var mvvmSpec = mvvmParser.Parse(Code.Inline(bstrInputFileContents));

        	var mvvmFile = TreeWalker.FindFileInSolution(solution, wszInputFilePath);

            var mvvmConfig = new HashSet<string>();

            var mvvmConfigFileName = string.Format("{0}.conf", mvvmFile.Name);
            if (mvvmFile.Parent.HasFile(mvvmConfigFileName))
            {
                var codeGenConfigDslParser = new CodeGeneratorConfig.Parser();
                mvvmConfig = codeGenConfigDslParser.Parse(Code.FromFile(Path.Combine(mvvmFile.DirectoryPath, mvvmConfigFileName)));
            }

			statusBarService.SetText(string.Format(statusBarStringTemplate, "Generating ViewModels"));
			var viewModelsConvention = new GeneratedViewModelsConvention(t4CodeGenerator);
			viewModelsConvention.Apply(mvvmSpec, mvvmFile);

            if (mvvmConfig.Contains("partial viewmodels"))
            {
                statusBarService.SetText(string.Format(statusBarStringTemplate, "Generating Partial classes for ViewModels"));
                var partialViewModelsConvention = new PartialViewModelsConvention(t4CodeGenerator);
                partialViewModelsConvention.Apply(mvvmSpec, mvvmFile);
            }

            if (mvvmConfig.Contains("views"))
            {
                statusBarService.SetText(string.Format(statusBarStringTemplate, "Generating Views for ViewModels"));
                var viewsConvention = new ViewsConvention(t4CodeGenerator, factory);
                viewsConvention.Apply(mvvmSpec, mvvmFile);
            }

            if (mvvmConfig.Contains("controllers"))
            {
                statusBarService.SetText(string.Format(statusBarStringTemplate, "Generating Controllers for ViewModels"));
                var controllersConvention = new ControllersConvention(t4CodeGenerator, factory);
                controllersConvention.Apply(mvvmSpec, mvvmFile);
            }

            if (mvvmConfig.Contains("tests"))
            {
                statusBarService.SetText(string.Format(statusBarStringTemplate, "Generating Unit Tests for ViewModels"));
                var unitTestsConvention = new UnitTestsConvention(t4CodeGenerator, factory);
                unitTestsConvention.Apply(mvvmSpec, mvvmFile);
            }

			statusBarService.SetText(string.Format(statusBarStringTemplate, "Generating ViewModels Completed :)"));

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
