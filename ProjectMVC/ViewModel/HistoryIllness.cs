using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ProjectMVC.Context;

namespace ProjectMVC.ViewModel
{
    public class HistoryIllness
    {

        public int PatientId { get; set; }
     
        
        [Display(Name = "Spitiality")]
        public int SpecialityId { get; set; }
        public List<Specialty> Specialties { get; set; }

        [Display(Name = "Illness")]
        public int IllnessID { get; set; }
        public List<Illness> Illnesses { get; set; }

        [Display(Name = "Doctors")]
        public int DoctorId { get; set; }

        public List<Doctor> Doctors { get; set; }

        public string Diagnosis { get; set; }
        
        

    }
}