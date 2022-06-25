using Application.Features.EmployeeFeatures.Commands;
using Application.Features.EmployeeFeatures.Queries;
using Inventory.Infrastructure;
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

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<AllEmployeesQueryResult>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] AllEmployeesQuery query)
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
