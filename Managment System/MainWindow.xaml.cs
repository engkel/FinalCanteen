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
            id_txt.Clear();
            category_txt.Clear();
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
            if (id_txt.Text == string.Empty)
            {
                MessageBox.Show("ID is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (category_txt.Text == string.Empty)
            {
                MessageBox.Show("Category is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        //button function for adding data from the text fields
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                try
                {
                    if (IsValid())
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO FoodItems VALUES (@ItemName, @ItemPrice, @ItemId, @CategoryNumber)", con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ItemName", name_txt.Text);
                        cmd.Parameters.AddWithValue("@ItemPrice", price_txt.Text);
                        cmd.Parameters.AddWithValue("@ItemId", id_txt.Text);
                        cmd.Parameters.AddWithValue("@CategoryNumber", category_txt.Text);
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
        }
    }
}
