﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfViewModelApp
{
    public class EmployeeCommand : ICommand
    {
        Action<object> execute;
        Func<object, bool> canExecute;

        public EmployeeCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter!);
        }

        public void Execute(object? parameter)
        {
            execute?.Invoke(parameter!);
        }
    }
}
