

using Application.DataContracts.Requests;
using Application.DataContracts.Responses;

namespace Application.Services.Abstract
{
    public interface IUserService
    {
       Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model);
    }
}
