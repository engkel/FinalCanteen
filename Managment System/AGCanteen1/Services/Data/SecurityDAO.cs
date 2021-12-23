using AGCanteen.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

//Author: Andrej

namespace AGCanteen.Services.Data
{
    public class SecurityDAO
    {
        //Connection to database
        //String connectionString = "\"Data Source = DESKTOP - AVMV162; Initial Catalog = CanteenDB; Integrated Security = True\"";
        //SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-AVMV162;Initial Catalog=CanteenDB;Integrated Security=True");

        //Checks if the user exists in the database
        internal bool FindUserId(UserModel user)
        {
            bool success = false;

            string query = "Select * from dbo.Employee Where EmpId = @Id";

            //Creating a database inside a using block
            //This ensures all resources are closed properly when query ends 
           
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-AVMV162;Initial Catalog=CanteenDB;Integrated Security=True"))
            {
                //Creating command and parameter objects
                SqlCommand command = new SqlCommand(query, connection);

                //Setting @Id to be equal to user.Id
                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = user.Id;


                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                       
                        success = true;
                       
                        
                    }
                    else
                    {
                        success = false;
                    }
                    reader.Close();


                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);

                }
            }
            return success;
        }

        internal bool StoreOrder(List<BreakfastModel> cart, int userID, string orderID)
        {


            bool success = false;

            foreach(var cartItem in cart)
            {
                string query = "INSERT INTO dbo.ShoppingCart Values(@OrderId, @Amount, @ItemID)";

        
                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-AVMV162;Initial Catalog=CanteenDB;Integrated Security=True"))
                {
                    //Creating command and parameter objects
                    SqlCommand command = new SqlCommand(query, connection);

                    //Setting the @ values to their counterparts
                    command.Parameters.Add("@OrderId", System.Data.SqlDbType.NVarChar, 1000).Value = orderID;
                    command.Parameters.Add("@Amount", System.Data.SqlDbType.Int).Value = cartItem.quantity;
                    command.Parameters.Add("@ItemID", System.Data.SqlDbType.Int).Value = cartItem.ItemID;

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                       
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);

                    }
                }

            }

            success = storeReceipt(orderID, userID);
            return success;
        }

        internal bool storeReceipt(string orderID, int userID)
        {

            string query2 = "INSERT INTO dbo.Receipt Values(@OrderId, @Date, @EmpId)";


            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-AVMV162;Initial Catalog=CanteenDB;Integrated Security=True"))
            {
                //Creating command and parameter objects
                SqlCommand command = new SqlCommand(query2, connection);

                //Setting the @ values to their counterparts
                command.Parameters.Add("@OrderId", System.Data.SqlDbType.NVarChar, 1000).Value = orderID;
                command.Parameters.Add("@Date", System.Data.SqlDbType.Date).Value = DateTime.Now.ToString("yyyy'-'MM'-'dd");
                command.Parameters.Add("@EmpId", System.Data.SqlDbType.Int).Value = userID;

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                   
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
            }

            return true;


        }






        //Get User
        internal List<UserModel> GetUser(UserModel user)
        {
            List<UserModel> currentUser = new List<UserModel>();

            string query = "Select * from dbo.Employee Where EmpId = @Id";

            //Creating a database inside a using block
            //This ensures all resources are closed properly when query ends 

            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-AVMV162;Initial Catalog=CanteenDB;Integrated Security=True"))
            {
                //Creating command and parameter objects
                SqlCommand command = new SqlCommand(query, connection);

                //Setting @Id to be equal to user.Id
                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = user.Id;


                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserModel um = new UserModel();
                            um.Id = reader.GetInt32(0);
                            um.name = reader.GetString(1);
                            currentUser.Add(um);
                        }


                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }

                return currentUser;
            }
          
        }


        //Gets all of the Canteen Items and returns them as a list
        internal List<BreakfastModel> GetCanteenItems()
        {
            
            List<BreakfastModel> returnList = new List<BreakfastModel>();

            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-AVMV162;Initial Catalog=CanteenDB;Integrated Security=True"))
            {
                string query = "Select * from dbo.FoodItems";
                //Creating command and parameter objects
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    //Data Reader is a place where you store your results from a sql query
                    SqlDataReader reader = command.ExecuteReader();


                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //Creates a Breakfast obeject and adds it to the list
                            BreakfastModel item = new BreakfastModel();
                            item.ItemID = reader.GetInt32(0);
                            item.Name = reader.GetString(1);
                            item.Price = reader.GetInt32(2);
                            item.Category = reader.GetInt32(3);
                            item.quantity = 1;

                            returnList.Add(item);
                        }
                       
                    }
                    
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return returnList;

        }



        //Add to cart

        internal void create(CartItemModel cart)
        {

            string query = "INSERT INTO dbo.ShoppingCart Values(@OrderId, @Amount, @ItemID)";

            //Creating a database inside a using block
            //This ensures all resources are closed properly when query ends 

            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-AVMV162;Initial Catalog=CanteenDB;Integrated Security=True"))
            {
                //Creating command and parameter objects
                SqlCommand command = new SqlCommand(query, connection);

                //Setting the @ values to their counterparts
                command.Parameters.Add("@OrderId", System.Data.SqlDbType.NVarChar,1000).Value = cart.OrderId;
                command.Parameters.Add("@OrderId", System.Data.SqlDbType.Int).Value = cart.Amount;
                command.Parameters.Add("@OrderId", System.Data.SqlDbType.Int).Value = cart.ItemID;

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
            }
        }








    }



}