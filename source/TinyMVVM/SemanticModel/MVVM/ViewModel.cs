using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TinyMVVM.SemanticModel.MVVM
{
    public class ViewModel
    {
        private List<ViewModelProperty> _data;
        private List<ViewModelCommand> _commands;

        public string Name { get; protected set; }
    	public string Namespace { get; set; }
        
        public ReadOnlyCollection<ViewModelProperty> Properties 
        {
            get { return _data.AsReadOnly(); } 
        }

        public ReadOnlyCollection<ViewModelCommand> Commands
        {
            get { return _commands.AsReadOnly(); }
        }

        private string _parent;
        public string Parent
        {
            get { return _parent; } 
            set
            {
                if (value == null) throw new ArgumentNullException();

                _parent = value;
            }
        }

        public ViewModel(string name)
        {
            ThrowExceptionIfNull(name, "name");

            _data = new List<ViewModelProperty>();
            _commands = new List<ViewModelCommand>();

            Name = name;

            Parent = "TinyMVVM.Framework.ViewModelBase";
        }

        public void AddProperty(ViewModelProperty data)
        {
            ThrowExceptionIfNull(data, "data");

            _data.Add(data);
        }

        private void ThrowExceptionIfNull(object obj, string name)
        {
            if (obj == null)
                throw new ArgumentNullException(name);
        }

        public void AddCommand(ViewModelCommand command)
        {
            ThrowExceptionIfNull(command, "command");

            _commands.Add(command);
        }
    }
}
