using Application.Features.EmployeeFeatures.Commands;
using Application.Features.EmployeeFeatures.Queries;
using MediatR;
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

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] AllEmployeesQuery query)
        {
            return Ok(_mediator.Send(query));
        }

        [HttpPut]
        public async Task<IActionResult> ChangeName([FromBody] ChangeEmployeeNameCommand command)
        {
            return Ok(_mediator.Send(command));
        }
    }
}
