using Microsoft.Data.SqlClient;
using SCITest.Application.Interfaces.Repositories;
using SCITest.Domain.Entities;
using SCITest.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCITest.Infrastructure.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ProductRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken)
        {
            await using var connection = (SqlConnection)_connectionFactory.CreateConnection();

            await connection.OpenAsync(cancellationToken);

            await using var command = new SqlCommand("dbo.sp_Product_Create", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = product.Name;
            command.Parameters.Add("@Description", SqlDbType.NVarChar, 1000).Value = (object?)product.Description ?? DBNull.Value;
            command.Parameters.Add("@Price", SqlDbType.Decimal).Value = product.Price;

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);

            if (!await reader.ReadAsync(cancellationToken))
            {
                throw new InvalidOperationException( "The product could not be created");
            }

            return MapProduct(reader);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await using var connection = (SqlConnection)_connectionFactory.CreateConnection();

            await connection.OpenAsync(cancellationToken);

            await using var command = new SqlCommand("dbo.sp_Product_Delete", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            var result = await command.ExecuteScalarAsync(cancellationToken);

            return result is not null && Convert.ToBoolean(result);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = new List<Product>();

            await using var connection = (SqlConnection)_connectionFactory.CreateConnection();

            await connection.OpenAsync(cancellationToken);

            await using var command = new SqlCommand("dbo.sp_Product_GetAll", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);

            while (await reader.ReadAsync(cancellationToken))
            {
                products.Add(MapProduct(reader));
            }

            return products;
        }

        public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            await using var connection = (SqlConnection)_connectionFactory.CreateConnection();

            await connection.OpenAsync(cancellationToken);

            await using var command = new SqlCommand("dbo.sp_Product_GetById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);

            if (!await reader.ReadAsync(cancellationToken))
            {
                return null;
            }

            return MapProduct(reader);
        }

        public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            await using var connection = (SqlConnection)_connectionFactory.CreateConnection();

            await connection.OpenAsync(cancellationToken);

            await using var command = new SqlCommand("dbo.sp_Product_Update", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Id", SqlDbType.Int).Value = product.Id;
            command.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = product.Name;
            command.Parameters.Add("@Description", SqlDbType.NVarChar, 1000).Value = (object?)product.Description ?? DBNull.Value;
            command.Parameters.Add("@Price", SqlDbType.Decimal).Value = product.Price;

            var result = await command.ExecuteScalarAsync(cancellationToken);

            return result is not null && Convert.ToBoolean(result);
        }

        private static Product MapProduct(SqlDataReader reader)
        {
            return new Product
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
            };
        }
    }
}
