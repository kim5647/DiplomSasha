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
    public class TaskAssignmentRepository : BaseRepository
    {
        public TaskAssignmentRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public List<TaskAssignment> GetAllTaskAssignments()
        {
            var assignments = new List<TaskAssignment>();
            string query = "SELECT * FROM Task_assignment";

            using var reader = ExecuteReader(query);
            while (reader.Read())
            {
                assignments.Add(new TaskAssignment(
                    taskAssignmentId: reader.GetInt32(0),
                    taskId: reader.GetInt32(1),
                    employeeId: reader.GetInt32(2),
                    assignedDate: reader.GetDateTime(3)
                ));
            }
            return assignments;
        }

        public void AddTaskAssignment(TaskAssignment assignment)
        {
            string query = @"INSERT INTO Task_assignment 
                          (TaskID, EmployeeID, AssignedDate) 
                          VALUES (@TaskID, @EmployeeID, @AssignedDate)";

            var parameters = new[]
            {
                new SqlParameter("@TaskID", assignment.TaskID),
                new SqlParameter("@EmployeeID", assignment.EmployeeID),
                new SqlParameter("@AssignedDate", assignment.AssignedDate)
            };

            ExecuteNonQuery(query, parameters);
        }
        public void AddTaskAssignment(List<TaskAssignment> assignment)
        {
            string query = @"INSERT INTO TaskAssignment 
                          (TaskID, EmployeeID, AssignedDate) 
                          VALUES (@TaskID, @EmployeeID, @AssignedDate)";

            foreach (var assignm in assignment)
            {
                var parameters = new[]
                {
                    new SqlParameter("@TaskID", assignm.TaskID),
                    new SqlParameter("@EmployeeID", assignm.EmployeeID),
                    new SqlParameter("@AssignedDate", assignm.AssignedDate)
                };

                ExecuteNonQuery(query, parameters);
            }
        }

        public void UpdateTaskAssignment(TaskAssignment assignment)
        {
            string query = @"UPDATE Task_assignment SET
                          TaskID = @TaskID,
                          EmployeeID = @EmployeeID,
                          AssignedDate = @AssignedDate
                          WHERE TaskAssignmentID = @TaskAssignmentID";

            var parameters = new[]
            {
                new SqlParameter("@TaskAssignmentID", assignment.TaskAssignmentID),
                new SqlParameter("@TaskID", assignment.TaskID),
                new SqlParameter("@EmployeeID", assignment.EmployeeID),
                new SqlParameter("@AssignedDate", assignment.AssignedDate)
            };

            ExecuteNonQuery(query, parameters);
        }

        public void DeleteTaskAssignment(int taskAssignmentId)
        {
            string query = "DELETE FROM Task_assignment WHERE TaskAssignmentID = @TaskAssignmentID";
            var parameters = new[] { new SqlParameter("@TaskAssignmentID", taskAssignmentId) };
            ExecuteNonQuery(query, parameters);
        }

        public TaskAssignment? GetTaskAssignmentById(TaskAssignment taskAssignment)
        {
            string query = "SELECT * FROM TaskAssignment WHERE ID = @ID";
            var parameters = new[] { new SqlParameter("@ID", taskAssignment.TaskAssignmentID) };

            using var reader = ExecuteReader(query, parameters);
            if (reader.Read())
            {
                return new TaskAssignment(
                    taskAssignmentId: reader.GetInt32(0),
                    taskId: reader.GetInt32(1),
                    employeeId: reader.GetInt32(2),
                    assignedDate: reader.GetDateTime(3)
                );
            }
            return null;
        }
        public TaskAssignment? GetTaskAssignmentById(Tasks tasks)
        {
            string query = "SELECT * FROM TaskAssignment WHERE TaskID = @TaskID";
            var parameters = new[] { new SqlParameter("@TaskID", tasks.TaskID) };

            using var reader = ExecuteReader(query, parameters);
            if (reader.Read())
            {
                return new TaskAssignment(
                    taskAssignmentId: reader.GetInt32(0),
                    taskId: reader.GetInt32(1),
                    employeeId: reader.GetInt32(2),
                    assignedDate: reader.GetDateTime(3)
                );
            }
            return null;
        }
        public List<TaskAssignment> GetTaskAssignmentById(Employee employee)
        {
            List<TaskAssignment> tasks = new List<TaskAssignment>();
            string query = "SELECT * FROM TaskAssignment WHERE EmployeeID = @EmployeeID";
            var parameters = new[] { new SqlParameter("@EmployeeID", employee.EmployeeID) };

            using var reader = ExecuteReader(query, parameters);
            while (reader.Read())
            {
                tasks.Add( new TaskAssignment(
                    taskAssignmentId: reader.GetInt32(0),
                    taskId: reader.GetInt32(1),
                    employeeId: reader.GetInt32(2),
                    assignedDate: reader.GetDateTime(3)
                ));
            }
            return tasks;
        }

        public List<TaskAssignment> GetAssignmentsByTask(int taskId)
        {
            var assignments = new List<TaskAssignment>();
            string query = "SELECT * FROM Task_assignment WHERE TaskID = @TaskID";
            var parameters = new[] { new SqlParameter("@TaskID", taskId) };

            using var reader = ExecuteReader(query, parameters);
            while (reader.Read())
            {
                assignments.Add(new TaskAssignment(
                    taskAssignmentId: reader.GetInt32(0),
                    taskId: reader.GetInt32(1),
                    employeeId: reader.GetInt32(2),
                    assignedDate: reader.GetDateTime(3)
                ));
            }
            return assignments;
        }

        public List<TaskAssignment> GetAssignmentsByEmployee(int employeeId)
        {
            var assignments = new List<TaskAssignment>();
            string query = "SELECT * FROM Task_assignment WHERE EmployeeID = @EmployeeID";
            var parameters = new[] { new SqlParameter("@EmployeeID", employeeId) };

            using var reader = ExecuteReader(query, parameters);
            while (reader.Read())
            {
                assignments.Add(new TaskAssignment(
                    taskAssignmentId: reader.GetInt32(0),
                    taskId: reader.GetInt32(1),
                    employeeId: reader.GetInt32(2),
                    assignedDate: reader.GetDateTime(3)
                ));
            }
            return assignments;
        }
    }
}
