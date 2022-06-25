using Application.Features.ShipperFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IMediator _mediator;

        // Constructor
        public ShipperController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/shipper
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AllShippersQueryResult>))]
        public async Task<IActionResult> GetShippers()
        {
            var shippers = await _mediator.Send(new GetAllShippersQuery());

            return Ok(shippers);
        }
    }
}
