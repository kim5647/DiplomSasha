using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core.ModelsSasha
{
    public class Position
    {
        public Position() { }
        public Position(int? positionId, string positionName, decimal baseSalary, int departmentId)
        {
            PositionID = positionId;
            PositionName = positionName;
            BaseSalary = baseSalary;
            DepartmentID = departmentId;
        }

        public Position(string positionName, decimal baseSalary, int departmentId)
        {
            PositionName = positionName;
            BaseSalary = baseSalary;
            DepartmentID = departmentId;
        }

        public int? PositionID { get; set; }
        public string PositionName { get; set; } = string.Empty;
        public decimal BaseSalary { get; set; }
        public int DepartmentID { get; set; }
    }
}
