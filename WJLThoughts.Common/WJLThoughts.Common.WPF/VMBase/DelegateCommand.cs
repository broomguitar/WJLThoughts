using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WJLThoughts.Common.WPF.VMBase
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _exeAction;
        private readonly Func<bool> _canExecute;
        private bool _canExecuteCache;
        public DelegateCommand(Action action)
        {
            this._exeAction = action;
            _canExecute = () => true;
        }
        public DelegateCommand(Action action, Func<bool> func)
        {
            this._exeAction = action;
            _canExecute = func;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            bool temp = _canExecute();

            if (_canExecuteCache != temp)
            {
                _canExecuteCache = temp;
                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, new EventArgs());
                }
            }

            return _canExecuteCache;
        }

        public void Execute(object parameter)
        {
            _exeAction?.Invoke();
        }
    }
    public class DelegateCommand<T> : ICommand
    {
        private Action<T> _exeAction;
        private Func<T, bool> _canExecute;
        private bool _canExecuteCache;
        public DelegateCommand(Action<T> action)
        {
            this._exeAction = action;
            _canExecute = t => true;
        }
        public DelegateCommand(Action<T> action, Func<T, bool> func)
        {
            this._exeAction = action;
            _canExecute = func;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            T data = (T)parameter;
            bool temp = _canExecute(data);

            if (_canExecuteCache != temp)
            {
                _canExecuteCache = temp;
                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, new EventArgs());
                }
            }

            return _canExecuteCache;
        }

        public void Execute(object parameter)
        {
            T data = (T)parameter;
            _exeAction?.Invoke(data);
        }
    }
}
