//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PomeDataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class HospitalDoctor
    {
        public int DoctorID { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> HospitalID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Speciality { get; set; }
        public string Designation { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string CNIC { get; set; }
        public string City { get; set; }
        public string Mobile { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string System { get; set; }
        public string Password { get; set; }
    }
}