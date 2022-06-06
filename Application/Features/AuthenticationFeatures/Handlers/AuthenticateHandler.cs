using Application.DataContracts.Responses;
using Application.Features.AuthenticationFeatures.Commands;
using Application.Services.Abstract;
using static Shared.CQRSInfrastructure;

namespace Application.Features.AuthenticationFeatures.Handlers
{
    public class AuthenticateHandler : ICommandHandler<AuthenticateCommand, AuthenticateResponse>
    {
        private IUserService _userService;

        public AuthenticateHandler(IUserService userService) { this._userService = userService; }

        public async Task<AuthenticateResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            return await _userService.AuthenticateAsync(request.UserModel);
        }
    }
}
