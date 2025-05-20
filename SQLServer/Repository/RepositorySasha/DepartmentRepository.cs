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
    public class DepartmentRepository : BaseRepository
    {
        public DepartmentRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public List<Department> GetAllDepartments()
        {
            var departments = new List<Department>();
            string query = "SELECT * FROM Department";

            using var reader = ExecuteReader(query);
            while (reader.Read())
            {
                departments.Add(new Department(
                    departmentId: reader.GetInt32(0),
                    departmentName: reader.GetString(1)
                ));
            }
            return departments;
        }

        public void AddDepartment(Department department)
        {
            string query = "INSERT INTO Department (DepartmentName) VALUES (@DepartmentName)";

            var parameters = new[]
            {
                new SqlParameter("@DepartmentName", department.DepartmentName)
            };

            ExecuteNonQuery(query, parameters);
        }

        public void UpdateDepartment(Department department)
        {
            string query = @"UPDATE Department 
                           SET DepartmentName = @DepartmentName
                           WHERE DepartmentID = @DepartmentID";

            var parameters = new[]
            {
                new SqlParameter("@DepartmentID", department.DepartmentID),
                new SqlParameter("@DepartmentName", department.DepartmentName)
            };

            ExecuteNonQuery(query, parameters);
        }

        public void DeleteDepartment(int departmentId)
        {
            string query = "DELETE FROM Department WHERE DepartmentID = @DepartmentID";
            var parameters = new[] { new SqlParameter("@DepartmentID", departmentId) };
            ExecuteNonQuery(query, parameters);
        }

        public Department? GetDepartmentById(Position position)
        {
            string query = "SELECT * FROM Department WHERE ID = @ID";
            var parameters = new[] { new SqlParameter("@ID", position.DepartmentID) };

            using var reader = ExecuteReader(query, parameters);
            if (reader.Read())
            {
                return new Department(
                    departmentId: reader.GetInt32(0),
                    departmentName: reader.GetString(1)
                );
            }
            return null;
        }
    }
}
