using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace AttendanceApplication.Models
{
    public class DataBContext : DbContext
    {

        public DataBContext() : base("DefaultConnection")
        {

        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Attendance> Attendance { get; set; }


    }
}
