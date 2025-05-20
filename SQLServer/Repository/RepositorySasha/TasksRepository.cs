using Microsoft.Data.SqlClient;
using SQLServer.DatabaseContext;
using SQLServer.Repository.Base;
using Core.Core.ModelsSasha;
using System.Reflection.PortableExecutable;

namespace SQLServer.Repository.RepositorySasha
{
    public class TasksRepository : BaseRepository
    {
        public TasksRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public List<Tasks> GetAllTasks()
        {
            var tasks = new List<Tasks>();
            string query = "SELECT * FROM Task";

            using var reader = ExecuteReader(query);
            while (reader.Read())
            {
                tasks.Add(new Tasks(
                    taskId: reader.GetInt32(0), 
                    taskName: reader.GetString(1),
                    dueDate: reader.GetDateTime(2),
                    priority: reader.GetString(3),
                    statusId: reader.GetInt32(4),
                    projectId: reader.GetInt32(5),
                    description: reader.GetString(6)
                ));
            }
            return tasks;
        }
        public Tasks GetOneTasks(Tasks task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task), "Task object cannot be null");

            string query = @"
                    SELECT *
                    FROM Task 
                    WHERE 
                    TaskName = @TaskName 
                    AND DueDate = @DueDate 
                    AND StatusID = @StatusID 
                    AND ProjectID = @ProjectID";

            var parameters = new[]
            {
                new SqlParameter("@TaskName", task.TaskName),
                new SqlParameter("@DueDate", task.DueDate),
                new SqlParameter("@StatusID", task.StatusID),
                new SqlParameter("@ProjectID", task.ProjectID),
            };

            // Передаем параметры в ExecuteReader
            using var reader = ExecuteReader(query, parameters);

            if (reader.Read())
            {
                return new Tasks
                {
                    TaskID = reader.GetInt32(0),
                    TaskName = reader.GetString(1),
                    DueDate = reader.GetDateTime(2),
                    Priority = reader.IsDBNull(3) ? null : reader.GetString(3),
                    StatusID = reader.GetInt32(4),
                    ProjectID = reader.GetInt32(5),
                    Description = reader.IsDBNull(6) ? null : reader.GetString(6)
                };
            }

