using Application.Features.ProductFeatures.Commands;
using Application.Features.ProductFeatures.Queries;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator) { _mediator = mediator; }

        // GET: api/products
        // this will always return a list of products (but it might be empty)
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _mediator.Send(new GetProductsQuery());
        }

        //GET: api/products/[id]
        [HttpGet("{id:int}", Name = nameof(GetProduct))]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProduct(int id)
        {
            Product? product = await _mediator.Send(new GetProductByIdQuery(id));
            if (product == null) { return NotFound(); } // 404 Resource not found

            return Ok(product); // 200 OK with product in body
        }

        // POST: api/products
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null) { return BadRequest("Product has to have a body."); }

            Product? inserted = await _mediator.Send(new AddProductCommand(product));
            if (inserted == null) { return BadRequest("Repository failed to create product."); }

            // 201 created
            return CreatedAtRoute(nameof(GetProduct), new { id = inserted.ProductId }, inserted);
        }

        // PUT: api/products/[id]
        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            // 404 bad request
            if (product == null || product.ProductId != id) { return BadRequest(); }

            // Check if the product exists
            Product? existing = await _mediator.Send(new GetProductByIdQuery(id));
            // 404 resource not found
            if (existing == null) { return NotFound(); }

            bool updated = await _mediator.Send(new UpdateProductCommand(product));
            if (!updated) { return BadRequest("Repository failed to update product."); }

            // 204 no content
            return new NoContentResult();
        }

        // DELETE: api/customers/[id]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            bool? deleted = await _mediator.Send(new DeleteProductCommand(id));
            // 404 Resource not found
            if (deleted == null) { return NotFound(); }
            // 404 Resource not found
            if (deleted == false) { return BadRequest($"Customer {id} was found but failed to delete."); }

            // 204 No content
            return new NoContentResult();

        }
    }
}
