using ProjectMVC.Context;
using ProjectMVC.Models;
using ProjectMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMVC.Controllers
{
    public class UserController : Controller
    {
        HMSEntities db = new HMSEntities();
        SpecialityADO speciality = new SpecialityADO();
        //****************************************************************************************
        public ActionResult Homepage()
        {
            if (Session["UID"] != null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            List<Hospital> allDataHospital = db.Hospitals.ToList();
            return View(allDataHospital);
        }
        public JsonResult GetHospitals(string term)
        {
            List<string> hospitals = db.Hospitals.Where(s => s.Name.StartsWith(term)).Select(x => x.Name).ToList();
            return Json(hospitals, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Homepage(string searchTerm)
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
        //####################################################################
        public ActionResult Details(int id)
        {
            if (Session["UID"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            Hospital Hospital = db.Hospitals.FirstOrDefault(x => x.ID == id);
            return View(Hospital);
        }
        //****************************************************************************
        public ActionResult Index()
        {
            if (Session["UID"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            List<Hospital> allDataHospital = db.Hospitals.ToList();
            return View(allDataHospital);
        }
        ////*******************************************************************************
        //        //[HttpGet]
        //        //public ActionResult Login()
        //        //{
        //        //    ViewBag.LogWrong = "none";
        //        //    return View();
        //        //}

        //        //[HttpPost]
        //        //public ActionResult Login(Log patient)
        //        //{
        //        //    if (!ModelState.IsValid)
        //        //    {
        //        //        ViewBag.LogWrong = "none";
        //        //        return View("Login");
        //        //    }
        //        //    Patient user = db.Patients.FirstOrDefault(x => x.Email == patient.Email && x.Password == patient.Password);
        //        //    if (user == null)
        //        //    {
        //        //        ViewBag.LogWrong = "block";
        //        //        ViewBag.LogWrong = "Your mail Or Password is Wrong please try again";
        //        //        return View("Login");
        //        //    }

        //        //    else
        //        //    {
        //        //        Session["ID"] = user.ID;
        //        //        Session["Email"] = user.Email;
        //        //        Session["Name"] = user.Name;
        //        //        return RedirectToAction("Index");
        //        //    }
        //        //}
        ////**********************************************************************
        //        public ActionResult Logout()
        //        {
        //            Session["UID"] = null;
        //            Session["Email"] = null;
        //            return RedirectToAction("Homepage");
        //        }


        //*********************************************************************


        public ActionResult Signup()
        {
          
            return View();
        }

        [HttpPost]
        public ActionResult Signup(PatientSignUp patient)
        {
            if (!ModelState.IsValid)
            {
              
                return View("Signup");
            }
            try
            {
                Patient p1 = new Patient()
                {
                    Name = patient.Name,
                    Email = patient.Email,
                    NationalID = patient.NationalID,
                    Address = patient.Address,
                    Phone = patient.Phone,
                    Age = patient.Age

                };
                db.Patients.Add(p1);
                db.SaveChanges();
                int patientID = p1.ID;
                //Patient newpatient = db.Patients.FirstOrDefault(x => x.Name == patient.Name && x.Password == patient.Password);
                UserAccount acc = new UserAccount
                {
                    Username = patient.Username,
                    password = patient.password,
                    PatientID = patientID,
                    RoleID = 1

                };
                db.UserAccounts.Add(acc);
                db.SaveChanges();
                Session["Name"] = patient.Name;
                Session["UID"] = patientID;
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                
                return View("Signup");
            }
           
        }

        //        //*****************************************************************
        //        [HttpGet]
        //        public ActionResult Reservation1(int id)
        //        {
        //            //if (Session["ID"] == null)
        //            //{
        //            //    return RedirectToAction("Login");
        //            //}
        //            Booking Book = new Booking();
        //            Reservation r = new Reservation();
        //            r.HospitalID = id;
        //            r.PatientID = (int)Session["UID"];

        //            Book.Specialties = db.Specialties.ToList();

        //            return View(Book);
        //        }

        //****************************************************************
        [HttpGet]
        public ActionResult Reservation()
        {
            if (Session["UID"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            Booking B = new Booking();
            B.Hospital = db.Hospitals.ToList();
            return View(B);
        }

        [HttpPost]
        public ActionResult Reservation(Booking Book)
        {
            if (Session["UID"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    Reservation res = new Reservation();
                    res.ReservationDate = Book.Date;
                    res.HospitalID = Book.HospitalId;
                    res.SpecialtyID = Book.SpecialityId;
                    res.PatientID = (int)Session["UID"];
                    OutpatientClinic d = db.OutpatientClinics.FirstOrDefault(x =>  x.DoctorID == Book.DoctorId);
                    res.BookID = d.ID;
                    db.Reservations.Add(res);

                    db.SaveChanges();
                    return Json(new { result = 1 });
                }
                catch (Exception ex)
                {
                    return Json(new { result = 0 });
                }

            }
            return Json(new { result = 0 });
        }
        public ActionResult GetSpeciality(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            // List<Specialty> spec = db.Specialties.Where(x => x.HospitalID == id).ToList();
            
            List<Specialty> spec = speciality.FetchAll_Speciality(id);

            return Json(spec, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetIllness(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            // List<Specialty> spec = db.Specialties.Where(x => x.HospitalID == id).ToList();
            // SpecialityADO speciality = new SpecialityADO();
            List<Illness> illnesses = db.Illnesses.Where(x => x.SpecialtyID == id).ToList();

            return Json(illnesses, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDoctor(int Hid, int Sid)
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<Doctor> doctors = db.Doctors.Where(x => x.HospitalID == Hid && x.SpecialtyID == Sid).ToList();

            return Json(doctors, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Getschedule(int Hid, int Sid, int Did)
        {
            db.Configuration.ProxyCreationEnabled = false;

            OutpatientClinic schedule = db.OutpatientClinics
                     .FirstOrDefault(y => y.DoctorID == Did);

            return Json(schedule, JsonRequestBehavior.AllowGet);
        }

        //______________________________________________________________________________________________________________
        [HttpGet]
        public ActionResult AddDiagnosis(int id)
        {
            if (Session["UID"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            List<Specialty> spec = speciality.FetchAll_Speciality(id);
            HistoryIllness historyIllness = new HistoryIllness();
            historyIllness.Specialties = spec;
            return View(historyIllness);
        }
        [HttpPost]
        public ActionResult AddDiagnosis(HistoryIllness historyIllness)
        {
            if (Session["UID"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            if (!ModelState.IsValid)
            {
                return Json(new { result = 0 });
            }
          
            try
            {
                db.Configuration.ProxyCreationEnabled = false;

                PatientDoctor patientDoctor = new PatientDoctor();
                patientDoctor.DoctorID = historyIllness.DoctorId;
                patientDoctor.IllnessID = historyIllness.IllnessID;
                patientDoctor.PatientID = (int)Session["UID"];
                patientDoctor.Diagnosis = historyIllness.Diagnosis;
                db.PatientDoctors.Add(patientDoctor);
                db.SaveChanges();
                return Json(new { result = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { result = 0 });
            }
        }

        public ActionResult GetDoctor2( int Sid)
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<Doctor> doctors = db.Doctors.Where(x => x.SpecialtyID == Sid).ToList();

            return Json(doctors, JsonRequestBehavior.AllowGet);
        }

        //____________________________________________________________________________________
        [HttpGet]
        public ActionResult MyReservation()
        {
            if (Session["UID"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            int id = (int)Session["UID"];
            List<Reservation> re = db.Reservations.Where(x => x.PatientID == id).Include(p => p.Specialty).Include(x => x.Hospital).Include(y => y.Patient).ToList();
            return View(re);
        }

        public ActionResult CancelReservation(int id)
        {
            Reservation rs = db.Reservations.FirstOrDefault(x => x.ID == id);
            db.Reservations.Remove(rs);
            db.SaveChanges();
            return RedirectToAction("MyReservation");
        }

        //*****************************************************************************************
        public ActionResult History()
        {
            if (Session["UID"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            int id = (int)Session["UID"];
            List<PatientDoctor> patientDoctors = db.PatientDoctors.Where(p => p.PatientID == id).Include(x=>x.Patient).Include(x=>x.Doctor).Include(x=>x.Illness).ToList();
            return View(patientDoctors);
        }


        //        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
     

        //        public JsonResult GetSearch(string search)
        //        {
        //            db.Configuration.ProxyCreationEnabled = false;
        //            List<Hospital> allsearch = db.Hospitals.Where(x => x.Name.Contains(search)).ToList();
        //            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //        }
    }
}