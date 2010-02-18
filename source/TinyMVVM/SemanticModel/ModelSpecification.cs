using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TinyMVVM.SemanticModel
{
    public class ModelSpecification
    {
        private List<ViewModel> viewModels = new List<ViewModel>();

        public ReadOnlyCollection<ViewModel> ViewModels
        {
            get { return viewModels.AsReadOnly(); }
        }

        public void AddViewModel(ViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException();

            viewModels.Add(viewModel);
        }
    }
}
