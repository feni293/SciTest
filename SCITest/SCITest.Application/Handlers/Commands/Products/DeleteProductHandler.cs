using SCITest.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCITest.Application.Handlers.Commands.Products
{
    public class DeleteProductHandler
    {
        private readonly ProductService _productService;

        public DeleteProductHandler(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<bool> HandleAsync(int id, CancellationToken cancellationToken)
        {
            return await _productService.DeleteAsync(id, cancellationToken);
        }
    }
}
