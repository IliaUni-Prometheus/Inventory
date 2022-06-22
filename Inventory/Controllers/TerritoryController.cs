using Application.Features.TerritoryFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerritoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        // Constructor
        public TerritoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/territory
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AllTerritoriesQueryResult>))]
        public async Task<IActionResult> GetTerritories()
        {
            var territories = await _mediator.Send(new GetAllTerritoriesQuery());

            return Ok(territories);
        }
    }
}
