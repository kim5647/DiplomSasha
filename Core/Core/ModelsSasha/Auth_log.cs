using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core.ModelsSasha
{
    public class Auth_log
    {
        public Auth_log() { }
        public Auth_log(int? logId, int employeeId, DateTime loginTime, bool isSuccessful, string message)
        {
            LogID = logId;
            EmployeeID = employeeId;
            LoginTime = loginTime;
            IsSuccessful = isSuccessful;
            Message = message;
        }

        public Auth_log(int employeeId, DateTime loginTime, bool isSuccessful, string message)
        {
            EmployeeID = employeeId;
            LoginTime = loginTime;
            IsSuccessful = isSuccessful;
            Message = message;
        }

        public int? LogID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime? LoginTime { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}