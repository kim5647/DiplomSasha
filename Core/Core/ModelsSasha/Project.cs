using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core.ModelsSasha
{
    public class Project
    {
        public Project() { }
        public Project(int? projectId, string projectName, DateTime startDate, DateTime endDate, decimal budget, string clientName, int statusId, string description)
        {
            ProjectID = projectId;
            ProjectName = projectName;
            StartDate = startDate;
            EndDate = endDate;
            Budget = budget;
            ClientName = clientName;
            StatusID = statusId;
            Description = description;
        }

        public Project(string projectName, DateTime startDate, DateTime endDate, decimal budget, string clientName, int statusId, string description)
        {
            ProjectName = projectName;
            StartDate = startDate;
            EndDate = endDate;
            Budget = budget;
            ClientName = clientName;
            StatusID = statusId;
            Description = description;
        }

        public int? ProjectID { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Budget { get; set; }
        public string? ClientName { get; set; }
        public int? StatusID { get; set; }
        public string? Description { get; set; }
    }
}
