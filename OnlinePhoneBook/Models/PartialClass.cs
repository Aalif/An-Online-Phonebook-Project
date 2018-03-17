using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace OnlinePhoneBook.Models
{
    //Partial Class of contactdetails
    [MetadataType(typeof(contactdetails_tbmetadata))]
    public partial class contactdetails_tb
    {
        public HttpPostedFileBase file { get; set; }
    }
    //this is contactdetails metdata class
       public class contactdetails_tbmetadata
    {
        public int id { get; set; }
        [Display(Name ="First Name")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Enter Name")]
        public string fname { get; set; }
        [Display(Name ="Last Name")]
        public string lname { get; set; }
        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "Enter a phone number")]
        [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?([-. (]*(\d{3})[-. )]*)?((\d{3})[-. ]*(\d{2,4})(?:[-.x ]*(\d+))?)\s*$", ErrorMessage = "Enter a valid phone number")]
        public string maincontact_number { get; set; }
        [Display(Name = "Optional Contact Number")]
        [DisplayFormat(NullDisplayText ="Contact number is missing")]
        [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?([-. (]*(\d{3})[-. )]*)?((\d{3})[-. ]*(\d{2,4})(?:[-.x ]*(\d+))?)\s*$", ErrorMessage = "Enter a valid phone number")]
        public string secondarycontact_number { get; set; }
        [Display(Name = "Address")]
        [DisplayFormat(NullDisplayText ="Optional Contact number is missing")]
        public string address { get; set; }
        [Display(Name = "Email")]
        [DisplayFormat(NullDisplayText = "Email is missing")]
        [RegularExpression("^[^@]+@[^@]+$", ErrorMessage ="Enter a valid email")]
        public string email { get; set; }
        [Display(Name = "Image")]
        public string imagepath { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Please select a country")]
        public Nullable<int> country_fid { get; set; }
        [Display(Name = "Types")]
        [Required(ErrorMessage = "Please select a type")]
        public Nullable<int> type_fid { get; set; }


    }
    //Partial Class of type
    [MetadataType(typeof(type_tbmetadata))]
    public partial class type_tb
    {

    }
    //this is type metadata class
    public class type_tbmetadata
    {
        public int id { get; set; }
        [Display(Name ="Types")]
        public string type { get; set; }
    }
    //Partial Class of Country
    [MetadataType(typeof(countrymetadata))]
    public partial class Country_tb
    {
    }

        //this is country metadata class
        public class countrymetadata
    {
        public int id { get; set; }
        [Display(Name ="Country")]
        [Required(ErrorMessage ="Please select a country")]
        public string country_name { get; set; }
    }
    [MetadataType(typeof(user_tbmetadata))]
    public partial class user_tb
    {

    }
    public  class user_tbmetadata
    {
        public int id { get; set; }
        [Display(Name ="User name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter User Name")]
        public string user_name { get; set; }
        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter email")]
        [RegularExpression("^[^@]+@[^@]+$", ErrorMessage = "Enter a valid email")]
        public string user_email { get; set; }
        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter password")]
        public string password { get; set; }
    }

}