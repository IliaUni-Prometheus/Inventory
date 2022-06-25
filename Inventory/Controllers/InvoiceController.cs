using Application.Features.InvoiceFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        // Constructor
        public InvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/invoice
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AllInvoicesQueryResult>))]
        public async Task<IActionResult> GetInvoices()
        {
            var invoices = await _mediator.Send(new GetAllInvoicesQuery());

            return Ok(invoices);
        }
    }
}
