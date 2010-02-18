using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TinyMVVM.SemanticModel
{
    public class ViewModel
    {
        private List<ViewModelData> _data;
        private List<ViewModelCommand> _commands;

        public string Name { get; protected set; }
        
        public ReadOnlyCollection<ViewModelData> Data 
        {
            get { return _data.AsReadOnly(); } 
        }

        public ReadOnlyCollection<ViewModelCommand> Commands
        {
            get { return _commands.AsReadOnly(); }
        }

        public ViewModel(string name)
        {
            ThrowExceptionIfNull(name, "name");

            _data = new List<ViewModelData>();
            _commands = new List<ViewModelCommand>();

            Name = name;
        }

        public void AddViewModelData(ViewModelData data)
        {
            ThrowExceptionIfNull(data, "data");

            _data.Add(data);
        }

        private void ThrowExceptionIfNull(object obj, string name)
        {
            if (obj == null)
                throw new ArgumentNullException(name);
        }

        public void AddViewModelCommand(ViewModelCommand command)
        {
            ThrowExceptionIfNull(command, "command");

            _commands.Add(command);
        }
    }
}
