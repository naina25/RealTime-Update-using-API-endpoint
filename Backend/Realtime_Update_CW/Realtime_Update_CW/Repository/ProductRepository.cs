using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Realtime_Update_CW.Models;
using RealtimeAPI;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace Realtime_Update_CW.Repository
{
    public class ProductRepository : IProductRepository
    {
        string connectionString = "";
        private readonly IHubContext<SignalServer> _context;

        public ProductRepository(IConfiguration configuration, IHubContext<SignalServer> context)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");

            SqlTableDependency<Product> dep = new SqlTableDependency<Product>(connectionString, tableName: "Products");

            dep.OnChanged += Products_Changed;
            dep.Start();
            _context = context;
        }

        private void Products_Changed(object sender, RecordChangedEventArgs<Product> e)
        {
            _context.Clients.All.SendAsync("NewData");
        }

        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "select * from Products";
                SqlCommand cmd = new SqlCommand(query, con);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var product = new Product
                    {
                        Product_id = Convert.ToInt32(reader["Product_id"]),
                        Product_name = reader["Product_name"].ToString(),
                        Product_price = Convert.ToInt32(reader["Product_price"])
                    };

                    products.Add(product);
                }
                con.Close();
            }

            return products;
        }
    }
}
