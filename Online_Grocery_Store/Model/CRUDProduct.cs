using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Windows;

namespace Online_Grocery_Store.Model
{
    class CRUDProduct //A class to perform database CRUD operations on Product Table stored in local database
    {
        SqlConnection con; //Instance of database connection object
        public CRUDProduct() //Constructor
        {
            try
            {
                string connString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=OGSDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; //Connection String
                con = new SqlConnection(connString); //Initializing connection instance
                con.Open(); //Opening connection with OGSDB database
            }
            catch (Exception ex)
            {
                throw ex; //Throwing exception
            }
        }
        public bool addProduct(Product prod) //Function adding new Product to database
        {
            if(con == null) return false; //If connection is not created, returing false
            try
            {
                string query = $"insert into Product(id, name, price, quantity) values({prod.Id}, '{prod.Name}', {prod.Price}, {prod.Quantity})"; //SQL query for inserting new product in database
                SqlCommand cmd = new SqlCommand(query, con); //Creating SQL statement
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
        public DataView readProducts() //Function read all Products from database
        {
            if (con == null) return null; //If connection is not created, returing false
            try
            {
                string query = $"select id as 'Product ID', name as 'Product Name', Price, Quantity from Product"; //SQL query for selecting all data in avaiable in product table from database
                SqlCommand cmd = new SqlCommand(query, con); //Creating SQL statement
                cmd.ExecuteNonQuery(); //Executing Query
                SqlDataAdapter adp = new SqlDataAdapter(cmd); //Creating object of DataAdapter
                DataTable table = new DataTable("Product"); //Creating object of DataTable named Product
                adp.Fill(table); //Filling Product DataTable
                return table.DefaultView;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool delProduct(int id) //Function delete Product from database
        {
            if (con == null) return false; //If connection is not created, returing false
            try
            {
                string query = $"delete from Product where id={id}"; //SQL query for deleting product with given id from database
                SqlCommand cmd = new SqlCommand(query, con); //Creating SQL statement
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
        public Product getProduct(int id) //Function to get Product by ID from database
        {
            if (con == null) return null; //If connection is not created, returing false
            try
            {
                string query = $"select * from Product where Id = {id}"; //SQL query for selecting product with given id from database
                SqlCommand cmd = new SqlCommand(query, con); //Creating SQL statement
                SqlDataReader dataRdr = cmd.ExecuteReader(); //Reading data from SQL statement
                if (dataRdr.Read()) //Iterating rows and checking is row available to iterate
                {
                    return new Product { Id = Convert.ToInt32(dataRdr["Id"]), Name = dataRdr["Name"].ToString() , Price = Convert.ToDecimal(dataRdr["Price"]), Quantity = Convert.ToInt32(dataRdr["Quantity"]) }; //returning a product read from database 
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool updateProduct(int id, int quan) //Function to update quantity of a Product in database
        {
            if (con == null) return false; //If connection is not created, returing false
            try
            {
                string query = $"update Product set Quantity = Quantity - {quan} where id={id}"; //SQL query for updating quantity of a Product from database
                SqlCommand cmd = new SqlCommand(query, con); //Creating SQL statement
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
        public bool delZQuanProd() //Function deletes Products with zero quantity from database
        {
            string query = $"delete from Product where Quantity = 0"; //SQL query for deleting all products with zero quantity from database
            SqlCommand cmd = new SqlCommand(query, con); //Creating SQL statement
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            return false;
        }
        public void closeDB() //Function closing connection with database
        {
            con.Close();  //Closing connection with database
        }
    }
}
