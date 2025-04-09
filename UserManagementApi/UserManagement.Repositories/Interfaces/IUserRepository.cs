using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Api.Models;
using UserManagement.Repositories.Models;

namespace UserManagement.Repositories.Interfaces
{
    public interface IUserRepository
    {

        Task<bool> Save(UserDto user);
        Task<UserDto?> FindById(int id);


    }
}
