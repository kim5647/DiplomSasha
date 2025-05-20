using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core.ModelsSasha
{
    public class ProjectAssignment
    {
        public ProjectAssignment() { }
        public ProjectAssignment(int? assignmentId, int projectId, int employeeId, int roleId, DateTime assignedDate)
        {
            AssignmentID = assignmentId;
            ProjectID = projectId;
            EmployeeID = employeeId;
            RoleID = roleId;
            AssignedDate = assignedDate;
        }

        public ProjectAssignment(int projectId, int employeeId, int roleId, DateTime assignedDate)
        {
            ProjectID = projectId;
            EmployeeID = employeeId;
            RoleID = roleId;
            AssignedDate = assignedDate;
        }

        public int? AssignmentID { get; set; }
        public int ProjectID { get; set; }
        public int EmployeeID { get; set; }
        public int RoleID { get; set; }
        public DateTime AssignedDate { get; set; }
    }
}
