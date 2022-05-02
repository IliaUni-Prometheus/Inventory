using System.ComponentModel.DataAnnotations;


namespace Application.DataContracts.Requests
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
