using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TMS.Models.Database.Entity
{
    public class AdvertiserinfoModel
    {

        public int advertiserid { get; set; }

        [Required(ErrorMessage = "Please enter User Name")]
        public string username { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        public string password { get; set; }
        [Required(ErrorMessage = "Please enter Email ID")]
        public string emailid { get; set; }

    }
}