            throw new InvalidOperationException($"Task not found.");
        }
        public void AddTask(Tasks task)
        {
            string query = @"INSERT INTO Task 
                          (TaskName, DueDate, Priority, StatusID, ProjectID, Description) 
                          VALUES (@TaskName, @DueDate, @Priority, @StatusID, @ProjectID, @Description)";

            var parameters = new[]
            {
                new SqlParameter("@TaskName", task.TaskName),
                new SqlParameter("@DueDate", task.DueDate),
                new SqlParameter("@Priority", (object)task.Priority ?? DBNull.Value),
                new SqlParameter("@StatusID", task.StatusID),
                new SqlParameter("@ProjectID", task.ProjectID),
                new SqlParameter("@Description", (object)task.Description ?? DBNull.Value)
            };

            ExecuteNonQuery(query, parameters);
        }

        public void UpdateTask(Tasks task)
        {
            string query = @"UPDATE Task SET
                          TaskName = @TaskName,
                          DueDate = @DueDate,
                          Priority = @Priority,
                          StatusID = @StatusID,
                          ProjectID = @ProjectID,
                          Description = @Description
                          WHERE TaskID = @TaskID";

            var parameters = new[]
            {
                new SqlParameter("@TaskID", task.TaskID),
                new SqlParameter("@TaskName", task.TaskName),
                new SqlParameter("@DueDate", task.DueDate),
                new SqlParameter("@Priority", (object)task.Priority ?? DBNull.Value),
                new SqlParameter("@StatusID", task.StatusID),
                new SqlParameter("@ProjectID", task.ProjectID),
                new SqlParameter("@Description", (object)task.Description ?? DBNull.Value)
            };

            ExecuteNonQuery(query, parameters);
        }

        public void DeleteTask(int taskId)
        {
            string query = "DELETE FROM Task WHERE TaskID = @TaskID";
            var parameters = new[] { new SqlParameter("@TaskID", taskId) };
            ExecuteNonQuery(query, parameters);
        }

        public List<Tasks> GetTaskById(List<TaskAssignment> taskAssignment)
        {
            List<Tasks> tasks = new List<Tasks>();
            string query = "SELECT * FROM Task WHERE ID = @ID";
            foreach (var tA in taskAssignment)
            {
                var parameters = new[] { new SqlParameter("@ID", tA.TaskID) };

                using var reader = ExecuteReader(query, parameters);
                while (reader.Read())
                {
                    tasks.Add( new Tasks(
                        taskId: reader.GetInt32(0),
                        taskName: reader.GetString(1),
                        dueDate: reader.GetDateTime(2),
                        priority: reader.GetString(3),
                        statusId: reader.GetInt32(4),
                        projectId: reader.GetInt32(5),
                        description: reader.GetString(6)
                    ));
                }
            }
            return tasks;
        }
        public Tasks? GetTaskById(Statuse statuse)
        {
            string query = "SELECT * FROM Task WHERE StatusID = @StatusID";
            var parameters = new[] { new SqlParameter("@StatusID", statuse.StatusID) };

            using var reader = ExecuteReader(query, parameters);
            if (reader.Read())
            {
                return new Tasks(
                    taskId: reader.GetInt32(0),
                    taskName: reader.GetString(1),
                    dueDate: reader.GetDateTime(2),
                    priority: reader.GetString(3),
                    statusId: reader.GetInt32(4),
                    projectId: reader.GetInt32(5),
                    description: reader.GetString(6)
                );
            }
            return null;
        }
        public Tasks? GetTaskById(Project project)
        {
            string query = "SELECT * FROM Task WHERE ProjectID = @ProjectID";
            var parameters = new[] { new SqlParameter("@ProjectID", project.ProjectID) };

            using var reader = ExecuteReader(query, parameters);
            if (reader.Read())
            {
                return new Tasks(
                    taskId: reader.GetInt32(0),
                    taskName: reader.GetString(1),
                    dueDate: reader.GetDateTime(2),
                    priority: reader.GetString(3),
                    statusId: reader.GetInt32(4),
                    projectId: reader.GetInt32(5),
                    description: reader.GetString(6)
                );
            }
            return null;
        }

        public List<Tasks> GetTasksByProject(int projectId)
        {
            var tasks = new List<Tasks>();
            string query = "SELECT * FROM Task WHERE ProjectID = @ProjectID";
            var parameters = new[] { new SqlParameter("@ProjectID", projectId) };

            using var reader = ExecuteReader(query, parameters);
            while (reader.Read())
            {
                tasks.Add(new Tasks(
                    taskId: reader.GetInt32(0),
                    taskName: reader.GetString(1),
                    dueDate: reader.GetDateTime(2),
                    priority: reader.GetString(3),
                    statusId: reader.GetInt32(4),
                    projectId: reader.GetInt32(5),
                    description: reader.GetString(6)
                ));
            }
            return tasks;
        }

        public List<Tasks> GetTasksByStatus(int statusId)
        {
            var tasks = new List<Tasks>();
            string query = "SELECT * FROM Task WHERE StatusID = @StatusID";
            var parameters = new[] { new SqlParameter("@StatusID", statusId) };

            using var reader = ExecuteReader(query, parameters);
            while (reader.Read())
            {
                tasks.Add(new Tasks(
                    taskId: reader.GetInt32(0),
                    taskName: reader.GetString(1),
                    dueDate: reader.GetDateTime(2),
                    priority: reader.GetString(3),
                    statusId: reader.GetInt32(4),
                    projectId: reader.GetInt32(5),
                    description: reader.GetString(6)
                ));
            }
            return tasks;
        }
    }

}
