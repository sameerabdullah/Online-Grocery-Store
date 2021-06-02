using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Online_Grocery_Store.Model
{
    class CRUDCustomer //A class to perform database CRUD operations on Customer Table stored in local database
    {
        SqlConnection con; //Instance of database connection object
        public CRUDCustomer() //Constructor
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
        public bool signUP(Customer custom) //Function adding new Customer to database
        {
            if (con == null) return false; //If connection is not created, returing false
            try
            {
                string query = $"insert into Customer(Username, Password, Phone) values('{custom.Username}', '{custom.Password}', '{custom.PhoneNo}')"; //SQL query for inserting new customer in database
                SqlCommand cmd = new SqlCommand(query, con); //Creating SQL statement
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            return false;
        }
        public bool verifyCustomer(string username, string password) //Function validating a Customer from database
        {
            if (con == null) return false; //If connection is not created, returing false
            string query = $"select * from Customer where username = @user and password = @pass"; //SQL parameterized query for selecting rows with required user and pass in database
            SqlParameter user = new SqlParameter("user", username); //Creating parameters for parameterized query
            SqlParameter pass = new SqlParameter("pass", password); //Creating parameters for parameterized query
            SqlCommand cmd = new SqlCommand(query, con); //Creating SQL statement
            cmd.Parameters.Add(user); //Adding 1st parameters to parameterized query
            cmd.Parameters.Add(pass); //Adding 2nd parameters to parameterized query
            SqlDataReader dr = cmd.ExecuteReader(); //Reading data from SQL statement
            if (dr.HasRows) //Checking is datareader has any row of data
                return true;
            return false;
        }
        public void closeDB() //Function closing connection with database
        {
            con.Close(); //Closing connection with database
        }
    }
}
