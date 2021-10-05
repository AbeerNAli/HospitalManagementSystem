//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Doctor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Doctor()
        {
            this.OutpatientClinics = new HashSet<OutpatientClinic>();
            this.PatientDoctors = new HashSet<PatientDoctor>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string NationalID { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string DescriptionJob { get; set; }
        public int SpecialtyID { get; set; }
        public int HospitalID { get; set; }
    
        public virtual Hospital Hospital { get; set; }
        public virtual Specialty Specialty { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OutpatientClinic> OutpatientClinics { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientDoctor> PatientDoctors { get; set; }
    }
}
