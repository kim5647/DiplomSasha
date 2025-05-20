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
    public class EmployeeRepository : BaseRepository
    {
        public EmployeeRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            string query = "SELECT * FROM Employee";

            using var reader = ExecuteReader(query);
            while (reader.Read())
            {
                employees.Add(new Employee(
                    employeeId: reader.GetInt32(0),
                    firstName: reader.GetString(1),
                    lastName: reader.GetString(2),
                    email: reader.GetString(3),
                    phoneNumber: reader.GetString(4),
                    hireDate: reader.GetDateTime(5),
                    positionId: reader.GetInt32(6),
                    usersID: reader.GetInt32(7)
                ));
            }
            return employees;
        }

        //public void AddEmployee(Employee employee)
        //{
        //    string query = @"INSERT INTO Employee 
        //                  (FirstName, LastName, Email, PhoneNumber, HireDate, DepartmentID, PositionID) 
        //                  VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @HireDate, @DepartmentID, @PositionID)";

        //    var parameters = new[]
        //    {
        //        new SqlParameter("@FirstName", employee.FirstName),
        //        new SqlParameter("@LastName", employee.LastName),
        //        new SqlParameter("@Email", employee.Email),
        //        new SqlParameter("@PhoneNumber", (object)employee.PhoneNumber ?? DBNull.Value),
        //        new SqlParameter("@HireDate", employee.HireDate),
        //        new SqlParameter("@DepartmentID", employee.DepartmentID),
        //        new SqlParameter("@PositionID", employee.PositionID)
        //    };

        //    ExecuteNonQuery(query, parameters);
        //}

        //public void UpdateEmployee(Employee employee)
        //{
        //    string query = @"UPDATE Employee SET
        //                  FirstName = @FirstName,
        //                  LastName = @LastName,
        //                  Email = @Email,
        //                  PhoneNumber = @PhoneNumber,
        //                  HireDate = @HireDate,
        //                  DepartmentID = @DepartmentID,
        //                  PositionID = @PositionID,
        //                  UsersID = @UsersID
        //                  WHERE EmployeeID = @EmployeeID";

        //    var parameters = new[]
        //    {
        //        new SqlParameter("@EmployeeID", employee.EmployeeID),
        //        new SqlParameter("@FirstName", employee.FirstName),
        //        new SqlParameter("@LastName", employee.LastName),
        //        new SqlParameter("@Email", employee.Email),
        //        new SqlParameter("@PhoneNumber", (object)employee.PhoneNumber ?? DBNull.Value),
        //        new SqlParameter("@HireDate", employee.HireDate),
        //        new SqlParameter("@DepartmentID", employee.DepartmentID),
        //        new SqlParameter("@PositionID", employee.PositionID),
        //        new SqlParameter("@UsersID", employee.UsersID)
        //    };

        //    ExecuteNonQuery(query, parameters);
        //}

        //public void DeleteEmployee(int employeeId)
        //{
        //    string query = "DELETE FROM Employee WHERE EmployeeID = @EmployeeID";
        //    var parameters = new[] { new SqlParameter("@EmployeeID", employeeId) };
        //    ExecuteNonQuery(query, parameters);
        //}

        //public Employee? GetEmployeeById(int employeeId)
        //{
        //    string query = "SELECT * FROM Employee WHERE EmployeeID = @EmployeeID";
        //    var parameters = new[] { new SqlParameter("@EmployeeID", employeeId) };

        //    using var reader = ExecuteReader(query, parameters);
        //    if (reader.Read())
        //    {
        //        return new Employee(
        //            employeeId: reader.GetInt32(0),
        //            firstName: reader.GetString(1),
        //            lastName: reader.GetString(2),
        //            email: reader.GetString(3),
        //            phoneNumber: reader.GetString(4),
        //            hireDate: reader.GetDateTime(5),
        //            departmentId: reader.GetInt32(6),
        //            positionId: reader.GetInt32(7),
        //            usersID: reader.GetInt32(8)
        //        );
        //    }
        //    return null;
        //}

        //public List<Employee> GetEmployeesByDepartment(int departmentId)
        //{
        //    var employees = new List<Employee>();
        //    string query = "SELECT * FROM Employee WHERE DepartmentID = @DepartmentID";
        //    var parameters = new[] { new SqlParameter("@DepartmentID", departmentId) };

        //    using var reader = ExecuteReader(query, parameters);
        //    while (reader.Read())
        //    {
        //        employees.Add(new Employee(
        //            employeeId: reader.GetInt32(0),
        //            firstName: reader.GetString(1),
        //            lastName: reader.GetString(2),
        //            email: reader.GetString(3),
        //            phoneNumber: reader.GetString(4),
        //            hireDate: reader.GetDateTime(5),
        //            departmentId: reader.GetInt32(6),
        //            positionId: reader.GetInt32(7),
        //            usersID: reader.GetInt32(8)
        //        ));
        //    }
        //    return employees;
        //}
        public Employee GetEmployeeByUserID(Users users)
        {
            if (users == null) throw new InvalidOperationException($"Employee for UserID not found.");
            string query = "SELECT * FROM Employee WHERE UsersID = @UsersID";
            var parameters = new[] { new SqlParameter("@UsersID", users.UserID) };

            using var reader = ExecuteReader(query, parameters);
            while (reader.Read())
            {
                return new Employee (
                    employeeId: reader.GetInt32(0),
                    firstName: reader.GetString(1),
                    lastName: reader.GetString(2),
                    email: reader.GetString(3),
                    phoneNumber: reader.GetString(4),
                    hireDate: reader.GetDateTime(5),
                    positionId: reader.GetInt32(6),
                    usersID: reader.GetInt32(7)
                );
            }
            throw new InvalidOperationException($"Employee for UserID {users.UserID} not found.");
        }
    }
}
