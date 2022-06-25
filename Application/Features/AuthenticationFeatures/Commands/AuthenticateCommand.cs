using Application.DataContracts.Requests;
using Application.DataContracts.Responses;
using Application.Services.Abstract;
using Domain.Models.Abstraction;
using static Shared.CQRSInfrastructure;

namespace Application.Features.AuthenticationFeatures.Commands
{
    /// <summary>
    /// Command
    /// </summary>
    public class AuthenticateCommand : ICommand<AuthenticateResponse>
    {
        // Request params
        public AuthenticateRequest UserModel { get; private set; }

        // Constructor
        public AuthenticateCommand(AuthenticateRequest userModel)
        {
            this.UserModel = userModel;
        }
    }

    /// <summary>
    /// Command handler
    /// </summary>
    public class AuthenticateHandler : ICommandHandler<AuthenticateCommand, AuthenticateResponse>
    {
        private IUserRepository _repo;
        private IJwtUtils _jwtUtils;

        // Constructor
        public AuthenticateHandler(IUserRepository repo, IJwtUtils jwtUtils) 
        {
            this._repo = repo;
            this._jwtUtils = jwtUtils;
        }

        public async Task<AuthenticateResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var user = await _repo.RetrieveByUsernameAsync(request.UserModel.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.UserModel.Password, user.PasswordHash))
            {
                throw new Exception("Username and password not found");
            }

            var jwtToken = await _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse() { Username = user.UserName, Role = user.Role, AccessToken = jwtToken };
        }
    }
}
