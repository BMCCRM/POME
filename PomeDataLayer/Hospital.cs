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
    
    public partial class Hospital
    {
        public int HospitalID { get; set; }
        public string HospitalName { get; set; }
        public string ContactNumber { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string System { get; set; }
    }
}
