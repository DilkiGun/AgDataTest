using UserManagement.Api.Models;

namespace UserManagement.Business.Interfaces
{
    public interface IUserService
    {
        UserModel CreateUser(string username, string email);
        UserModel GetUserById(int userId);
        bool UpdateEmail(int userId, string newEmail);
    }
}