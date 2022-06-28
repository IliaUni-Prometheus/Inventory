using Application.Features.EmployeeFeatures.Queries;
using Application.Features.OrderFeautres.Queries;
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
        [HttpGet]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(200, Type = typeof(BrowseResult<EmployeeDTO>))]
        public async Task<IActionResult> GetEmployees([FromQuery] int page = 1)
        {
            return Ok(await _mediator.Send(new GetEmployeesQuery(page)));
        }

        //[HttpPut]
        //public async Task<IActionResult> ChangeName([FromBody] ChangeEmployeeNameCommand command)
        //{
        //    return Ok(await _mediator.Send(command));
        //}
    }
}
