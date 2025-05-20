using Core.Core.ModelsSasha;
using Microsoft.Data.SqlClient;
using SQLServer.DatabaseContext;
using SQLServer.Repository.Base;

namespace SQLServer.Repository.RepositorySasha
{
    public class UsersRepository : BaseRepository
    {
        public UsersRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public List<Users> GetAllUsers()
        {
            var users = new List<Users>();
            const string query = "SELECT UserID, Login, Password FROM Users";

            using var reader = ExecuteReader(query);
            while (reader.Read())
            {
                users.Add(MapUserFromReader(reader));
            }
            return users;
        }

        public void AddUser(Users user)
        {
            const string query = @"INSERT INTO Users (Login, Password) 
                                VALUES (@Login, @Password)";

            var parameters = new[]
            {
                new SqlParameter("@Login", user.Login),
                new SqlParameter("@Password", user.Password)
            };

            ExecuteNonQuery(query, parameters);
        }

        public Users? CheckUser(Users user)
        {
            const string query = @"SELECT *
                                FROM Users 
                                WHERE Login = @Login AND Password = @Password";

            var parameters = new[]
            {
                new SqlParameter("@Login", user.Login),
                new SqlParameter("@Password", user.Password)
            };

            using var reader = ExecuteReader(query, parameters);
            return reader.Read() ? MapUserFromReader(reader) : null;
        }

        public void UpdateUser(Users user)
        {
            const string query = @"UPDATE Users 
                                SET Login = @Login,
                                    Password = @Password
                                WHERE UserID = @UserID";

            var parameters = new[]
            {
                new SqlParameter("@UserID", user.UserID),
                new SqlParameter("@Login", user.Login),
                new SqlParameter("@Password", user.Password)
            };

            ExecuteNonQuery(query, parameters);
        }

        public void DeleteUser(int userId)
        {
            const string query = "DELETE FROM Users WHERE UserID = @UserID";
            var parameters = new[] { new SqlParameter("@UserID", userId) };
            ExecuteNonQuery(query, parameters);
        }

        public Users? GetUserById(Users users)
        {
            const string query = "SELECT UserID, Login, Password FROM Users WHERE UserID = @UserID";
            var parameters = new[] { new SqlParameter("@UserID", users.UserID) };

            using var reader = ExecuteReader(query, parameters);
            return reader.Read() ? MapUserFromReader(reader) : null;
        }
        public Users? GetUserById(Employee employee)
        {
            const string query = "SELECT UserID, Login, Password FROM Users WHERE UserID = @UserID";
            var parameters = new[] { new SqlParameter("@UserID", employee.UsersID) };

            using var reader = ExecuteReader(query, parameters);
            return reader.Read() ? MapUserFromReader(reader) : null;
        }

        public Users? GetUserByLogin(Users user)
        {
            const string query = @"SELECT *
                                FROM Users 
                                WHERE Login = @Login AND Password = @Password";

            var parameters = new[]
            {
                new SqlParameter("@Login", user.Login),
                new SqlParameter("@Password", user.Password)
            };

            using var reader = ExecuteReader(query, parameters);
            return reader.Read() ? MapUserFromReader(reader) : null;
        }

        private Users MapUserFromReader(SqlDataReader reader)
        {
            return new Users(
                userID: reader.GetInt32(reader.GetOrdinal("UserID")),
                login: reader.GetString(reader.GetOrdinal("Login")),
                password: reader.GetString(reader.GetOrdinal("Password"))
            );
        }
    }
}