using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CabarlesWPF.Commands
{
    public class AsyncRelayCommand<T> : ICommand
    {
        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool> _canExecute;
        private bool _isExecuting;

        public AsyncRelayCommand(Func<T, Task> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (parameter == null && typeof(T).IsValueType)
                return !_isExecuting && (_canExecute == null || _canExecute(default(T)));
            return !_isExecuting && (_canExecute == null || _canExecute((T)parameter));
        }

        public async void Execute(object parameter)
        {
            _isExecuting = true;
            CommandManager.InvalidateRequerySuggested();

            try
            {
                await _execute((T)parameter);
            }
            finally
            {
                _isExecuting = false;
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }
}
