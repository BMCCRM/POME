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
    
    public partial class SurveyResponse
    {
        public int ID { get; set; }
        public Nullable<int> PharmacistID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string System { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public Nullable<System.DateTime> SurveyDateTime { get; set; }
    }
}
