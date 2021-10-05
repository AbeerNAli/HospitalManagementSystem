using ProjectMVC.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMVC.Controllers
{
    public class SearchController : Controller
    {
        HMSEntities db = new HMSEntities();
        public ActionResult Index()
        {

            return View(db.Hospitals);
        }

        [HttpPost]
        public ActionResult Index(string searchTerm)
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

        public JsonResult GetHospitals(string term)
        {
            List<string> hospitals = db.Hospitals.Where(s => s.Name.StartsWith(term)).Select(x => x.Name).ToList();
            return Json(hospitals, JsonRequestBehavior.AllowGet);
        }

    }
}