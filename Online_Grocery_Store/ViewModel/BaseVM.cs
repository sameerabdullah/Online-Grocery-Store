using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Online_Grocery_Store.ViewModel
{
    class BaseVM : INotifyPropertyChanged //A class implementing INotifyPropertyChanged and is the base class of all View Models
    {
        public event PropertyChangedEventHandler PropertyChanged; //Buildin Event INotifyPropertyChanged Interface when any property value changes
        protected void NotifyPropertyChanged(string name) //Function calling te PropertyChanged
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
