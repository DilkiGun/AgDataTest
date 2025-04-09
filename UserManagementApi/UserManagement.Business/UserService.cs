using UserManagement.Api.Models;
using UserManagement.Api.Utilities;
using UserManagement.Business.Interfaces;
using UserManagement.Core.Utilities;
using UserManagement.Repositories.Interfaces;
using UserManagement.Repositories.Models;

namespace UserManagement.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateUser(UserModel user)
        {
            UserDto userDto = new UserDto() { Email = user.Email, Username = user.Username };
            return await _repository.Save(userDto);
        }

        public async Task<UserModel> GetUserById(int userId)
        {
            UserDto user = await _repository.FindById(userId);
            return new UserModel(userId, user.Username, user.Email);
        }

        public async Task<bool> UpdateEmail(int userId, string newEmail)
        {
            var user = await _repository.FindById(userId);
            if (user == null) return false;

            user.Email = newEmail;
            return await _repository.Save(user);
        }
    }
}
