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

namespace Managment_System
{
    /// <summary>
    /// Interaction logic for EmployeeManagment.xaml
    /// </summary>
    public partial class EmployeeManagment : Window
    {
        public EmployeeManagment()
        {
            InitializeComponent();
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }
    }
}
