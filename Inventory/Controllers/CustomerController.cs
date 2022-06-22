using Application.Features.CustomerFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        // Constructor
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/customer
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AllCustomersQueryResult>))]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _mediator.Send(new GetAllCustomersQuery());

            return Ok(customers);
        }
    }
}
