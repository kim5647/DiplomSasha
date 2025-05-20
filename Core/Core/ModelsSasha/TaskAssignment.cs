using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core.ModelsSasha
{
    public class TaskAssignment
    {
        public TaskAssignment() { }
        public TaskAssignment(int? taskAssignmentId, int taskId, int employeeId, DateTime assignedDate)
        {
            TaskAssignmentID = taskAssignmentId;
            TaskID = taskId;
            EmployeeID = employeeId;
            AssignedDate = assignedDate;
        }
        public TaskAssignment(int taskId, int employeeId, DateTime assignedDate)
        {
            TaskID = taskId;
            EmployeeID = employeeId;
            AssignedDate = assignedDate;
        }

        public int? TaskAssignmentID { get; set; }
        public int TaskID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime AssignedDate { get; set; }
    }
}
