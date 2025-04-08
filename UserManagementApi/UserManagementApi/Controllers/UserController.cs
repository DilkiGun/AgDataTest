using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.Models;
using UserManagement.Business.Interfaces;
using UserManagement.Core.Utilities;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserView _view;
        private readonly INotificationService _notificationService;

        public UserController(IUserService userService, IUserView view, INotificationService notificationService)
        {
            _userService = userService;
            _view = view;
            _notificationService = notificationService;
        }

        [HttpPost("create")]
        public IActionResult CreateUser(string username, string email)
        {
            if (username.Length < 3)
            {
                return BadRequest(new { Success = false, Error = "Username too short" });
            }

            var user = _userService.CreateUser(username, email);
            if (user != null)
            {
                _notificationService.SendWelcomeEmail(user);
                return Ok(new { Success = true, Html = _view.DisplayUserProfile(user) });
            }

            return BadRequest(new { Success = false, Error = "Failed to save user" });
        }

        [HttpGet("profile/{userId}")]
        public IActionResult GetUserProfile(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user != null)
            {
                return Ok(new { Success = true, Html = _view.DisplayUserProfile(user) });
            }

            return NotFound(new { Success = false, Error = "User not found" });
        }

        [HttpPost("update-email")]
        public IActionResult UpdateEmail(int userId, string newEmail)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return BadRequest(new { Success = false, Error = "No user selected" });
            }

            if (!_userService.UpdateEmail(userId, newEmail))
            {
                return BadRequest(new { Success = false, Error = "Failed to update email" });
            }

            return Ok(new { Success = true, Html = _view.DisplayUserProfile(user) });
        }
    }

}
