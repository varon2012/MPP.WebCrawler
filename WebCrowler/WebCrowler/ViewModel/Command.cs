using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WebCrowler.ViewModel
{
    class Command : ICommand
    {
        public delegate void ExecuteDelegate();

        private readonly ExecuteDelegate command;

        public Command(ExecuteDelegate command)
        {
            this.command = command;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            command();
        }

        public event EventHandler CanExecuteChanged;
    }
}
