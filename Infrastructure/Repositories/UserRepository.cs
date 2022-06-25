using Domain.Models;
using Domain.Models.Abstraction;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NorthwindContext _db;

        // Constructor
        public UserRepository(NorthwindContext db)
        {
            _db = db;
        }

        public async Task<User?> RetrieveByIdAsync(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        public async Task<User?> RetrieveByUsernameAsync(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
