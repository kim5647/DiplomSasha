using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core.ModelsSasha
{
    public class Role
    {
        public Role() { }
        public Role(int? roleId, string roleName)
        {
            RoleID = roleId;
            RoleName = roleName;
        }

        public Role(string roleName)
        {
            RoleName = roleName;
        }

        public int? RoleID { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}
