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
    public class ProjectAssignmentRepository : BaseRepository
    {
        public ProjectAssignmentRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public List<ProjectAssignment> GetAllProjectAssignments()
        {
            var assignments = new List<ProjectAssignment>();
            string query = "SELECT * FROM Project_assignment";

            using var reader = ExecuteReader(query);
            while (reader.Read())
            {
                assignments.Add(new ProjectAssignment(
                    assignmentId: reader.GetInt32(0),
                    projectId: reader.GetInt32(1),
                    employeeId: reader.GetInt32(2),
                    roleId: reader.GetInt32(3),
                    assignedDate: reader.GetDateTime(4)
                ));
            }
            return assignments;
        }

        public void UpdateProjectAssignment(ProjectAssignment assignment)
        {
            string query = @"UPDATE Project_assignment SET
                          ProjectID = @ProjectID,
                          EmployeeID = @EmployeeID,
                          RoleID = @RoleID,
                          AssignedDate = @AssignedDate
                          WHERE AssignmentID = @AssignmentID";

            var parameters = new[]
            {
                new SqlParameter("@AssignmentID", assignment.AssignmentID),
                new SqlParameter("@ProjectID", assignment.ProjectID),
                new SqlParameter("@EmployeeID", assignment.EmployeeID),
                new SqlParameter("@RoleID", assignment.RoleID),
                new SqlParameter("@AssignedDate", assignment.AssignedDate)
            };

            ExecuteNonQuery(query, parameters);
        }

        public void DeleteProjectAssignment(int assignmentId)
        {
            string query = "DELETE FROM Project_assignment WHERE AssignmentID = @AssignmentID";
            var parameters = new[] { new SqlParameter("@AssignmentID", assignmentId) };
            ExecuteNonQuery(query, parameters);
        }

        public ProjectAssignment? GetProjectAssignmentById(int assignmentId)
        {
            string query = "SELECT * FROM Project_assignment WHERE AssignmentID = @AssignmentID";
            var parameters = new[] { new SqlParameter("@AssignmentID", assignmentId) };

            using var reader = ExecuteReader(query, parameters);
            if (reader.Read())
            {
                return new ProjectAssignment(
                    assignmentId: reader.GetInt32(0),
                    projectId: reader.GetInt32(1),
                    employeeId: reader.GetInt32(2),
                    roleId: reader.GetInt32(3),
                    assignedDate: reader.GetDateTime(4)
                );
            }
            return null;
        }
        public void AddProjectAssignment(List<ProjectAssignment> projectAssignments)
        {
            if (projectAssignments == null)
                throw new ArgumentNullException(nameof(projectAssignments));

            const string query = @"
                INSERT INTO ProjectAssignment 
                (ProjectID, EmployeeID, RoleID, AssignedDate) 
                VALUES (@ProjectID, @EmployeeID, @RoleID, @AssignedDate)";

            foreach (var assignment in projectAssignments)
            {
                var parameters = new[]
                {
                    new SqlParameter("@ProjectID", assignment.ProjectID),
                    new SqlParameter("@EmployeeID", assignment.EmployeeID),
                    new SqlParameter("@RoleID", assignment.RoleID),
                    new SqlParameter("@AssignedDate", assignment.AssignedDate)
                };

                ExecuteNonQuery(query, parameters);
            }
        }



        public List<ProjectAssignment> GetProjectAssignmentByEmployeeId(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            var result = new List<ProjectAssignment>();
            const string query = "SELECT * FROM ProjectAssignment WHERE EmployeeID = @EmployeeID";
            var parameters = new[] { new SqlParameter("@EmployeeID", employee.EmployeeID) };

            using var reader = ExecuteReader(query, parameters);
            while (reader.Read())
            {
                result.Add(new ProjectAssignment(
                    assignmentId: reader.GetInt32(0),
                    projectId: reader.GetInt32(1),
                    employeeId: reader.GetInt32(2),
                    roleId: reader.GetInt32(3),
                    assignedDate: reader.GetDateTime(4)
                ));
            }
            return result;
        }

        public List<ProjectAssignment> GetAssignmentsByProject(int projectId)
        {
            var assignments = new List<ProjectAssignment>();
            string query = "SELECT * FROM Project_assignment WHERE ProjectID = @ProjectID";
            var parameters = new[] { new SqlParameter("@ProjectID", projectId) };

            using var reader = ExecuteReader(query, parameters);
            while (reader.Read())
            {
                assignments.Add(new ProjectAssignment(
                    assignmentId: reader.GetInt32(0),
                    projectId: reader.GetInt32(1),
                    employeeId: reader.GetInt32(2),
                    roleId: reader.GetInt32(3),
                    assignedDate: reader.GetDateTime(4)
                ));
            }
            return assignments;
        }
    }
}
