using ProjectAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ProjectAPI.ViewModel;

namespace ProjectAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]

    public class AdminController : ApiController
    {
        HMSEntities db = new HMSEntities();

        public HttpResponseMessage Get()
        {
            GetHospital getHospital = new GetHospital();

            // var LocationAllHospital = db.Hospitals.Select(p=>new { p.Name, p.HospitalLatitude,p.HospitalLongitude,p.NumOfAllBed /*, p.NumOfAvailableBed*/}).ToList();
            // var LocationAllHospital = getHospital.FetchAll_hospitals();
           var LocationAllHospital = db.HospitalsApis.Select(p=>new { p.Name, p.HospitalLatitude,p.HospitalLongitude,p.NumOfAllBed , p.NumAvailable}).ToList();


            try
            {
                if (LocationAllHospital == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                return Request.CreateResponse(HttpStatusCode.OK, LocationAllHospital);
            }catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex);
            }
           
        }


        public HttpResponseMessage GetallDetail(int id)
        {
            var HospitalDetail = db.Hospitals.Where(x => x.ID == id).Select(p => new {p.ID, p.Name, p.NumOfAllBed, p.Phone, p.Address, p.Doctors/*, p.NumOfAvailableBed*/ }).FirstOrDefault();
            var AvailableBed = db.Reservations.Where(x => x.AdmitOrDecline == true && x.HospitalID == id).Count();
            Hospitals hospitals = new Hospitals()
            {
                ID = HospitalDetail.ID,
                Name = HospitalDetail.Name,
                NumOfAllBed = HospitalDetail.NumOfAllBed,
                Address = HospitalDetail.Address,
                Phone = HospitalDetail.Phone,
                NumOfAvailableBed = (HospitalDetail.NumOfAllBed-AvailableBed),
                
            };

            try
            {
                if (hospitals == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                return Request.CreateResponse(HttpStatusCode.OK, hospitals);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex);
            }

        }
    }
}
