using SCITest.Application.DTOs.Products;
using SCITest.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCITest.Application.Handlers.Queries.Products
{
    public class GetProductByIdHandler
    {
        private readonly ProductService _productService;

        public GetProductByIdHandler(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<ProductResponse?> HandleAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _productService.GetByIdAsync(id, cancellationToken);

            if (product is null)
                return null;

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
