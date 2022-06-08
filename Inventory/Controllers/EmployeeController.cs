using Application.Features.EmployeeFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator) { _mediator = mediator; }

        // GET: api/employee
        // this will always return list of employees (but it might be empty)
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(BrowseResult<EmployeeDTO>))]
        public async Task<IActionResult> GetOrders([FromQuery] int page = 1)
        {
            var orders = await _mediator.Send(new GetEmployeesQuery(page));

            return Ok(orders);
        }
    }
}
