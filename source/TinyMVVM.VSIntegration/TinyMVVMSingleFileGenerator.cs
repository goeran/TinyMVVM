using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using VSLangProj80;

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
    public class TinyMVVMSingleFileGenerator : IVsSingleFileGenerator
    {
        public int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".mvvm";

            return VSConstants.S_OK;
        }

        public int Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace, IntPtr[] rgbOutputFileContents, out uint pcbOutput, IVsGeneratorProgress pGenerateProgress)
        {
            pcbOutput = 1;

            return VSConstants.S_OK;
        }
    }
}
