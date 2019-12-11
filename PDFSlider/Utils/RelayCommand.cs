using System;
using System.Windows.Input;

namespace PDFSlider.Services
{
    public class RelayCommand : ICommand
    {
        #region Fields
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;
        #endregion

        public RelayCommand(Action<object> execute)
                           : this(execute, null) { }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute is null)
                throw new ArgumentNullException("RelayCommand execute error");

            this.execute = execute;
            if (canExecute != null)
                this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
