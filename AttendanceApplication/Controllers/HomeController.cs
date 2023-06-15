using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AttendanceApplication.Models;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data.Entity;

namespace AttendanceApplication.Controllers
{
    public class HomeController : Controller
    {
        private DataBContext db = new DataBContext();

        [Authorize]
        public ActionResult Index(AttendanceViewModel vm)
        {
            
            Employee userinfo = JsonConvert.DeserializeObject<Employee>(User.Identity.Name);
            DateTime todayDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
            bool AttendanceValid = db.Attendance.Any(c => c.DateOfDay == todayDate && c.EmployeeID == userinfo.ID);
            if (AttendanceValid == true)
            {
                vm.iscoming = true;
                bool AttendanceAllValid = db.Attendance.Any(c => c.DateOfDay == todayDate && c.EmployeeID == userinfo.ID && c.LeaveTime != null);
                if (AttendanceAllValid)
                {
                    vm.isLeave = true;
                }
            }
            else
            {
                vm.iscoming = false;
            }
            return View(vm);
        }


        [HttpPost]
        [Authorize]
        public ActionResult Index(Attendance attendance, AttendanceViewModel vm)
        {
            Employee userinfo = JsonConvert.DeserializeObject<Employee>(User.Identity.Name);

            DateTime myDateTime = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));

            DateTime todayDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));

            bool AttendanceValid = db.Attendance.Any(c => c.DateOfDay == todayDate && c.EmployeeID == userinfo.ID);
            if (AttendanceValid)
            {
                var attendanceRow = db.Attendance.Where(c => c.DateOfDay == todayDate && c.EmployeeID == userinfo.ID).Single();
                vm.iscoming = true;
                vm.isLeave = true;
                attendanceRow.LeaveTime = myDateTime;

                db.Entry(attendanceRow).State = EntityState.Modified;
                db.SaveChanges();

                return View(vm);

            }
            else
            {
                int userID = userinfo.ID;
                DateTime now = DateTime.Now;
                vm.isLeave = false;
                Attendance Attendance = new Attendance
                {
                    EmployeeID = userID,
                    ComingTime = myDateTime,
                    DateOfDay = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy")),

                };

                db.Attendance.Add(Attendance);
                db.SaveChanges();
                vm.iscoming = true;
                return View(vm);

            }

        }

        [Authorize]
        public ActionResult Lunch(LunchViewModel sm)
        {

            Employee userinfo = JsonConvert.DeserializeObject<Employee>(User.Identity.Name);
            DateTime todayDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
            bool LunchValid = db.Attendance.Any(c => c.DateOfDay == todayDate && c.EmployeeID == userinfo.ID);
            if (LunchValid == true)
            {
                sm.startlunch = false;
                bool LunchAllValid = db.Attendance.Any(c => c.DateOfDay == todayDate && c.EmployeeID == userinfo.ID && c.LunchStart != null );
                if (LunchAllValid)
                {
                    sm.startlunch = true;
                }
            }
            else
            {
                sm.startlunch = true;
            }
            return View(sm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Lunch(Attendance lunch, LunchViewModel sm)
        { 
             Employee userinfo = JsonConvert.DeserializeObject<Employee>(User.Identity.Name);

             DateTime myDateTime = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));

             DateTime todayDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));

            bool LunchValid = db.Attendance.Any(c => c.DateOfDay == todayDate && c.EmployeeID == userinfo.ID);
            if (LunchValid)
            {
                var attendanceRow = db.Attendance.Where(c => c.DateOfDay == todayDate && c.EmployeeID == userinfo.ID).Single();
                sm.startlunch = true;
                attendanceRow.LunchStart = myDateTime;

                db.Entry(attendanceRow).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index","Home");
                
            }
           
            else
            {
                int userID = userinfo.ID;
                DateTime now = DateTime.Now;
                sm.startlunch = false;
                Attendance Lunch = new Attendance
                {
                    EmployeeID = userID,
                    LunchStart = myDateTime,
                    DateOfDay = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy")),

                };
                db.Attendance.Add(Lunch);
                db.SaveChanges();
                sm.startlunch = true;
                return RedirectToAction("Index", "Home");

            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Attendance Tracking Example ";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Developer";

            return View();
        }
    }
}