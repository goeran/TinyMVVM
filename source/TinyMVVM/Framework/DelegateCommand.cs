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
        private Func<bool> canExecuteDelegate;

        public DelegateCommand(Action executeDelegate) :
            this(executeDelegate, () => true)
        {
        }

        public DelegateCommand(Action executeDelegate, Func<bool> canExecuteDelegate)
        {
            if (executeDelegate == null || canExecuteDelegate == null)
                throw new ArgumentNullException();

            this.executeDelegate = executeDelegate;
            this.canExecuteDelegate = canExecuteDelegate;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
                executeDelegate();
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteDelegate();
        }

        public event EventHandler CanExecuteChanged;
    }
}
