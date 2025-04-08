using UserManagement.Api.Models;
using UserManagement.Api.Utilities;
using UserManagement.Business.Interfaces;
using UserManagement.Core.Utilities;
using UserManagement.Repositories.Interfaces;

namespace UserManagement.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public UserModel CreateUser(string username, string email)
        {
            var user = new UserModel(username, email);
            return _repository.Save(user) ? user : null;
        }

        public UserModel GetUserById(int userId)
        {
            return _repository.FindById(userId);
        }

        public bool UpdateEmail(int userId, string newEmail)
        {
            var user = _repository.FindById(userId);
            if (user == null) return false;

            user.Email = newEmail;
            return _repository.Save(user);
        }
    }
}
