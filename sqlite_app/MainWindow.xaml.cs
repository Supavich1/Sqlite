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

namespace sqlite_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataAccess.InitializeDatabase();
            Customers.InitializeDatabase();
            //Customers.alterData();
            //Customers.dropData();
            Customers.AddData("Supavich", "Piromkloy", "Testmail@gmail.com");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string all_data = "";
            foreach(string data in Customers.GetData())
            {
                all_data = all_data + " " + data + "\n";
            }
            MessageBox.Show(all_data);
        }
    }
}
