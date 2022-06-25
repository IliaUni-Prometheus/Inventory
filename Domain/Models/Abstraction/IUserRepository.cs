namespace Domain.Models.Abstraction
{
    public interface IUserRepository
    {
        Task<User?> RetrieveByIdAsync(int id);
        Task<User?> RetrieveByUsernameAsync(string userName);
    }
}
