using Application.Features.RegionFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IMediator _mediator;

        // Constructor
        public RegionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/region
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AllRegionsQueryResult>))]
        public async Task<IActionResult> GetRegions()
        {
            var regions = await _mediator.Send(new GetAllRegionsQuery());

            return Ok(regions);
        }
    }
}
