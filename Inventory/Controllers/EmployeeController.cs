using Application.Features.EmployeeFeatures.Queries;
using Inventory.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<AllEmployeesQueryResult>), StatusCodes.Status200OK)]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(BrowseResult<EmployeeDTO>))]
        public async Task<IActionResult> GetOrders([FromQuery] int page = 1)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPut]
        public async Task<IActionResult> ChangeName([FromBody] ChangeEmployeeNameCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
