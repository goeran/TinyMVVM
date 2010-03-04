using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Input;

namespace TinyMVVM.Framework
{
    public class CommandStateChangeRecorder
    {
        private bool observingCommandsFlag = false;
        private object viewModel;
        private List<Record> data = new List<Record>();
        private List<ICommand> publicCommands = new List<ICommand>();

        public ReadOnlyCollection<Record> Data
        {
            get
            {
                return new ReadOnlyCollection<Record>(data);
            }
        }

        public CommandStateChangeRecorder(Object viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException("viewModel");

            this.viewModel = viewModel;

            FindPublicCommands();
        }

        private void FindPublicCommands()
        {
            var vmType = viewModel.GetType();
            var publicProperties = vmType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var commandProperties = publicProperties.
                Where(p => IsPropertyTypeAnImplOfCommand(p)).
                Select(p => p.GetValue(viewModel, null)).
                Cast<ICommand>().ToList();

            publicCommands.AddRange(commandProperties.Where(i => i != null));
        }
            
        private bool IsPropertyTypeAnImplOfCommand(PropertyInfo p)
        {
            return p.PropertyType.GetInterface(typeof (ICommand).FullName) != null;
        }

        public void Start()
        {
            if (!IsObservingCommands())
                StartObservingCommands();
        }

        private bool IsObservingCommands()
        {
            return observingCommandsFlag;
        }

        private void StartObservingCommands()
        {
            foreach (var command in publicCommands)
            {
                command.CanExecuteChanged += command_CanExecuteChanged;
            }
            observingCommandsFlag = true;
        }

        void command_CanExecuteChanged(object sender, EventArgs e)
        {
            var command = sender as ICommand;
            data.Add(new Record(command, command.CanExecute(null)));
        }

        public class Record
        {
            public ICommand Command { get; private set; }
            public bool CanExecute { get; private set; }

            public Record(ICommand command, bool canExecute)
            {
                Command = command;
                CanExecute = canExecute;
            }
        }
    }
}
