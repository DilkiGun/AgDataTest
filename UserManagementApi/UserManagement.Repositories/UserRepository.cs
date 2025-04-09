using Microsoft.Data.Sqlite;
using UserManagement.Api.Models;
using UserManagement.Repositories.Interfaces;
using UserManagement.Repositories.Models;

namespace UserManagement.Repositories
{
    public class UserRepository: IUserRepository
    {
          private readonly UserDbContext _context;

            public UserRepository(UserDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Save(UserDto user)
            {
                if (user.Id == 0)
                {
                    _context.Users.Add(user);
                }
                else
                {
                    _context.Users.Update(user);
                }

                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<UserDto?> FindById(int id)
            {
                return await _context.Users.FindAsync(id);
            }
        }

    }
