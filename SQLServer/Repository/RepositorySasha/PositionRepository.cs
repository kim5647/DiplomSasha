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
    public class PositionRepository : BaseRepository
    {
        public PositionRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public List<Position> GetAllPositions()
        {
            var positions = new List<Position>();
            string query = "SELECT * FROM Position";

            using var reader = ExecuteReader(query);
            while (reader.Read())
            {
                positions.Add(new Position(
                    positionId: reader.GetInt32(0),
                    positionName: reader.GetString(1),
                    baseSalary: reader.GetDecimal(2),
                    departmentId: reader.GetInt32(3)
                ));
            }
            return positions;
        }

        public void AddPosition(Position position)
        {
            string query = @"INSERT INTO Position 
                          (PositionName, BaseSalary, DepartmentID) 
                          VALUES (@PositionName, @BaseSalary, @DepartmentID)";

            var parameters = new[]
            {
                new SqlParameter("@PositionName", position.PositionName),
                new SqlParameter("@BaseSalary", position.BaseSalary),
                new SqlParameter("@DepartmentID", position.DepartmentID)
            };

            ExecuteNonQuery(query, parameters);
        }

        public void UpdatePosition(Position position)
        {
            string query = @"UPDATE Position SET
                          PositionName = @PositionName,
                          BaseSalary = @BaseSalary,
                          DepartmentID = @DepartmentID
                          WHERE PositionID = @PositionID";

            var parameters = new[]
            {
                new SqlParameter("@PositionID", position.PositionID),
                new SqlParameter("@PositionName", position.PositionName),
                new SqlParameter("@BaseSalary", position.BaseSalary),
                new SqlParameter("@DepartmentID", position.DepartmentID)
            };

            ExecuteNonQuery(query, parameters);
        }

        public void DeletePosition(int positionId)
        {
            string query = "DELETE FROM Position WHERE PositionID = @PositionID";
            var parameters = new[] { new SqlParameter("@PositionID", positionId) };
            ExecuteNonQuery(query, parameters);
        }

        public Position? GetPositionById(Employee employee)
        {
            string query = "SELECT * FROM Position WHERE ID = @ID";
            var parameters = new[] { new SqlParameter("@ID", employee.PositionID) };

            using var reader = ExecuteReader(query, parameters);
            if (reader.Read())
            {
                return new Position(
                    positionId: reader.GetInt32(0),
                    positionName: reader.GetString(1),
                    baseSalary: reader.GetDecimal(2),
                    departmentId: reader.GetInt32(3)
                );
            }
            return null;
        }

        public List<Position> GetPositionsByDepartment(int departmentId)
        {
            var positions = new List<Position>();
            string query = "SELECT * FROM Position WHERE DepartmentID = @DepartmentID";
            var parameters = new[] { new SqlParameter("@DepartmentID", departmentId) };

            using var reader = ExecuteReader(query, parameters);
            while (reader.Read())
            {
                positions.Add(new Position(
                    positionId: reader.GetInt32(0),
                    positionName: reader.GetString(1),
                    baseSalary: reader.GetDecimal(2),
                    departmentId: reader.GetInt32(3)
                ));
            }
            return positions;
        }
    }
}
