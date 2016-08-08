using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hapo31.IkaNama.Utils
{
	class Command : ICommand
	{
		readonly Action<object> execute;
		readonly Predicate<object> canExecute;

		public Command(Action<object> execute)
			: this(execute, null) {}

		public Command(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null) throw new ArgumentNullException("第一引数がnullです");
			this.execute = execute;
			this.canExecute = canExecute;
		}
			 
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		public bool CanExecute(object parameter)
		{
			return canExecute == null ? true : canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			execute(parameter);
		}
	}
}
