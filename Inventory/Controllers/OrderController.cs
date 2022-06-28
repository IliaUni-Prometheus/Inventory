using Application.Features.OrderFeautres.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator) { _mediator = mediator; }

        // GET: api/order
        // this will always return list of orders (but it might be empty)
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(Shared.DTOs.ErrorDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(200, Type = typeof(BrowseResult<OrderDTO>))]
        public async Task<IActionResult> GetOrders([FromQuery] int page = 1, int pageSize = 10)
        {
            return Ok(await _mediator.Send(new GetOrdersQuery(page, pageSize)));
        }

        //GET: api/order/[id]
        [HttpGet("{id:int}", Name = nameof(GetOrder))]
        [ProducesResponseType(200, Type = typeof(OrderDTO))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOrder(int id)
        {
            return Ok(await _mediator.Send(new GetOrderByIdQuery(id))); // 200 OK with orderDTO in body
        }
    }
}
