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
    public class StatusRepository : BaseRepository
    {
        public StatusRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public List<Statuse> GetAllStatuses()
        {
            var statuses = new List<Statuse>();
            string query = "SELECT * FROM Status";

            using var reader = ExecuteReader(query);
            while (reader.Read())
            {
                statuses.Add(new Statuse(
                    statusId: reader.GetInt32(0),
                    statusName: reader.GetString(1)
                ));
            }
            return statuses;
        }

        public void AddStatus(Statuse status)
        {
            string query = "INSERT INTO Statuse (StatusName) VALUES (@StatusName)";

            var parameters = new[]
            {
                new SqlParameter("@StatusName", status.StatusName)
            };

            ExecuteNonQuery(query, parameters);
        }

        public void UpdateStatus(Statuse status)
        {
            string query = @"UPDATE Status SET
                          StatusName = @StatusName
                          WHERE StatusID = @StatusID";

            var parameters = new[]
            {
                new SqlParameter("@StatusID", status.StatusID),
                new SqlParameter("@StatusName", status.StatusName)
            };

            ExecuteNonQuery(query, parameters);
        }

        public void DeleteStatus(int statusId)
        {
            string query = "DELETE FROM Status WHERE StatusID = @StatusID";
            var parameters = new[] { new SqlParameter("@StatusID", statusId) };
            ExecuteNonQuery(query, parameters);
        }

        public List<Statuse> GetStatusById(List<Project> project)
        {
            List<Statuse> statuses = new List<Statuse>();
            string query = "SELECT * FROM [Status] WHERE ID = @ID";
            foreach (Project p in project)
            {
                var parameters = new[] { new SqlParameter("@ID", p.StatusID) };

                using var reader = ExecuteReader(query, parameters);
                if (reader.Read())
                {
                    statuses.Add(new Statuse(
                        statusId: reader.GetInt32(0),
                        statusName: reader.GetString(1)
                    ));
                }
            }
            
            return statuses;
        }
        public List<Statuse> GetStatusById(List<Tasks> tasks)
        {
            List<Statuse> statuses = new List<Statuse>();
            string query = "SELECT * FROM [Status] WHERE ID = @ID";
            foreach (Tasks t in tasks)
            {
                var parameters = new[] { new SqlParameter("@ID", t.StatusID) };

                using var reader = ExecuteReader(query, parameters);
                while (reader.Read())
                {
                    statuses.Add(new Statuse(
                        statusId: reader.GetInt32(0),
                        statusName: reader.GetString(1)
                    ));
                }
            }

            return statuses;
        }

        public Statuse? GetActiveStatus()
        {
            string query = "SELECT * FROM Status WHERE StatusName = 1";

            using var reader = ExecuteReader(query);
            if (reader.Read())
            {
                return new Statuse(
                    statusId: reader.GetInt32(0),
                    statusName: reader.GetString(1)
                );
            }
            return null;
        }
    }
}
