using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public Role Role { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
