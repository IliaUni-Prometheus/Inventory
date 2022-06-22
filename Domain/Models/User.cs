using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class User
    {
        private User(Role role, string userName, string passwordHash)
        {
            Role = role;
            UserName = userName;
            PasswordHash = passwordHash;
        }

        public int Id { get; private set; }
        public string UserName { get; private set; }
        public Role Role { get; private set; }
        [JsonIgnore]
        public string PasswordHash { get; private set; }

        public static User CreateAdmin(string passwordash)
        {
            return new User(Role.Admin, "Admin-Test", passwordash);
        }

        public static User CreateCustomer(string passwordash)
        {
            return new User(Role.Customer, "Customer-Test", passwordash);
        }
        public static User CreateSupplier(string passwordash)
        {
            return new User(Role.Supplier, "Supplier-Test", passwordash);
        }
    }
}
