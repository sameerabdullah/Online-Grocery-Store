using Online_Grocery_Store.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Online_Grocery_Store.View
{
    /// <summary>
    /// Interaction logic for CustomerLogin.xaml
    /// </summary>
    public partial class CustomerLogin : UserControl
    {
        public CustomerLogin()
        {
            InitializeComponent();
            this.DataContext = new MainVM(); //Setting datacontext with object oF MainVM
        }
    }
}
