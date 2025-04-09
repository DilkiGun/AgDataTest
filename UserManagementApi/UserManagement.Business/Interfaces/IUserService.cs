using UserManagement.Api.Models;

namespace UserManagement.Business.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(UserModel user);
        Task<UserModel> GetUserById(int userId);
        Task<bool> UpdateEmail(int userId, string newEmail);
    }
}