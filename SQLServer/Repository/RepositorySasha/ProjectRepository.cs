using Core.Core.ModelsSasha;
using Microsoft.Data.SqlClient;
using SQLServer.DatabaseContext;
using SQLServer.Repository.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServer.Repository.RepositorySasha
{
    public class ProjectRepository : BaseRepository
    {
        public ProjectRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public List<Project> GetAllProjects()
        {
            var projects = new List<Project>();
            string query = "SELECT * FROM Project";

            using var reader = ExecuteReader(query);
            while (reader.Read())
            {
                projects.Add(new Project(
                    projectId: reader.IsDBNull(0) ? null : reader.GetInt32(0),
                    projectName: reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    startDate: reader.IsDBNull(2) ? DateTime.MinValue : reader.GetDateTime(2),
                    endDate: reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3), // Используем MinValue вместо null
                    budget: reader.IsDBNull(4) ? 0m : reader.GetDecimal(4),
                    clientName: reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                    statusId: reader.IsDBNull(6) ? 0 : reader.GetInt32(6), // Используем 0 вместо null
                    description: reader.IsDBNull(7) ? string.Empty : reader.GetString(7)
                ));
            }
            return projects;
        }

        public void AddProject(Project project)
        {
            string query = @"INSERT INTO Project 
                          (ProjectName, StartDate, EndDate, Budget, ClientName, StatusID, Description) 
                          VALUES (@ProjectName, @StartDate, @EndDate, @Budget, @ClientName, @StatusID, @Description)";

            var parameters = new[]
            {
                new SqlParameter("@ProjectName", project.ProjectName),
                new SqlParameter("@StartDate", project.StartDate),
                new SqlParameter("@EndDate", project.EndDate),
                new SqlParameter("@Budget", project.Budget),
                new SqlParameter("@ClientName", (object)project.ClientName ?? DBNull.Value),
                new SqlParameter("@StatusID", project.StatusID),
                new SqlParameter("@Description", (object)project.Description ?? DBNull.Value)
            };

            ExecuteNonQuery(query, parameters);
        }

        public void UpdateProject(Project project)
        {
            string query = @"UPDATE Project SET
                          ProjectName = @ProjectName,
                          StartDate = @StartDate,
                          EndDate = @EndDate,
                          Budget = @Budget,
                          ClientName = @ClientName,
                          StatusID = @StatusID,
                          Description = @Description
                          WHERE ProjectID = @ProjectID";

            var parameters = new[]
            {
                new SqlParameter("@ProjectID", project.ProjectID),
                new SqlParameter("@ProjectName", project.ProjectName),
                new SqlParameter("@StartDate", project.StartDate),
                new SqlParameter("@EndDate", project.EndDate),
                new SqlParameter("@Budget", project.Budget),
                new SqlParameter("@ClientName", (object)project.ClientName ?? DBNull.Value),
                new SqlParameter("@StatusID", project.StatusID),
                new SqlParameter("@Description", (object)project.Description ?? DBNull.Value)
            };

            ExecuteNonQuery(query, parameters);
        }

        public void DeleteProject(int projectId)
        {
            string query = "DELETE FROM Project WHERE ProjectID = @ProjectID";
            var parameters = new[] { new SqlParameter("@ProjectID", projectId) };
            ExecuteNonQuery(query, parameters);
        }

        public List<Project> GetProjectById(List<ProjectAssignment> projectAssignment)
        {
            List<Project> projects = new List<Project>();
            
            foreach (var assignment in projectAssignment)
            {
                string query = "SELECT * FROM Project WHERE ID = @ID";
                var parameters = new[] { new SqlParameter("@ID", assignment.ProjectID) };

                using var reader = ExecuteReader(query, parameters);
                while (reader.Read())
                {
                    projects.Add(new Project
                    {
                        ProjectID = reader.GetInt32(0),
                        ProjectName = reader.GetString(1),
                        StartDate = reader.GetDateTime(2),
                        EndDate = reader.GetDateTime(3),
                        Budget = reader.GetDecimal(4),
                        ClientName = reader.GetString(5),
                        StatusID = reader.GetInt32(6),
                        Description = reader.IsDBNull(7) ? null : reader.GetString(7)
                    });
                }
            }
            return projects;
        }
        public Project GetProjectByOne(Project project)
        {
            string query = @"SELECT * FROM Project
                  WHERE 
                  ProjectName = @ProjectName AND
                  EndDate = @EndDate AND
                  Budget = @Budget AND
                  ClientName = @ClientName AND
                  StatusID = @StatusID AND
                  Description = @Description";

            var parameters = new[]
            {
                new SqlParameter("@ProjectName", project.ProjectName),
                new SqlParameter("@EndDate", (object)project.EndDate ?? DBNull.Value),
                new SqlParameter("@Budget", project.Budget),
                new SqlParameter("@ClientName", (object)project.ClientName ?? DBNull.Value),
                new SqlParameter("@StatusID", project.StatusID),
                new SqlParameter("@Description", (object)project.Description ?? DBNull.Value)
            };

            using var reader = ExecuteReader(query, parameters);
            if (reader.Read())
            {
                return new Project(
                    projectId: reader.GetInt32(0),
                    projectName: reader.GetString(1),
                    startDate: reader.IsDBNull(2) ? DateTime.Now : reader.GetDateTime(2),
                    endDate: reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3), // <<<<< исправили
                    budget: reader.GetDecimal(4),
                    clientName: reader.IsDBNull(5) ? null : reader.GetString(5),
                    statusId: reader.GetInt32(6),
                    description: reader.IsDBNull(7) ? null : reader.GetString(7)
                );
            }
            throw new InvalidOperationException($"Project not found.");
        }



        public List<Project> GetProjectsByStatus(int statusId)
        {
            var projects = new List<Project>();
            string query = "SELECT * FROM Project WHERE StatusID = @StatusID";
            var parameters = new[] { new SqlParameter("@StatusID", statusId) };

            using var reader = ExecuteReader(query, parameters);
            while (reader.Read())
            {
                projects.Add(new Project(
                    projectId: reader.GetInt32(0),
                    projectName: reader.GetString(1),
                    startDate: reader.GetDateTime(2),
                    endDate: reader.GetDateTime(3),
                    budget: reader.GetDecimal(4),
                    clientName: reader.GetString(5),
                    statusId: reader.GetInt32(6),
                    description: reader.GetString(7)
                ));
            }
            return projects;
        }
    }
}
