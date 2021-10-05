
using ProjectMVC.Context;
using ProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMVC.Controllers
{
    public class LoginAdminHospitalController : Controller
    {
        HMSEntities db = new HMSEntities();
        // GET: LoginAdminHospital
        public ActionResult Index()
        {
            return View();
        }

       //*******************************************************************************
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.LogWrong = "none";
            return View();
        }

        [HttpPost]
        public ActionResult Login(Log_Admin_Hospi acc)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.LogWrong = "none";
                return View("Login");
            }
            UserAccount user = db.UserAccounts.FirstOrDefault(x => x.Username == acc.Username && x.password == acc.Password);
           
            if (user == null)
            {
                ViewBag.LogWrong = "block";
                return View("Login");
            }

            else
            {
                
                
                if(user.RoleID == 1)
                {
                    Session["UID"] = user.PatientID;
                    Session["Email"] = user.Username;
                    return RedirectToAction("Index", "User");
                }
                else if (user.RoleID == 2)
                {
                    Session["HID"] = user.HospitalID;
                    Session["NameHospital"] = db.Hospitals.FirstOrDefault(x => x.ID == user.HospitalID).ToString();
                    return RedirectToAction("MyProfile", "Hospital");
                }
                else
                {


                    Session["ID_AH"] = user.ID;
                    Session["Username"] = user.Username;
                    return RedirectToAction("Index", "Admin");
                }
            }
        }
//**********************************************************************
        public ActionResult Logout()
        {
            Session["ID"] = null;
            Session["Email"] = null;
            Session["Name"] = null;
            return RedirectToAction("Login");
        }

        //*****************************************************************
   
    }
}
 