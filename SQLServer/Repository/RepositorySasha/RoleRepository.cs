using Core.Core.ModelsSasha;
using Microsoft.Data.SqlClient;
using SQLServer.DatabaseContext;
using SQLServer.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServer.Repository.RepositorySasha
{
    public class RoleRepository : BaseRepository
    {
        public RoleRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public List<Role> GetAllRoles()
        {
            var roles = new List<Role>();
            string query = "SELECT * FROM Role";

            using var reader = ExecuteReader(query);
            while (reader.Read())
            {
                roles.Add(new Role(
                    roleId: reader.GetInt32(0),
                    roleName: reader.GetString(1)
                ));
            }
            return roles;
        }

        public void AddRole(Role role)
        {
            string query = "INSERT INTO Role (RoleName) VALUES (@RoleName)";

            var parameters = new[]
            {
                new SqlParameter("@RoleName", role.RoleName)
            };

            ExecuteNonQuery(query, parameters);
        }

        public void UpdateRole(Role role)
        {
            string query = @"UPDATE Role SET
                          RoleName = @RoleName
                          WHERE RoleID = @RoleID";

            var parameters = new[]
            {
                new SqlParameter("@RoleID", role.RoleID),
                new SqlParameter("@RoleName", role.RoleName)
            };

            ExecuteNonQuery(query, parameters);
        }

        public void DeleteRole(int roleId)
        {
            string query = "DELETE FROM Role WHERE RoleID = @RoleID";
            var parameters = new[] { new SqlParameter("@RoleID", roleId) };
            ExecuteNonQuery(query, parameters);
        }

        public Role? GetRoleById(ProjectAssignment projectAssignment)
        {
            string query = "SELECT * FROM Role WHERE ID = @ID";
            var parameters = new[] { new SqlParameter("@ID", projectAssignment.RoleID) };

            using var reader = ExecuteReader(query, parameters);
            if (reader.Read())
            {
                return new Role(
                    roleId: reader.GetInt32(0),
                    roleName: reader.GetString(1)
                );
            }
            return null;
        }

        public Role? GetRoleByName(string roleName)
        {
            string query = "SELECT * FROM Role WHERE RoleName = @RoleName";
            var parameters = new[] { new SqlParameter("@RoleName", roleName) };

            using var reader = ExecuteReader(query, parameters);
            if (reader.Read())
            {
                return new Role(
                    roleId: reader.GetInt32(0),
                    roleName: reader.GetString(1)
                );
            }
            return null;
        }
    }
}
