using Application.DataContracts.Requests;
using Application.DataContracts.Responses;
using static Shared.CQRSInfrastructure;

namespace Application.Features.AuthenticationFeatures.Commands
{
    public class AuthenticateCommand : ICommand<AuthenticateResponse>
    {
        public AuthenticateRequest UserModel { get; private set; }

        public AuthenticateCommand(AuthenticateRequest userModel) { this.UserModel = userModel; }
    }
}
