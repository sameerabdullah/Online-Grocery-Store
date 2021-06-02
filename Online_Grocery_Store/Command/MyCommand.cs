using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Online_Grocery_Store.Command
{
    class MyCommand : ICommand //A class of a Degelate Command implementing of ICommand Interface
    {
        public event EventHandler CanExecuteChanged //Builtin Event of ICommand Interface occurs when change occur in class 
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        private Action<object> exe; //An instance of Action Delegate which takes object as parameter and has return type void
        private Predicate<object> canExe; //An instance of Predicate Delegate which takes object as parameter and has return type bool
        public MyCommand(Predicate<object> canExe, Action<object> exe) //Constructor of Delegate Command
        {
            this.canExe = canExe;
            this.exe = exe;
        }
        public bool CanExecute(object parameter) //Overriding CanExecute(object) function of ICommand Interface
        {
            return this.canExe(parameter);
        }

        public void Execute(object parameter) //Overriding Execute(object) function of ICommand Interface
        {
            this.exe(parameter);
        }
    }
}
