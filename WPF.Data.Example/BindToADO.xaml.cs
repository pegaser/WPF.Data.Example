using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para BindToADO.xaml
    /// </summary>
    public partial class BindToADO : Window
    {
        private DataTable products;
        public BindToADO()
        {
            InitializeComponent();
            cmdGetProducts.Click += CmdGetProducts_Click;
            cmdDeleteProduct.Click += CmdDeleteProduct_Click;
            cmdGetProductFilter.Click += CmdGetProductFilter_Click;
        }

        private void CmdGetProductFilter_Click(object sender, RoutedEventArgs e)
        {
            ICollection<Product> products = App.StoreDB.GetProductsFilter(1);
            lstProducts.DisplayMemberPath = "ModelName";
            lstProducts.ItemsSource = products;
        }

        private void CmdDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            //products.Rows.Remove((DataRow)lstProducts.SelectedItem);
            ((DataRowView)lstProducts.SelectedItem).Row.Delete();
        }

        private void CmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDB.GetProductsADO();
            lstProducts.DisplayMemberPath = "ModelName";
            lstProducts.ItemsSource = products.DefaultView;
        }
    }
}
