using ProjectMVC.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectMVC.Models;

namespace ProjectMVC.Controllers
{
    public class HospitalController : Controller
    {
        HMSEntities db = new HMSEntities();
      



        //*********************************************************************
        // GET: Hospital
        [HttpGet]
        public ActionResult MyProfile()
        {
            if (Session["HID"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            int HID = (int)Session["HID"];
            Hospital hospital = db.Hospitals.Where(x => x.ID == HID).FirstOrDefault();
            return View(hospital);
        }
        //***************************************************************************

        [HttpGet]
        public ActionResult EditProfile()
        {
            if (Session["HID"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            int HID = (int)Session["HID"];

            Hospital h = db.Hospitals.Where(x => x.ID == HID).FirstOrDefault();
            return View(h);
        }
        [HttpPost]
        public ActionResult EditProfile(Hospital hospital)
        {
            if (Session["HID"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            int HID = (int)Session["HID"];
            if (!ModelState.IsValid)
            {
                return View("EditProfile");

            }
           
            try
            {
                var EditHospital = db.Hospitals.Single(x => x.ID == HID);
                EditHospital.Name = hospital.Name;
                EditHospital.NumOfAllBed = hospital.NumOfAllBed;
                // EditHospital.NumOfAvailableBed = hospital.NumOfAvailableBed;
                EditHospital.Phone = hospital.Phone;
                EditHospital.Address = hospital.Address;
                EditHospital.HospitalLongitude = hospital.HospitalLongitude;
                EditHospital.HospitalLatitude = hospital.HospitalLatitude;
                //EditHospital.Logo = hospital.Logo;
                db.SaveChanges();
                return RedirectToAction("MyProfile");
            }
            catch (Exception ex)
            {
                return RedirectToAction("MyProfile");
            }
        }

        //*************************************************************************************
        [HttpGet]
        public ActionResult ShowSpeciality()
        {
            if (Session["HID"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            int HID = (int)Session["HID"];

            SpecialityADO speciality = new SpecialityADO();
            // List<Specialty> specialties = db.Specialties.Where(x => x.ID == db.Doctors.Count() && x.SpecialtyID == db.Specialties.ID).Include(x => x.Hospital).ToList();
            List<Specialty> specialties = speciality.FetchAll_Speciality(HID);

            return View(specialties);
        }


        ////****************************************************************************

        [HttpGet]
        public ActionResult ShowReservation(int id)
        {
            if (Session["HID"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            int HID = (int)Session["HID"];

            //List<Reservation> re = db.Reservations.Where(x => x.HospitalID == id).Include(p => p.Specialty).Include(x => x.Doctor).Include(y => y.Patient).ToList();

            List<Reservation> re = db.Reservations.Where(x => x.HospitalID == id && x.SpecialtyID == id).Include(p => p.Specialty).Include(y => y.Patient).ToList();
            return View(re);
        }

        [HttpPost]
        public ActionResult ConfirmReservation(int id, bool confirm)
        {
            db.Configuration.ProxyCreationEnabled = false;
            try
            {
                var EditHospital = db.Reservations.Single(x => x.ID == id);
                EditHospital.AdmitOrDecline = confirm;
                db.SaveChanges();
                List<Reservation> response = db.Reservations.Where(x => x.HospitalID == id && x.SpecialtyID == id ).Include(p => p.Specialty).Include(y => y.Patient).ToList();
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = 0 });
            }
            //return Json(new { result = 0 });
        }
        //***********************************************

        [HttpPost]
        public ActionResult Search(string searchTerm)
        {

            List<Hospital> hospitals;
            if (string.IsNullOrEmpty(searchTerm))
            {
                hospitals = db.Hospitals.ToList();
            }
            else
            {
                hospitals = db.Hospitals.Where(s => s.Name.StartsWith(searchTerm)).ToList();
            }
            return View(hospitals);
        }

        //*****************************************************************************
        public ActionResult History(int id)
        {
            if (Session["HID"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            List<PatientDoctor> patientDoctors = db.PatientDoctors.Where(p => p.PatientID == id).Include(x => x.Patient).Include(x => x.Doctor).Include(x => x.Illness).ToList();
            return View(patientDoctors);
        }
    }
}