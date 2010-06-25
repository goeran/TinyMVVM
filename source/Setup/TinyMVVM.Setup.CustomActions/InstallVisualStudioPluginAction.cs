using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace TinyMVVM.Setup.CustomActions
{
	[RunInstaller(true)]
	public partial class InstallVisualStudioPluginAction : System.Configuration.Install.Installer
	{
		public InstallVisualStudioPluginAction()
		{
			InitializeComponent();
		
		}

		protected override void OnCommitted(IDictionary savedState)
		{
			base.OnCommitted(savedState);

			InstallTinyMVVMVisualStudioPlugin();
		}

		private void InstallTinyMVVMVisualStudioPlugin()
		{
			var vsPluginInstallerPath = Path.Combine(Context.Parameters["TARGETDIR"], "VsIntegration", "TinyMVVM.VSIntegration.vsix");

			Process.Start(vsPluginInstallerPath);
		}
	}
}
