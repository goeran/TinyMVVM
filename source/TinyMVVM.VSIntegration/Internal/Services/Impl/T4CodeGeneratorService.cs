using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.VSIntegration.Internal.Templates;
using File = TinyMVVM.VSIntegration.Internal.Model.VsSolution.File;

namespace TinyMVVM.VSIntegration.Internal.Services.Impl
{
	public class T4CodeGeneratorService : ICodeGeneratorService

	{
		public void Generate(File inputFile, File outputFile, CodeGeneratorArgs args)
		{
			var iTextTemplating = Package.GetGlobalService(typeof(STextTemplating)) as ITextTemplating;

			var t4Template = args.Template.Content;

			//TODO: Find a more optimal solution than concat string like this
			t4Template = t4Template.Replace("$(Code)", args.ModelSpecification.Code);
			t4Template = t4Template.Replace("$(MvvmFilePath)", inputFile.Path);

			t4Template = t4Template.Replace("$(TinyMVVMDir)", GetTinyMVVMInstallDirPath());
			t4Template = t4Template.Replace("$(ViewModel.Name)", args.ViewModel.Name);
			t4Template = t4Template.Replace("$(CurrentNamespace)", outputFile.Parent.CurrentNamespace);
			var content = iTextTemplating.ProcessTemplate(outputFile.Path, t4Template, null, null);

			using (var fs = outputFile.NewFileStream())
			{
				fs.Write(content);
			}

		}

	    private string GetTinyMVVMInstallDirPath()
	    {
	        return Path.Combine(
	            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
	            "TinyMVVM",
	            "bin",
	            ".net4");
	    }
	}
}
