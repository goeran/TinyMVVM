using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TinyMVVM.SemanticModel.MVVM
{
    public class ModelSpecification
    {
        private List<ViewModel> viewModels = new List<ViewModel>();


        public ModelSpecification()
        {
            Usings = new List<string>();
        }

    	public string Code { get; set; }
		public List<string> Usings { get; protected set; }

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
