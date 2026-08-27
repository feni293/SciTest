using Microsoft.AspNetCore.Mvc;
using SCITest.Application.DTOs.Products;
using SCITest.Application.Handlers.Commands.Products;
using SCITest.Application.Handlers.Queries.Products;

namespace SCITest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly CreateProductHandler _createProductHandler;
        private readonly UpdateProductHandler _updateProductHandler;
        private readonly DeleteProductHandler _deleteProductHandler;
        private readonly GetProductsHandler _getProductsHandler;
        private readonly GetProductByIdHandler _getProductByIdHandler;

        public ProductController(
            CreateProductHandler createProductHandler,
            UpdateProductHandler updateProductHandler,
            DeleteProductHandler deleteProductHandler,
            GetProductsHandler getProductsHandler,
            GetProductByIdHandler getProductByIdHandler)
        {
            _createProductHandler = createProductHandler;
            _updateProductHandler = updateProductHandler;
            _deleteProductHandler = deleteProductHandler;
            _getProductsHandler = getProductsHandler;
            _getProductByIdHandler = getProductByIdHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var products = await _getProductsHandler.HandleAsync(cancellationToken);

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductResponse>> GetById(int id, CancellationToken cancellationToken)
        {
            var product = await _getProductByIdHandler.HandleAsync(id, cancellationToken);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponse>> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _createProductHandler.HandleAsync(request, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,[FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var updated = await _updateProductHandler.HandleAsync(id, request, cancellationToken);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var deleted = await _deleteProductHandler.HandleAsync(id, cancellationToken);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
