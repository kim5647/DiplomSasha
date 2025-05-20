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
    public class Auth_logRepository : BaseRepository
    {
        public Auth_logRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public List<Auth_log> GetAllAuthLogs()
        {
            var logs = new List<Auth_log>();
            string query = "SELECT * FROM auth_log";

            using var reader = ExecuteReader(query);
            while (reader.Read())
            {
                logs.Add(new Auth_log(
                    logId: reader.GetInt32(0),
                    employeeId: reader.GetInt32(1),
                    loginTime: reader.GetDateTime(2),
                    isSuccessful: reader.GetBoolean(3),
                    message: reader.GetString(4)
                ));
            }
            return logs;
        }

        public void AddAuthLog(Auth_log authLog)
        {
            string query = @"INSERT INTO auth_log 
                          (employee_id, login_time, is_successful, message) 
                          VALUES (@EmployeeID, @LoginTime, @IsSuccessful, @Message)";

            var parameters = new[]
            {
                new SqlParameter("@EmployeeID", authLog.EmployeeID),
                new SqlParameter("@LoginTime", authLog.LoginTime),
                new SqlParameter("@IsSuccessful", authLog.IsSuccessful),
                new SqlParameter("@Message", authLog.Message)
            };

            ExecuteNonQuery(query, parameters);
        }

        public List<Auth_log> GetAuthLogsByEmployee(int employeeId)
        {
            var logs = new List<Auth_log>();
            string query = "SELECT * FROM auth_log WHERE employee_id = @EmployeeID";
            var parameters = new[] { new SqlParameter("@EmployeeID", employeeId) };

            using var reader = ExecuteReader(query, parameters);
            while (reader.Read())
            {
                logs.Add(new Auth_log(
                    logId: reader.GetInt32(0),
                    employeeId: reader.GetInt32(1),
                    loginTime: reader.GetDateTime(2),
                    isSuccessful: reader.GetBoolean(3),
                    message: reader.GetString(4)
                ));
            }
            return logs;
        }

        public List<Auth_log> GetFailedAuthAttempts(DateTime startDate, DateTime endDate)
        {
            var logs = new List<Auth_log>();
            string query = @"SELECT * FROM auth_log 
                           WHERE is_successful = 0 
                           AND login_time BETWEEN @StartDate AND @EndDate";

            var parameters = new[]
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };

            using var reader = ExecuteReader(query, parameters);
            while (reader.Read())
            {
                logs.Add(new Auth_log(
                    logId: reader.GetInt32(0),
                    employeeId: reader.GetInt32(1),
                    loginTime: reader.GetDateTime(2),
                    isSuccessful: reader.GetBoolean(3),
                    message: reader.GetString(4)
                ));
            }
            return logs;
        }
    }
}
