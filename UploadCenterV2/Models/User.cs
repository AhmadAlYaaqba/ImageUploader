//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UploadCenterV2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        public int User_ID { get; set; }
        [Required(ErrorMessage = "Please Enter your Fullname")]
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please Enter your Username")]
        [DisplayName("User Name")]
        public string username { get; set; }
        [Required(ErrorMessage = "Please Enter your Password")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("password")]
        public string ConfirmPassword { get; set; }
        public string LoginErrorMessage { get; set; }
    }
}
