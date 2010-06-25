using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace TinyMVVM.VSIntegration.Internal.Services.Impl
{
	public class VsStatusBarService : IStatusBarService
	{
		private IVsStatusbar vsStatusbar;

		public VsStatusBarService()
		{
			vsStatusbar = Package.GetGlobalService(typeof (SVsStatusbar)) as IVsStatusbar;
		}

		public void SetText(string text)
		{
			vsStatusbar.SetText(text);
		}
	}
}
