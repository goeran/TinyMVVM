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
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace TinyMVVM.Framework
{
    public class DelegateCommand : ICommand
    {
		private Action executeDelegate;

		public DelegateCommand() :
			this(() => { }, () => true)
		{
		}

    	public Action ExecuteDelegate
    	{
    		get { return executeDelegate; } 
			set
			{
				if (value == null) throw new ArgumentNullException();

				executeDelegate = value;
			}
    	}

    	private Func<bool> canExecuteDelegate;
    	public Func<bool> CanExecuteDelegate
    	{
    		get { return canExecuteDelegate; } 
			set
			{
				if (value == null) throw new ArgumentNullException();

				canExecuteDelegate = value;
			}
    	}

        public DelegateCommand(Action executeDelegate) :
            this(executeDelegate, () => true)
        {
        }

        public DelegateCommand(Action executeDelegate, Func<bool> canExecuteDelegate)
        {
            if (executeDelegate == null || canExecuteDelegate == null)
                throw new ArgumentNullException();

            ExecuteDelegate = executeDelegate;
            CanExecuteDelegate = canExecuteDelegate;
        }

        public void Execute()
        {
            Execute(null);
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
                ExecuteDelegate();
        }

        public bool CanExecute()
        {
            return CanExecute(null);
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteDelegate();
        }

        public void TriggerCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
}
