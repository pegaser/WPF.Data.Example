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

namespace WPF.Data.Example
{
    /// <summary>
    /// Lógica de interacción para BindToCollection.xaml
    /// </summary>
    public partial class BindToCollection : Window
    {
        private ICollection<Product> products;
        public BindToCollection()
        {
            InitializeComponent();
            cmdGetProducts.Click += CmdGetProducts_Click;
            cmdDeleteProduct.Click += CmdDeleteProduct_Click;
        }

        private void CmdDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            products.Remove((Product)lstProducts.SelectedItem);
        }

        private void CmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDB.GetProducts();
            lstProducts.DisplayMemberPath = "ModelName";
            lstProducts.ItemsSource = products;
        }
    }
}
