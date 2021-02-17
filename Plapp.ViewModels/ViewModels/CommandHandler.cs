
using System;
using System.Windows.Input;

namespace Plapp.ViewModels
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

    public class CommandHandler<T> : ICommand
    {
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        private readonly Action<T> _action;
        private readonly Predicate<T> _canExecute;

        public CommandHandler(Action<T> action, Predicate<T> executionPredicate = null)
        {
            _action = action;
            _canExecute = executionPredicate ?? new Predicate<T>((p) => true);
        }

        public bool CanExecute(object parameter) => _canExecute((T)parameter);

        public void Execute(object parameter) => _action((T)parameter);
    }
}
