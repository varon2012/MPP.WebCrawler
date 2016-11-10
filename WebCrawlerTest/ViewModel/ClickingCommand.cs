using System;
using System.Windows.Input;

namespace WebCrawlerTest.ViewModel
{
    internal class ClickingCommand : ICommand
    {
            private Action action;
            private bool canExecute;
            public event EventHandler CanExecuteChanged;

            public ClickingCommand(Action action)
            {
                this.action = action;
                canExecute = true;
            }

            public bool CanExecute(object parameter)
            {
                return canExecute;
            }

            public void Execute(object parameter)
            {
                action();
            }
        
    }
}
