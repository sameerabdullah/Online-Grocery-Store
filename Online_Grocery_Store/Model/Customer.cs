using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Online_Grocery_Store.Model
{
    class Customer : INotifyPropertyChanged //A class containing all properties of the reqired customer in this project implementng INotifyPropertyChanged Interface which notify the client that value property changed
    {
        public event PropertyChangedEventHandler PropertyChanged; //Buildin Event INotifyPropertyChanged Interface when any property value changes
        private void NotifyPropertyChanged(string name) //Function calling te PropertyChanged
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        private string username; //Customer's Username
        private string password; //Customer's Password
        private string phoneNo; //Customer's Phone Number

        public string Username //Property for Customer's Username
        {
            get { return username; }
            set
            {
                username = value;
                NotifyPropertyChanged("Username"); //Calling the PropertyChanged Event
            }
        }

        public string Password //Property for Customer's Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged("Password"); //Calling the PropertyChanged Event
            }
        }

        public string PhoneNo //Property for Customer's Phone Number
        {
            get { return phoneNo; }
            set
            {
                phoneNo = value;
                NotifyPropertyChanged("PhoneNo"); //Calling the PropertyChanged Event
            }
        }
    }
}
