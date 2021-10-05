using ProjectMVC.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMVC.Controllers
{
    public class AdminController : Controller
    {
        HMSEntities db = new HMSEntities();
   //**************************************************************************************
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["ID_AH"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
       
            List<Hospital> allDataHospital = db.Hospitals.ToList();
            return View(allDataHospital);
        }
        //************************************************************************************************
        [HttpGet]
        public ActionResult Detail(int id)
        {
            if (Session["ID_AH"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            Hospital hospital = db.Hospitals.Where(x => x.ID == id).FirstOrDefault();
            return View(hospital);
        }
        //**********************************************************************************************   
        [HttpGet]
        public ActionResult Add()
        {
            if (Session["ID_AH"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Add(Hospital hospital)
        {
            if (Session["ID_AH"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Hospitals.Add(hospital);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index");
                }

            }
            return View("Add");
        }
        //**************************************************************************************************
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["ID_AH"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            if (id != null)
            {
                Hospital h = db.Hospitals.Where(x => x.ID == id).FirstOrDefault();
                db.Hospitals.Remove(h);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        //***********************************************************************************************

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["ID_AH"] == null)
            {
                return RedirectToAction("Login", "LoginAdminHospital");
            }
            Hospital h = db.Hospitals.Where(x => x.ID == id).FirstOrDefault();
            return View(h);
        }
        [HttpPost]
        public ActionResult Edit(Hospital hospital)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var EditHospital = db.Hospitals.Single(x => x.ID == hospital.ID);
                    EditHospital.Name = hospital.Name;
                    EditHospital.NumOfAllBed = hospital.NumOfAllBed;
                    EditHospital.HospitalLongitude = hospital.HospitalLongitude;
                    EditHospital.HospitalLatitude = hospital.HospitalLatitude;
                    // EditHospital.NumOfAvailableBed = hospital.NumOfAvailableBed;
                    EditHospital.Phone = hospital.Phone;
                    EditHospital.Address = hospital.Address;
                    //EditHospital.Logo = hospital.Logo;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index");
                }

            }
            return View("Add");
        }

    }
}