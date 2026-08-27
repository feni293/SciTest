using SCITest.Application.DTOs.Products;
using SCITest.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCITest.Application.Handlers.Queries.Products
{
    public class GetProductsHandler
    {
        private readonly ProductService _productService;

        public GetProductsHandler(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IEnumerable<ProductResponse>> HandleAsync(CancellationToken cancellationToken)
        {
            var products = await _productService.GetAllAsync(cancellationToken);

            return products.Select(product => new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CreatedDate = product.CreatedDate
            });
        }
    }
}
