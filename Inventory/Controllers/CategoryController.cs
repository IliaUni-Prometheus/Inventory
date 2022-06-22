using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        // Constructor
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
