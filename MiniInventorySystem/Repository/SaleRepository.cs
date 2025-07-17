using MiniInventorySystem.Interface;
using MiniInventorySystem.Models;
using System.Data.SqlClient;
using System.Data;

namespace MiniInventorySystem.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly IConfiguration _configuration;

        public SaleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Sale> GetAllSales()
        {
            var sales = new List<Sale>();

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("GetAllSalesWithDetails", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                sales.Add(new Sale
                {
                    SaleId = reader.GetInt32(reader.GetOrdinal("SaleId")),
                    CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                    CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                    SaleDate = reader.GetDateTime(reader.GetOrdinal("SaleDate"))
                });
            }

            return sales;
        }

        public Sale GetSaleById(int saleId)
        {
            var sale = new Sale();

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("GetSaleByIdWithDetails", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SaleId", saleId);

            conn.Open();

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                sale.SaleId = reader.GetInt32(reader.GetOrdinal("SaleId"));
                sale.CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId"));
                sale.CustomerName = reader.GetString(reader.GetOrdinal("CustomerName"));
                sale.SaleDate = reader.GetDateTime(reader.GetOrdinal("SaleDate"));
            }

            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    sale.Details.Add(new SaleDetail
                    {
                        SaleDetailId = reader.GetInt32(reader.GetOrdinal("SaleDetailId")),
                        SaleId = reader.GetInt32(reader.GetOrdinal("SaleId")),
                        ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                        ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                        Quantity = reader.GetInt32(reader.GetOrdinal("Quantity"))
                    });
                }
            }

            return sale;
        }

        public void InsertSale(Sale sale)
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            conn.Open();

            foreach (var detail in sale.Details)
            {
                using var cmd = new SqlCommand("InsertSaleWithDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerId", sale.CustomerId);
                cmd.Parameters.AddWithValue("@SaleDate", sale.SaleDate);
                cmd.Parameters.AddWithValue("@ProductId", detail.ProductId);
                cmd.Parameters.AddWithValue("@Quantity", detail.Quantity);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateSale(Sale sale)
        {
            var dt = CreateSaleDetailDataTable(sale.Details);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("UpdateSaleWithDetails", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SaleId", sale.SaleId);
            cmd.Parameters.AddWithValue("@CustomerId", sale.CustomerId);
            cmd.Parameters.AddWithValue("@SaleDate", sale.SaleDate);
            cmd.Parameters.AddWithValue("@SaleDetails", dt);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteSale(int saleId)
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("DeleteSale", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SaleId", saleId);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        private DataTable CreateSaleDetailDataTable(List<SaleDetail> details)
        {
            var table = new DataTable();
            table.Columns.Add("ProductId", typeof(int));
            table.Columns.Add("Quantity", typeof(int));

            foreach (var detail in details)
            {
                table.Rows.Add(detail.ProductId, detail.Quantity);
            }

            return table;
        }
    }

}
