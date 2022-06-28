using Application.Features.OrderFeautres.Queries;
using Inventory.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(400, Type = typeof(ErrorDetails))]
        [ProducesResponseType(200, Type = typeof(BrowseResult<OrderDTO>))]
        public async Task<IActionResult> GetOrders([FromQuery] int page = 1, int pageSize = 10)
        {
            var orders = await _mediator.Send(new GetOrdersQuery(page, pageSize));

            return Ok(orders);
        }

        //GET: api/order/[id]
        [HttpGet("{id:int}", Name = nameof(GetOrder))]
        [ProducesResponseType(200, Type = typeof(OrderDTO))]
        [ProducesResponseType(400, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetOrder(int id)
        {
            OrderDTO? order = await _mediator.Send(new GetOrderByIdQuery(id));
            if (order == null) { return NotFound(); } // 404 Resource not found

            return Ok(order); // 200 OK with orderDTO in body
        }
    }
}
