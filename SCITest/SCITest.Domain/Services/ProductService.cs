using SCITest.Application.Interfaces.Repositories;
using SCITest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCITest.Domain.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken)
        {
            ValidateProduct(product);

            return await _productRepository.CreateAsync(product, cancellationToken);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return await _productRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _productRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            return await _productRepository.UpdateAsync(product, cancellationToken);
        }

        private static void ValidateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name is required.");

            if (product.Name.Length > 200)
                throw new ArgumentException(
                    "Product name cannot exceed 200 characters.");

            if (product.Description?.Length > 1000)
                throw new ArgumentException(
                    "Product description cannot exceed 1000 characters.");

            if (product.Price <= 0)
                throw new ArgumentException(
                    "Product price must be greater than zero.");
        }
    }
}
