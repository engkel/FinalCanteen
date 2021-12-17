using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace Managment_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadGrid();
        }

        //accessing database for the data grid
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-G3GFTK2;Initial Catalog=CanteenDB;Integrated Security=True");

        //class for clearing out the data
        public void clearData()
        {
            name_txt.Clear();
            price_txt.Clear();
            category_txt.Clear();
            search_txt.Clear();
        }

        //loading the database information in the data grid
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from FoodItems", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            datagrid.ItemsSource = dt.DefaultView;

        }

        //button function for clearing the data from the text fields
        private void ClearDataBtn_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }

        //ensuring that text field are fullfilled before adding data
        public bool IsValid()
        {
            if (name_txt.Text == string.Empty)
            {
                MessageBox.Show("Name is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (price_txt.Text == string.Empty)
            {
                MessageBox.Show("Price is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (category_txt.Text == string.Empty)
            {
                MessageBox.Show("Category is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        //button function for adding data from the fields in the database
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
                try
                {
                    if (IsValid())
                    {
                    //command to be executed in the database
                        SqlCommand cmd = new SqlCommand("INSERT INTO FoodItems VALUES (@ItemName, @ItemPrice, @CategoryNumber)", con);
                        cmd.CommandType = CommandType.Text;
                    //set parameter values
                        cmd.Parameters.AddWithValue("@ItemName", name_txt.Text);
                        cmd.Parameters.AddWithValue("@ItemPrice", price_txt.Text);
                        cmd.Parameters.AddWithValue("@CategoryNumber",category_txt.Text);
                        con.Open();
                    cmd.ExecuteNonQuery();
                        con.Close();
                        LoadGrid();
                        MessageBox.Show("Successfully registered", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                        clearData();
                    }

                }catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }

        //button function for deleting data from the fields in the database
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from FoodItems where ItemId = " +search_txt.Text+ " ", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Entry deleted successfully", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                con.Close();
                clearData();
                LoadGrid();
                con.Close();

            } catch (SqlException ex)
            {
                MessageBox.Show("Not Deleted" +ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        //button function for updating the data from existing fields in the database
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update FoodItems set ItemName = '"+name_txt.Text+"', ItemPrice = '"+price_txt.Text+"', CategoryNumber = '"+category_txt.Text+"' WHERE ItemId = '"+search_txt.Text+"' ", con);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Entry updated succsessfully", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);

            } catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            } 
            finally
            {
                con.Close();
                clearData();
                LoadGrid();
            }
        }
    }
}
