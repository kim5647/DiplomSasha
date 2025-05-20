using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core.ModelsSasha
{
    public class Department
    {
        public Department() { }
        public Department(int? departmentId, string departmentName)
        {
            DepartmentID = departmentId;
            DepartmentName = departmentName;
        }

        public Department(string departmentName)
        {
            DepartmentName = departmentName;
        }

        public int? DepartmentID { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
    }
}
