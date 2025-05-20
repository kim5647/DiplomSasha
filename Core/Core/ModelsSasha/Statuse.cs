using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core.ModelsSasha
{
    public class Statuse
    {
        public Statuse() { }
        public Statuse(int? statusId, string statusName)
        {
            StatusID = statusId;
            StatusName = statusName;
        }

        public Statuse(string statusName)
        {
            StatusName = statusName;
        }

        public int? StatusID { get; set; }
        public string StatusName { get; set; }
    }
}
