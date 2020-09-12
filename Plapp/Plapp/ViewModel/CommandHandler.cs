
using System;
using System.Windows.Input;

namespace Plapp
{
    public class CommandHandler : ICommand
    {
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        private readonly Action _action;
        private readonly Predicate<object> _canExecute;

        public CommandHandler(Action action, Predicate<object> executionPredicate = null)
        {
            _action = action;
            _canExecute = executionPredicate ?? new Predicate<object>((p) => true);
        }

        public bool CanExecute(object parameter) => _canExecute(parameter);

        public void Execute(object parameter) => _action();
    }
}
