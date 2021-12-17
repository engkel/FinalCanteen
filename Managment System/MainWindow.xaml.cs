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
        }

        //Redirecting to the Employee Managment window
        private void EmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManagment window = new EmployeeManagment();
            window.Show();
            Close();
        }

        //Redirecting to Order Managment Window
        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderManagment window = new OrderManagment();
            window.Show();
            Close();
        }

        //Redirecting to Production Managment Window
        private void ProductBtn_Click(object sender, RoutedEventArgs e)
        {
            ProductManagment window = new ProductManagment();
            window.Show();
            Close();
        }

        //Redirecting to Category Managment Window
        private void CategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            CategoryManagment window = new CategoryManagment();
            window.Show();
            Close();
        }
    }
}
