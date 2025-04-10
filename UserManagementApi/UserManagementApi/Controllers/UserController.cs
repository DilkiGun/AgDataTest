using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.Models;
using UserManagement.Business.Interfaces;
using UserManagement.Core.Models;
using UserManagement.Core.Utilities;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;

        public UserController(IUserService userService, INotificationService notificationService)
        {
            _userService = userService;           
            _notificationService = notificationService;
        }

        private static readonly List<UserModel> Users = new()
        {
            new UserModel ( 1,  "Alice","alice@example.com" ),
            new UserModel ( 2,  "Bob", "bob@example.com"),
            new UserModel ( 3, "Charlie",  "charlie@example.com" ),
            new UserModel ( 4,  "AdminUser",  "admin@admin.com" )
        };

        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> GetUsers()
        {
            return Ok(Users);
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(UserModel user)
        {
            if (user.Username.Length < 3)
            {
                return BadRequest(new { Success = false, Error = "Username too short" });
            }

            var isUserCreated = await _userService.CreateUser(user);
            if (isUserCreated)
            {
                _notificationService.SendWelcomeEmail(user);
                return Ok(new { Success = true, Data = user });
            }

            return BadRequest(new { Success = false, Error = "Failed to save user" });
        }

        [HttpGet("profile/{userId}")]
        public async Task<IActionResult> GetUserProfile(int userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user != null)
            {
                return Ok(new { Success = true, Data = UserViewModel.FromModel(user) });
            }

            return NotFound(new { Success = false, Error = "User not found" });
        }

        [HttpPost("update-email")]
        public async Task<IActionResult> UpdateEmail(int userId, string newEmail)
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return BadRequest(new { Success = false, Error = "No user selected" });
            }

            if (!await _userService.UpdateEmail(userId, newEmail))
            {
                return BadRequest(new { Success = false, Error = "Failed to update email" });
            }

            return Ok(new { Success = true, Html = "_view.DisplayUserProfile(user)" });
        }
    }

}
