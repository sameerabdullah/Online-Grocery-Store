using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Online_Grocery_Store.Model
{
    class Product : INotifyPropertyChanged //A class containing all properties of the reqired product in this project implementng INotifyPropertyChanged Interface which notify the client that value property changed
    {
        public event PropertyChangedEventHandler PropertyChanged; //Buildin Event INotifyPropertyChanged Interface when any property value changes
        private void NotifyPropertyChanged(string name) //Function calling te PropertyChanged
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        private int id; //Product ID
        private string name; //Product Name
        private decimal price; //Product Price
        private int quantity; ///Product Quantity

        public int Id //Property for Product ID
        {
            get { return id; }
            set { id = value;
                NotifyPropertyChanged("Id"); //Calling the PropertyChanged Event
            }
        }

        public string Name //Property for Product Name
        {
            get { return name; }
            set { name = value;
                NotifyPropertyChanged("Name"); //Calling the PropertyChanged Event
            }
        }

        public decimal Price //Property for Product Price
        {
            get { return price; }
            set { price = value;
                NotifyPropertyChanged("Price"); //Calling the PropertyChanged Event
            }
        }

        public int Quantity //Property for Product Quantity
        {
            get { return quantity; }
            set { quantity = value;
                NotifyPropertyChanged("Quantity"); //Calling the PropertyChanged Event
            }
        }
    }
}
