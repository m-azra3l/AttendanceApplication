using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceApplication.Models
{
    public class Attendance
    {
        public int ID { get; set; }

        [DisplayName("CLock In")]
        public DateTime ComingTime { get; set; }


        [DisplayName("Date")]
        public DateTime DateOfDay { get; set; }

        [DisplayName("Clock Out")]
        public DateTime? LeaveTime { get; set; }

        [DisplayName("Lunch Time")]
        public DateTime? LunchStart { get; set; }

        public int EmployeeID { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
