using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceApplication.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]

        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }

    public class AttendanceViewModel
    {
        public bool iscoming { get; set; }
        public bool isLeave { get; set; }
        
    }

    public class LunchViewModel
    {
        public bool startlunch { get; set; }
    }
}
