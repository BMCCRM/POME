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
    
    public partial class Option
    {
        public int ID { get; set; }
        public Nullable<int> PAnswerID { get; set; }
        public Nullable<int> QuestionID { get; set; }
        public string Answer { get; set; }
        public Nullable<bool> IsInput { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string System { get; set; }
    }
}
