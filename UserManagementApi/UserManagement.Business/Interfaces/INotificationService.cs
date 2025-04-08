using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Api.Models;

namespace UserManagement.Business.Interfaces
{
    public interface INotificationService
    {
        bool SendWelcomeEmail(UserModel user);
        bool SendAccountUpdateEmail(UserModel user);
    }
}
