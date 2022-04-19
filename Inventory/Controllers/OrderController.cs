using Application.Features.OrderFeatures.Commands;
using Application.Features.OrderFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AllOrdersQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPut]
        public async Task<IActionResult> Put(ChangeOrderShipNameCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}
