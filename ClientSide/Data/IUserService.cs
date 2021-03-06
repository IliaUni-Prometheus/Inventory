using ClientSide.Models;

namespace ClientSide.Data
{
    public interface IUserService
    {
        Task<SignInViewModel> SignIn(SignInViewModel model);
        Task LogOut();
    }
}
