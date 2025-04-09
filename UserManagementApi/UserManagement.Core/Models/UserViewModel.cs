using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Api.Models;

namespace UserManagement.Core.Models
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }

        public static UserViewModel FromModel(UserModel user)
        {
            return new UserViewModel
            {
                Username = user.Username,
                Email = user.Email,
                IsAdmin = user.Email.EndsWith("@admin.com")
            };
        }
    }
}
