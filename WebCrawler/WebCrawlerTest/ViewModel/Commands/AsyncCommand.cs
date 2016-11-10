using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WebCrawlerTest.ViewModel.Commands
{
    internal class AsyncCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Func<Task> _method;
        private bool _canExecute;

        public bool CanExecute
        {
            get { return _canExecute; }
            set
            {
                if (_canExecute != value)
                {
                    _canExecute = value;
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public AsyncCommand(Func<Task> method)
        {
            _method = method;
            _canExecute = true;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return _canExecute;
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync();
        }

        private Task ExecuteAsync()
        {
            return _method();
        }
    }
}