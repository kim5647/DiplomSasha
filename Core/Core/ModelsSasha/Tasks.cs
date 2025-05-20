using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core.ModelsSasha
{
    public class Tasks
    {
        public Tasks() { }
        public Tasks(int? taskId, string taskName, DateTime dueDate, string priority, int statusId, int projectId,string description)
        {
            TaskID = taskId;
            TaskName = taskName;
            DueDate = dueDate;
            Priority = priority;
            StatusID = statusId;
            ProjectID = projectId;
            Description = description;
        }

        public Tasks(string taskName, DateTime dueDate, string priority, int statusId, int projectId, string description)
        {
            TaskName = taskName;
            DueDate = dueDate;
            Priority = priority;
            StatusID = statusId;
            ProjectID = projectId;
            Description = description;
        }

        public int? TaskID { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
        public int? StatusID { get; set; }
        public int ProjectID { get; set; }
        public string? Description { get; set; }
    }
}
