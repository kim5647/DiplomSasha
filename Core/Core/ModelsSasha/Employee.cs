using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core.ModelsSasha
{
    public class Employee
    {
        public string FullName => $"{FirstName} {LastName}";
        public Employee() { }
        public Employee(int? employeeId, string firstName, string lastName, string email, string phoneNumber, DateTime hireDate, int positionId, int usersID)
        {
            EmployeeID = employeeId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            HireDate = hireDate;
            PositionID = positionId;
        }

        public Employee(string firstName, string lastName, string email, string phoneNumber, DateTime hireDate, int positionId, int usersID)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            HireDate = hireDate;
            PositionID = positionId;
            UsersID = usersID;
        }

        public int? EmployeeID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public int PositionID { get; set; }
        public int UsersID { get; set; }
    }
}
