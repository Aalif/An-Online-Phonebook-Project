//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlinePhoneBook.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class contactdetails_tb
    {
        public int id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string maincontact_number { get; set; }
        public string secondarycontact_number { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string imagepath { get; set; }
        public Nullable<int> country_fid { get; set; }
        public Nullable<int> type_fid { get; set; }
    
        public virtual Country_tb Country_tb { get; set; }
        public virtual type_tb type_tb { get; set; }
    }
}
