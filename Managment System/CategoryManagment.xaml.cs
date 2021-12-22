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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace Managment_System
{
    /// <summary>
    /// Interaction logic for CategoryManagment.xaml
    /// </summary>
    public partial class CategoryManagment : Window
    {
        public CategoryManagment()
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
            Id_txt.Clear();
        }

        //loading the database information in the data grid
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from FoodCategory", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            datagrid.ItemsSource = dt.DefaultView;

        }

        //button function for returning to the main menu window
        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        //button function for clearing the data from the text fields
        private void ClearDataBtn_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }

        //ensuring that all text fields are fullfilled before adding data to database
        public bool IsValid()
        {
            if (name_txt.Text == string.Empty)
            {
                MessageBox.Show("Category Name is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Id_txt.Text == string.Empty)
            {
                MessageBox.Show("Category ID is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    SqlCommand cmd = new SqlCommand("INSERT INTO FoodCategory VALUES (@CategoryNumber, @CategoryName)", con);
                    cmd.CommandType = CommandType.Text;
                    //set parameter values
                    cmd.Parameters.AddWithValue("@CategoryName", name_txt.Text);
                    cmd.Parameters.AddWithValue("@CategoryNumber", Id_txt.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid();
                    MessageBox.Show("Successfully registered", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //button function for deleting data from the fields in the database
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from FoodCategory where CategoryNumber = " + search_txt.Text + " ", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Entry deleted successfully", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                con.Close();
                clearData();
                LoadGrid();
                con.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not Deleted" + ex.Message);
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
            SqlCommand cmd = new SqlCommand("update FoodCategory set CategoryName = '" + name_txt.Text + "' WHERE CategoryNumber = '" + search_txt.Text + "' ", con);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Entry updated succsessfully", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (SqlException ex)
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
