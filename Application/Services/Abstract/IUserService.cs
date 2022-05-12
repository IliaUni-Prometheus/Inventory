

using Application.DataContracts.Requests;
using Application.DataContracts.Responses;
using Domain.Models;

namespace Application.Services.Abstract
{
    public interface IUserService
    {
       Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model);
        Task<User> GetById(int value);
    }
}
