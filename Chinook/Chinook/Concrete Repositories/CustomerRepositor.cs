﻿using Chinook.Models;
using Chinook.Repositories;
using Microsoft.Data.SqlClient;

namespace Chinook.Concrete_Repositories
{
    public class CustomerRepositor : ICustomerRepository
    {
        public string ConnectionString { get; set; } = string.Empty;

        public Customer GetById(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer WHERE CustomerId = @id";
            
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            using SqlDataReader reader = command.ExecuteReader();

            var result = new Customer();
            while(reader.Read())
            {
                result = new Customer(
                    reader.GetInt32(0),
                    (!reader.IsDBNull(1) ? reader.GetString(1) : "No first name"),
                    (!reader.IsDBNull(2) ? reader.GetString(2) : "No last name"),
                    (!reader.IsDBNull(3) ? reader.GetString(3) : "No country"),
                    (!reader.IsDBNull(4) ? reader.GetString(4) : "No postal code"),
                    (!reader.IsDBNull(5) ? reader.GetString(5) : "No phone number"),
                    (!reader.IsDBNull(6) ? reader.GetString(6) : "No email")
                    );
            }

            return result;
             
        }

        public IEnumerable<Customer> GetAll()
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer";
            using var command = new SqlCommand(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return new Customer(
                    reader.GetInt32(0),
                    (!reader.IsDBNull(1) ? reader.GetString(1) : "No first name"),
                    (!reader.IsDBNull(2) ? reader.GetString(2) : "No last name"),
                    (!reader.IsDBNull(3) ? reader.GetString(3) : "No country"),
                    (!reader.IsDBNull(4) ? reader.GetString(4) : "No postal code"),
                    (!reader.IsDBNull(5) ? reader.GetString(5) : "No phone number"),
                    (!reader.IsDBNull(6) ? reader.GetString(6) : "No email")
                    );
            }

        }
    }
}
