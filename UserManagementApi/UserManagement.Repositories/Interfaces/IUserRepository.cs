using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Api.Models;

namespace UserManagement.Repositories.Interfaces
{
    public interface IUserRepository
    {
        bool Save(UserModel user);
        UserModel FindById(int id);

    }
}
