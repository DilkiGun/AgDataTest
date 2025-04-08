using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Api.Models;

namespace UserManagement.Core.Utilities
{
    public class NotificationService
    {
        private readonly Dictionary<string, bool> _settings;

        public NotificationService()
        {
            _settings = new Dictionary<string, bool>
        {
            { "EmailNotifications", true },
            { "SlackNotifications", false }
        };
        }

        public bool SendWelcomeEmail(UserModel user)
        {
            if (!_settings["EmailNotifications"]) return false;

            Console.WriteLine($"Sending welcome email to {user.Email}");
            return true;
        }
    }
}