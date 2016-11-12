using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WebCrawlerUI.ViewModel
{
    public class Command : ICommand
    {
        #region Constructors

        public Command(Action action)
        {
            ExecuteDelegate = action;
        }

        public Command(Action<object> action)
        {
            ExecuteDelegateParameterized = action;
        }

        #endregion


        #region Properties

        public Predicate<object> CanExecuteDelegate { get; set; }
        public Action<object> ExecuteDelegateParameterized { get; set; }
        public Action ExecuteDelegate { get; set; }

        #endregion


        #region ICommand Members

        public virtual bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate != null)
            {
                return CanExecuteDelegate(parameter);
            }

            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public virtual void Execute(object parameter)
        {
            if (parameter == null)
            {
                Execute();
            }
            else
            {
                if (ExecuteDelegateParameterized != null)
                {
                    ExecuteDelegateParameterized(parameter);
                }
            }
        }

        public virtual void Execute()
        {
            if (ExecuteDelegate != null)
            {
                ExecuteDelegate();
            }
        }

        #endregion
    }
}
