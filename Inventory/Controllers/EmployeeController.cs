using Application.Features.EmployeeFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator) { _mediator = mediator; }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AllEmployeesQueryResult>))]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _mediator.Send(new GetAllEmployeesQuery());

            return Ok(orders);
        }
    }
}
