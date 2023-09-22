using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Data.Example
{
    public class StoreDB
    {
        private string connectionString = Settings.Default.StoreDatabase;
        public Product GetProduct(int ID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetProductByID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductID", ID);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    // Create a Product object that wraps the
                    // current record.
                    Product product = new Product((int)reader["ProductID"],
                        (string)reader["ModelNumber"],
                        (string)reader["ModelName"], (decimal)reader["UnitCost"],
                        (string)reader["Description"]);
                    return (product);
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                con.Close();
            }
        }
        internal void UpdateProduct(Product product)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UpdateProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
            cmd.Parameters.AddWithValue("@ModelNumber", product.ModelNumber);
            cmd.Parameters.AddWithValue("@ModelName", product.ModelName);
            cmd.Parameters.AddWithValue("@UnitCost", product.UnitCost);
            cmd.Parameters.AddWithValue("@Description", product.Description);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public ICollection<Product> GetProducts()
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetProducts", con);
            cmd.CommandType =CommandType.StoredProcedure;
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product((int)reader["ProductID"],
                        (string)reader["ModelNumber"], (string)reader["ModelName"],
                        (decimal)reader["UnitCost"], (string)reader["Description"]);
                    products.Add(product); 
                }
            }
            finally
            {
                con.Close();
            }
            return products;
        }

        public DataTable GetProductsADO()
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetProducts", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Products");
            return dataSet.Tables[0];
        }

        public ICollection<Product> GetProductsFilter(int cost)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetProducts", con);
            cmd.CommandType = CommandType.StoredProcedure;
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product((int)reader["ProductID"],
                        (string)reader["ModelNumber"], (string)reader["ModelName"],
                        (decimal)reader["UnitCost"], (string)reader["Description"]);
                    products.Add(product);
                }
            }
            finally
            {
                con.Close();
            }

            //List<Product> matches = new List<Product>();
            //foreach(Product product in products)
            //{
            //    if(product.UnitCost > cost)
            //        matches.Add(product);
            //}

            IEnumerable<Product> matches = from product in products where product.UnitCost > cost select product;

            return matches.ToList();
        }
    }
}
