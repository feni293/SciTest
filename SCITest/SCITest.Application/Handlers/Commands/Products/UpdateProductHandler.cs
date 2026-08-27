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
    public class UpdateProductHandler
    {
        private readonly ProductService _productService;

        public UpdateProductHandler(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<bool> HandleAsync(int id, UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            return await _productService.UpdateAsync(product, cancellationToken);
        }
    }
}
