using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WebCrawlerTest.ViewModel
{
    internal class CrawlingCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Func<Task> command;
        private bool isEnabled;

        public CrawlingCommand(Func<Task> command)
        {
            this.command = command;
            isEnabled = true;
        }


        public bool CanExecute(object parameter)
        {
            return isEnabled;
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync();
        }

        private Task ExecuteAsync()
        {
            return command();
        }

        public void Disable()
        {
            isEnabled = false;
        }

        public void Enable()
        {
            isEnabled = true;
        }
    }
}
