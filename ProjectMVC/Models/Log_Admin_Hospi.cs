using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectMVC.Models
{
    public class Log_Admin_Hospi
    {
        [Required(ErrorMessage = "Please Enter Your Email")]
        [Display(Name = "User Name")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please Enter Your Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public int roleId { get; set; }


        public int hospitalId { get; set; }
    }
}