using MiniInventorySystem.Interface;
using MiniInventorySystem.Models;
using System.Data.SqlClient;
using System.Data;

namespace MiniInventorySystem.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new();

            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("sp_GetCustomers", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                customers.Add(new Customer
                {
                    CustomerId = (int)dr["Id"],
                    CustomerName = dr["Name"].ToString(),
                    Address = dr["Address"].ToString(),
                    ContactNo = dr["ContactNo"].ToString()
                });
            }
            return customers;
        }

        public Customer GetCustomerById(int id)
        {
            Customer customer = null;

            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("sp_GetCustomerById", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if ((int)dr["Id"] == id)
                {
                    customer = new Customer
                    {
                        CustomerId = id,
                        CustomerName = dr["Name"].ToString(),
                        Address = dr["Address"].ToString(),
                        ContactNo = dr["ContactNo"].ToString()
                    };
                    break;
                }
            }
            return customer;
        }

        public void InsertCustomer(Customer customer)
        {
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("sp_AddCustomer", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", customer.CustomerName);
            cmd.Parameters.AddWithValue("@Address", customer.Address);
            cmd.Parameters.AddWithValue("@ContactNo", customer.ContactNo);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdateCustomer(Customer customer)
        {
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("sp_UpdateCustomer", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", customer.CustomerId);
            cmd.Parameters.AddWithValue("@Name", customer.CustomerName);
            cmd.Parameters.AddWithValue("@Address", customer.Address);
            cmd.Parameters.AddWithValue("@ContactNo", customer.ContactNo);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteCustomer(int id)
        {
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("sp_DeleteCustomer", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
