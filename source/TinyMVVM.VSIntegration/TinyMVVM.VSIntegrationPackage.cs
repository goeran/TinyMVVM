using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using TinyMVVM.VSIntegration.Internal.Factories;

namespace TinyMVVM.VSIntegration
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the informations needed to show the this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [Guid(GuidList.guidTinyMVVM_VSIntegrationPkgString)]
    [ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids.SolutionExists)]
    public sealed class TinyMVVM_VSIntegrationPackage : Package
    {
        private EnvDTE.DTE dte;
        List<Document> docs = new List<Document>();
        List<DocumentEvents> docsEvents = new List<DocumentEvents>();

        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public TinyMVVM_VSIntegrationPackage()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }



        /////////////////////////////////////////////////////////////////////////////
        // Overriden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initilaization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Trace.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();


            //var statusbar = GetService(typeof (IVsStatusbar)) as IVsStatusbar;
            //statusbar.SetText("TinyMVVM");

            dte = GetService(typeof (EnvDTE.DTE)) as EnvDTE.DTE;

            //var factory = new VsIntegrationModelFactory(dte);
            //var solution = factory.NewSolution();

			//foreach (ProjectItem projItem in dte.Solution.Projects.Item(1).ProjectItems)
			//{
			//    var fileInfo = new FileInfo(projItem.Name);
			//    if (fileInfo.Extension == ".cs")
			//    {
			//        docs.Add(projItem.Document);
			//    }
			//}

			//docs.ForEach(d =>
			//{
			//    var events = dte.Events.get_DocumentEvents(d);
			//    docsEvents.Add(events);

			//    events.DocumentSaved += new _dispDocumentEvents_DocumentSavedEventHandler(TinyMVVM_VSIntegrationPackage_DocumentSaved);
			//});
        }

        void TinyMVVM_VSIntegrationPackage_DocumentSaved(Document Document)
        {
            //MessageBox.Show("File saved!");
        }
        #endregion

    }
}
