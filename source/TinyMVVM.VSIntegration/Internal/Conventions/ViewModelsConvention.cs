using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMVVM.SemanticModel.MVVM;
using TinyMVVM.TinyMVVM_VSIntegration.Internal.Model;

namespace TinyMVVM.TinyMVVM_VSIntegration.Internal.Conventions
{
    public class ViewModelsConvention : IViewModelConvention
    {
        public void Apply(ModelSpecification viewModelSpecification, File mvvmFile)
        {
            foreach (var viewModel in viewModelSpecification.ViewModels)
            {
                var fileName = string.Format("{0}.mvvm.cs", viewModel.Name);
                if (!mvvmFile.HasCodeBehindFile(fileName))
                    mvvmFile.NewCodeBehindFile(fileName);
            }
        }
    }
}
