using Application.DataContracts.Requests;
using Application.Features.AuthenticationFeatures.Commands;
using Inventory.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator) { this._mediator = mediator; }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _mediator.Send(new AuthenticateCommand(model));
            return Ok(response);
        }
    }
}
