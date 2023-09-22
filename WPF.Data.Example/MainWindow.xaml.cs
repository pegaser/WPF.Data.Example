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

namespace WPF.Data.Example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cmdGetProduct.Click += CmdGetProduct_Click;
            cmdUpdateProduct.Click += CmdUpdateProduct_Click;
            cmdChangePrice.Click += CmdChangePrice_Click;
        }

        private void CmdChangePrice_Click(object sender, RoutedEventArgs e)
        {
            Product product = gridProductDetails.DataContext as Product;
            product.UnitCost *= 1.1M;
        }

        private void CmdUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            Product product = (Product)gridProductDetails.DataContext;
            try
            {
                App.StoreDB.UpdateProduct(product);
            }
            catch
            {
                MessageBox.Show("Error contacting database.");
            }
        }


        private void CmdGetProduct_Click(object sender, RoutedEventArgs e)
        {
            int ID;
            if (Int32.TryParse(txtProductId.Text, out ID))
            {
                try
                {
                    gridProductDetails.DataContext = App.StoreDB.GetProduct(ID);
                }
                catch
                {
                    MessageBox.Show("Error contacting database.");
                }
            }
            else
            {
                MessageBox.Show("Invalid ID.");
            }
        }
    }
}
