using Application.Features.SupplierFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IMediator _mediator;

        // Constructor
        public SupplierController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/supplier
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AllSuppliersQueryResult>))]
        public async Task<IActionResult> GetSuppliers()
        {
            var suppliers = await _mediator.Send(new GetAllSuppliersQuery());

            return Ok(suppliers);
        }
    }
}
