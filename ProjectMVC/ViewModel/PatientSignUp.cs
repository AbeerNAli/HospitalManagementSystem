using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectMVC.ViewModel
{
    public class PatientSignUp
    {
        [Required(ErrorMessage = "Please Enter your Name")]
        [Display(Name = "Name: ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter your Email")]
        [Display(Name = "Email: ")]
        public string Email { get; set; }
        [Display(Name = "National ID: ")]
        public string NationalID { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Nullable<int> Age { get; set; }
        [Required(ErrorMessage = "Please Enter your  User Name")]
        [Display(Name = "User Name: ")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please Enter your password")]
        [Display(Name = "PAssword: ")]
        public string password { get; set; }
        public Nullable<int> PatientID { get; set; }

    }
}