using Online_Grocery_Store.Command;
using Online_Grocery_Store.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
namespace Online_Grocery_Store.ViewModel
{
    class MainVM : BaseVM //A class perform all the view model operations inherted from BaseVM
    {
        public MyCommand scmd { get; set; } //Command for changing screens, helping to switching screen from one to another
        public MyCommand apcmd { get; set; } //Command for add product button, adding product from database
        public MyCommand dpcmd { get; set; } //Command for delete product button, delete product from database
        public MyCommand lgcmd { get; set; } //Command for login button, validate customer from database and moved it to Sale Screen
        public MyCommand sucmd { get; set; } //Command for signup button, add customer in database
        public MyCommand actcmd { get; set; } //Command for add cart button, add product in cart
        public MyCommand cocmd { get; set; } //Command for finish button, performing checkout
        private BaseVM page; //Instance used to create objects of different view models for screen switching
        private DataView productDG; //Instance of DataView type for holding products available in database to show them in datagrid
        private DataView cartDG; //Instance of DataView type for holding in-cart products to show them in datagrid
        private CRUDProduct crudP; //Instance of CRUDProduct to perform database crud operations of Product Table
        private CRUDCustomer crudC; //Instance of CRUDCustomer to perform database crud operations of Customer Table 
        private bool firstIter; //Boolean flag used to handle datagrid
        private DataSet dataSet; //Instance of DataSet used for cart datagrid's data
        private DataTable dataTable; //Instance of DataTable used for cart datagrid's data
        private List<Product> cart; //List used to product available in cart
        public BaseVM Page //Property for page
        {
            get { return page; }
            set 
            { 
                page = value;
                NotifyPropertyChanged("Page"); //Calling the PropertyChanged Event
            }
        }
        public DataView ProductDG //Property for productDG
        {
            get { return productDG; }
            set
            {
                productDG = value;
                NotifyPropertyChanged("ProductDG"); //Calling the PropertyChanged Event
            }
        }
        public DataView CartDG //Property for cartDG
        {
            get { return cartDG; }
            set
            {
                cartDG = value;
                NotifyPropertyChanged("cartDG"); //Calling the PropertyChanged Event
            }
        }
        public MainVM() //Constructor, used for commands initializations
        {
            scmd = new MyCommand(canExeTrue, Execute); //Initializing Delegate Command with two function with one parameter of object type and one with bool return type and other one is with void respectively
            apcmd = new MyCommand(canExeAddProd, ExeAddProd); //Initializing Delegate Command with two function with one parameter of object type and one with bool return type and other one is with void respectively
            dpcmd = new MyCommand(canExeDelProd, ExeDelProd); //Initializing Delegate Command with two function with one parameter of object type and one with bool return type and other one is with void respectively
            lgcmd = new MyCommand(canExeLogin, ExeLogin); //Initializing Delegate Command with two function with one parameter of object type and one with bool return type and other one is with void respectively
            sucmd = new MyCommand(canExeSignup, ExeSignup); //Initializing Delegate Command with two function with one parameter of object type and one with bool return type and other one is with void respectively
            actcmd = new MyCommand(canExeAddCart, ExeAddCart); //Initializing Delegate Command with two function with one parameter of object type and one with bool return type and other one is with void respectively
            cocmd = new MyCommand(canExeCheckOut, ExeCheckOut); //Initializing Delegate Command with two function with one parameter of object type and one with bool return type and other one is with void respectively
            firstIter = true;
        }
        public bool canExeTrue(object obj) //Function always returning true acting as CanExcute for scmd, also performing some datagrid operation
        {
            if (obj.ToString().Equals("BackHome") && firstIter)
            {
                dataSet = new DataSet(); //Creating DataSet Object
                dataTable = new DataTable(); //Creating DataTable Object
                dataSet.Tables.Add(dataTable);
                dataTable.Columns.Add("Product ID"); //Adding Column with named as Product ID in dataTable 
                dataTable.Columns.Add("Product Name"); //Adding Column with named as Product Name in dataTable
                dataTable.Columns.Add("Price"); //Adding Column with named as Price in dataTable
                dataTable.Columns.Add("Quantity"); //Adding Column with named as Quantity in dataTable
                cart = new List<Product>(); //Creating object of List of Products
            }
            if ((obj.ToString().Equals("BackAdmin") || obj.ToString().Equals("BackHome")) && firstIter)
            {
                try
                {
                    crudP = new CRUDProduct(); //Creating CRUDProduct which opens database also
                    ProductDG = crudP.readProducts(); //Adding data to product datagrid
                    firstIter = false;
                    crudP.closeDB(); //Closing database
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); //Showing exception Message
                }
            }
            return true;
        }
        public void Execute(object obj) //Function acting as Excute for scmd, switch screen operations
        {
            if (obj.ToString().Equals("Admin"))
                Page = new AdminDashboard(); //Creating object of AdminDashboard to render Admin Dashboard Screen
            else if (obj.ToString().Equals("BackAdmin"))
                Page = new AdminDashboard(); //Creating object of AdminDashboard to render Admin Dashboard Screen
            else if (obj.ToString().Equals("Customer"))
                Page = new CustomerLogin();  //Creating object of CustomerLogin to render Customer Login Screen
            else if (obj.ToString().Equals("Home"))
                Page = new Home(); //Creating object of Home to render Home Screen
            else if (obj.ToString().Equals("BackHome"))
                Page = new Home(); //Creating object of Home to render Home Screen
            else if (obj.ToString().Equals("Products"))
                Page = new ShowProducts(); //Creating object of ShowProduct to render Show Product Screen
        }
        public bool canExeAddProd(object obj) //Function acting as CanExcute for apcmd, returning true when all fields of add product form are filled
        {
            var data = obj as object[]; //Casting object to object array
            if (data == null) return false;
            for (int i = 0; i < 4; i++)
                if (String.IsNullOrEmpty(data[i].ToString())) //Checking is fields has any value
                    return false;
            return true;
        }
        public void ExeAddProd(object obj) //Function acting as Excute for apcmd, adding product in database
        {
            var data = obj as object[]; //Casting object to object array
            try
            {
                if(Convert.ToInt32(data[3]) < 1) //Checking is given quantity negative/zero or not
                {
                    MessageBox.Show("Quantity must not negative or zero!"); //Showing negative or zero quanity message
                    return;
                }
                else if (Convert.ToDecimal(data[2]) <= 0) //Checking is given price negative/zero or not 
                {
                    MessageBox.Show("Price must not negative or zero!"); //Showing negative or zero price message
                    return;
                }
                crudP = new CRUDProduct(); //Creating CRUDProduct which opens database also
                if (crudP.addProduct(
                    new Product
                    {
                        Id = Convert.ToInt32(data[0]),
                        Name = data[1].ToString(),
                        Price = Convert.ToDecimal(data[2]),
                        Quantity = Convert.ToInt32(data[3])
                    }
                )) //Adding product to database and checking is added or not
                    MessageBox.Show("Product Successfully Added!"); //Showing Product Added message
                else
                    MessageBox.Show("Product already exist with same ID!"); //Showing Product already exist message
                crudP.closeDB(); //Closing database
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //Showing exception message
            }
        }
        public bool canExeDelProd(object obj) //Function acting as CanExcute for dpcmd, returning true when field of delete product form is filled
        {
            if (obj == null || String.IsNullOrEmpty(obj.ToString())) //Checking is ID field is empty or not
                return false;
            return true;
        }
        public void ExeDelProd(object obj) //Function acting as Excute for dpcmd, deleting product from database
        {
            var data = obj as object[]; //Casting object to object array
            try
            {
                crudP = new CRUDProduct(); //Creating CRUDProduct which opens database also
                if (crudP.delProduct(Convert.ToInt32(obj))) //Deleting product from database and checking is deleted or not
                    MessageBox.Show("Product Successfully Removed!"); //Showing Product Remove message
                else
                    MessageBox.Show("Product does not exist with same ID!"); //Showing Product does not exist message
                crudP.closeDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //Shwoing exception message
            }
        }
        public bool canExeSignup(object obj) //Function acting as CanExcute for sucmd, returning true when all fields of signup form are filled
        {
            var data = obj as object[]; //Casting object to object array
            if (data == null) return false;
            if (String.IsNullOrEmpty(data[0].ToString()) || String.IsNullOrEmpty(((PasswordBox)data[1]).Password) || String.IsNullOrEmpty(data[2].ToString())) //Checking fields any is empty or not
                return false;
            return true;
        }
        public void ExeSignup(object obj) //Function acting as Excute for sucmd, performing Customer signup
        {
            var data = obj as object[]; //Casting object to object array
            string phone = data[2].ToString();
            if ((phone.IndexOf("03") == 0 && phone.Length == 11) || (phone.IndexOf("923") == 0 && phone.Length == 12)) //Checking is phone number is valid
            {
                for (int i = 2; i< phone.Length; i++)
                    if(phone[i] < '0' || phone[i] > '9') //Checking is phone number is valid
                    {
                        MessageBox.Show("Invalid Phone Number!"); //Showing Invalid Phone Number message
                        return;
                    }
            }
            else
            {
                MessageBox.Show("Invalid Phone Number!"); //Showing Invalid Phone Number message
                return;
            }
            try
            {
                crudC = new CRUDCustomer(); //Creating CRUDCustomer which opens database also
                if (crudC.signUP(
                    new Customer
                    {
                        Username = data[0].ToString(),
                        Password = ((PasswordBox)data[1]).Password,
                        PhoneNo = phone
                    }
                )) //Adding customer to database and checking is added or not
                    MessageBox.Show("Successfully Signed Up!"); //Showing customer signed up message
                else
                    MessageBox.Show("Customer already exist!"); //Showing customer already exist message
                crudC.closeDB(); //Closing database
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //Showing exception message
            }
        }
        public bool canExeLogin(object obj) //Function acting as CanExcute for lgcmd, returning true when all fields of login form are filled
        {
            var data = obj as object[]; //Casting object to object array
            if (data == null) return false;
            if (String.IsNullOrEmpty(data[0].ToString()) || String.IsNullOrEmpty(((PasswordBox)data[1]).Password)) //Checking fields any is empty or not
                return false;
            return true;
        }
        public void ExeLogin(object obj) //Function acting as Excute for lgcmd, performing Customer login and moving to sale screen
        {
            var data = obj as object[]; //Casting object to object array
            try {
                crudC = new CRUDCustomer(); //Creating CRUDCustomer which opens database also
                if (crudC.verifyCustomer(data[0].ToString(), ((PasswordBox)data[1]).Password)) //validating customer from database
                    Page = new Sale(); //rendering to Customer Sale screen
                else
                    MessageBox.Show("Invalid Credentials!"); //Showing Invalid Credentials message
                crudC.closeDB(); //Closing database
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //Showin exception message
            }
        }
        public bool canExeAddCart(object obj) //Function acting as CanExcute for actcmd, returning true when all fields of add cart form are filled
        {
            var data = obj as object[]; //Casting object to object array
            if (data == null) return false;
            for (int i = 0; i < 2; i++)
                if (String.IsNullOrEmpty(data[i].ToString())) //Checking any field is empty or not
                    return false;
            return true;
        }
        public void ExeAddCart(object obj) //Function acting as Excute for actcmd, adding product to cart
        {
            var data = obj as object[]; //Casting object to object array
            try
            {
                int id = Convert.ToInt32(data[0]);
                int quan = Convert.ToInt32(data[1]);
                if (quan < 1) //Checking is given quantity negative/zero or not
                {
                    MessageBox.Show("Quantity must not negative or zero!"); //Showing negative or zero quanity message
                    return;
                }
                crudP = new CRUDProduct(); //Creating CRUDProduct which opens database also
                Product prod = crudP.getProduct(id); //Retriving product from cart
                if (prod == null) //Validating product exist or not
                {
                    MessageBox.Show("Product does not exist!"); //Showing product already exist message
                    return;
                }
                int crtQuan = getQuanCL(id); //Retriving product quantity from cart
                if (crtQuan == -1 && prod.Quantity < quan) //Validating quatity
                {
                    MessageBox.Show($"Sorry, { prod.Quantity } Products are not available!"); //Showing low quantity message
                    return;
                }
                else if( prod.Quantity - crtQuan < quan) //Validating quatity
                {
                    MessageBox.Show($"Sorry, { prod.Quantity - crtQuan } Products are not available!"); //Showing low quantity message
                    return;
                }
                prod.Quantity = quan; //Changing product quanity
                addCL(prod); //Adding product exist in cart List increase quantity oterwise add new product
                var row = dataTable.NewRow(); //New row object in dataTable
                row["Product ID"] = prod.Id; //Adding Product ID value in dataTable's new row
                row["Product Name"] = prod.Name; //Adding Product Name value in dataTable's new row
                row["Price"] = prod.Price; //Adding Price value in dataTable's new row
                row["Quantity"] = prod.Quantity; //Adding Quantity value in dataTable's new row
                dataTable.Rows.Add(row); //Adding row in dataTable
                CartDG = dataTable.DefaultView; //Adding dataTable data to datagrid
                crudP.closeDB(); //Closing database
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //Showing exception message
            }
        }
        public bool canExeCheckOut(object obj) //Function acting as CanExcute for cocmd, returning true when CartDG has at least one row
        {
            if (cart.Count > 0) //Checking count of products in cart List
                return true;
            return false;
        }
        public void ExeCheckOut(object obj) //Function acting as Excute for actcmd, performing checout and showing receipt message box and redirecting to Main/Home Screen
        {
            try
            {
                int purchasedTill = cart.Count;
                crudP = new CRUDProduct(); //Creating CRUDProduct which opens database also
                for (int i = 0; i < cart.Count; i++)
                    if (!crudP.updateProduct(cart[i].Id, cart[i].Quantity)) //Updating product quantity in database
                    {
                        MessageBox.Show("Process Failed!"); //Showing updating failed message
                        purchasedTill = i;
                        return;
                    }
                string receipt = $"{new String('-', 20)} Receipt {new String('-', 20)}\n"; //receipt indentation
                decimal total = 0M; //Initizing variable with 0 to calcuate net total in it
                for (int i = 0; i < cart.Count; i++)
                {
                    receipt += $"{String.Format("{0,-20:D}", cart[i].Name.ToString())} {String.Format("{0,-10:D}", cart[i].Price.ToString())} {String.Format("{0,-5:D}", cart[i].Quantity.ToString())}\n"; //Adding products in receipt
                    total += cart[i].Price*cart[i].Quantity; //Calculating net total
                }
                receipt += new String('-', 50); //receipt indentation
                receipt += $"\nNet Total = {total}"; ////adding net total to receipt
                MessageBox.Show(receipt); //Showing receipt
                crudP.delZQuanProd(); //Deleting products with zero quantity
                crudP.closeDB(); //Closing database
                firstIter = true;
                cart.Clear(); //Clearing products List
                CartDG = null; //Clearing cart datagrid
                canExeTrue("BackHome"); //Refreshing Cart and Product datagrid
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //Showing exception message
            }
        }
        private void addCL(Product prod) //Private function add product or increase product quantity from cart list
        {
            for(int i = 0; i<cart.Count; i++)
                if(cart[i].Id == prod.Id)
                {
                    cart[i].Quantity += prod.Quantity;  //Increasing product quantity in cart
                    return;
                }
            cart.Add(prod);  //Adding new product in cart
        }
        private int getQuanCL(int id) //Private function retriving quantity of a product from cart list
        {
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Id == id)
                    return cart[i].Quantity;
            return -1; //If product does not exist in cart list
        }
    }
}
