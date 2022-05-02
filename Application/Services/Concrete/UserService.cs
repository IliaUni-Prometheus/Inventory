using Application.DataContracts.Requests;
using Application.DataContracts.Responses;
using Application.Services.Abstract;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly NorthwindContext _db;
        private IJwtUtils _jwtUtils;

        public UserService(NorthwindContext db, IJwtUtils jwtUtils)
        {
            _db = db;
            _jwtUtils = jwtUtils;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model)
        {
            var user = await _db.Users.FirstOrDefaultAsync(a => a.UserName == model.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                throw new Exception("Username and password not found");
            }

            var jwtToken = await _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse() { Role = user.Role, Token = jwtToken };
        }
    }
}
