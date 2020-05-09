using System;
using System.Windows.Input;

namespace TourApp.Base
{
    public class BaseCommand : ICommand
    {
        private Action<object> _action;
        public BaseCommand(Action<object> action)
        {
            _action = action;
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) { return true; }
        public void Execute(object parameter) =>
           _action(parameter);
    }
}
