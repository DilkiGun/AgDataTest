using Microsoft.Data.Sqlite;
using UserManagement.Api.Models;
using UserManagement.Repositories.Interfaces;

namespace UserManagement.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly SqliteConnection _connection;

        public UserRepository()
        {
            _connection = new SQLiteConnection("Data Source=database.db");
            _connection.Open();
        }

        public bool Save(UserModel user)
        {
            using var command = _connection.CreateCommand();
            if (!user.Id.HasValue)
            {
                command.CommandText = "INSERT INTO Users (Username, Email) VALUES (@username, @email)";
                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@email", user.Email);
                command.ExecuteNonQuery();
                user.Id = (int)_connection.LastInsertRowId;
            }
            else
            {
                command.CommandText = "UPDATE Users SET Username = @username, Email = @email WHERE Id = @id";
                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@id", user.Id.Value);
                command.ExecuteNonQuery();
            }
            return true;
        }

        public UserModel FindById(int id)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT Username, Email FROM Users WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new UserModel(reader.GetString(0), reader.GetString(1)) { Id = id };
            }
            return null;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }


}
}
