#region Copyright
// <copyright>
//  This library is free software; you can redistribute it and/or
//  modify it under the terms of the GNU Lesser General Public
//  License as published by the Free Software Foundation; either
//  version 2.1 of the License, or (at your option) any later version.
//  
//  This library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//  Lesser General Public License for more details.
//  
//  You should have received a copy of the GNU Lesser General Public
//  License along with this library; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
// </copyright> 
// 
// <contactinfo>
//  The project webpage is located at http://tinymvvm.googlecode.com which contains all the  neccessary information. You might also find more information on Gøran's blog:  http://blog.goeran.no.
// </contactinfo>
// 
// <author>Gøran Hansen</author>
// <email>mail@goeran.no</email>
// <date>2010-02-01</date>
// 
#endregion

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
            return p.PropertyType.GetInterface(typeof (ICommand).FullName, true) != null;
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
