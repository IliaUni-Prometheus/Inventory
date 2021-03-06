
using Domain.Models;

namespace Application.DataContracts.Responses
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public string AccessToken { get; set; }
        public AuthenticateResponse()
        {

        }   
        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Role = user.Role;
            AccessToken = token;
        }
    }
}
