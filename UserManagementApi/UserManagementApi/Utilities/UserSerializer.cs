using System.Text.Json;
using UserManagement.Api.Models;

namespace UserManagement.Api.Utilities
{
    public static class UserSerializer
    {
        public static string Serialize(UserModel user)
        {
            var data = new
            {
                user.Id,
                user.Username,
                user.Email,
                IsAdmin = user.Email.EndsWith("@admin.com")
            };
            return JsonSerializer.Serialize(data);
        }

    }
}
