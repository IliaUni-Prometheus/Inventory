﻿using Application.Features.OrderFeautres.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(AllOrdersQueryResult))]
        public async Task<IActionResult> GetOrders([FromQuery] int page = 1, [FromQuery] int itemsPerPage = 10)
        {
            var orders = await _mediator.Send(new GetAllOrdersQuery(page, itemsPerPage));

            return Ok(orders);
        }

        //GET: api/order/[id]
        [HttpGet("{id:int}", Name = nameof(GetOrder))]
        [ProducesResponseType(200, Type = typeof(OrderByIdQueryResult))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOrder(int id)
        {
            OrderByIdQueryResult? order = await _mediator.Send(new GetOrderByIdQuery(id));
            // 404 Resource not found
            if (order == null) { return NotFound(); }

            // 200 OK with order DTO in body
            return Ok(order);
        }
    }
}