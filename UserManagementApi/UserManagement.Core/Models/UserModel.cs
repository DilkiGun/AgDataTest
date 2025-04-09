using System.Text.Json;
using System.Text.RegularExpressions;

namespace UserManagement.Api.Models
{
    public class UserModel
    {
        public int? Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }

        public UserModel(int? id, string username, string email)
        {
            Id = id;
            Username = username;
            Email = email;
        }
    }
}
