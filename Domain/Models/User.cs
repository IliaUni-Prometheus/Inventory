using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class User
    {
        public User() { }

        private User(Role role, string userName, string passworHash)
        {
            Role = role;
            UserName = userName;
            PasswordHash = passworHash;
        }

        public int Id { get; private set; }

        public string UserName { get; private set; }

        public Role Role { get; private set; }

        [JsonIgnore]
        public string PasswordHash { get; private set; }


        public static User CreateAdmin(string passworHash)
        {
            return new User(Role.Admin, "Admin-Test", passworHash);
        }

        public static User CreateCustomer(string passworHash)
        {
            return new User(Role.Customer, "Customer-Test", passworHash);
        }
    }
}
