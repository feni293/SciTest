using SCITest.Application.DTOs.Products;
using SCITest.Domain.Entities;
using SCITest.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCITest.Application.Handlers.Commands.Products
{
    public class CreateProductHandler
    {
        private readonly ProductService _productService;

        public CreateProductHandler(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<ProductResponse> HandleAsync(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            var createdProduct = await _productService.CreateAsync(product, cancellationToken);

            return MapToResponse(createdProduct);
        }

        private static ProductResponse MapToResponse(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CreatedDate = product.CreatedDate
            };
        }
    }
}
