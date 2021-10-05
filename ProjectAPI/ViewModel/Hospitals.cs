using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectAPI.ViewModel
{
    public class Hospitals
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string HospitalLongitude { get; set; }
        public string HospitalLatitude { get; set; }
        public Nullable<int> NumOfAllBed { get; set; }
        public Nullable<int> NumOfAvailableBed { get; set; }
    }
}