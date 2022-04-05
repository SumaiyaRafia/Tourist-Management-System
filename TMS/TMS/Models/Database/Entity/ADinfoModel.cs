using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TMS.Models.Database.Entity
{
    public class ADinfoModel
    {

        public int adid { get; set; }
        [Required(ErrorMessage = "Please enter Advertisement Name")]
        public string advertisename { get; set; }
        [Required(ErrorMessage = "Please enter Advertisement type")]
        public string advertisetype { get; set; }
        [Required(ErrorMessage = "Please enter your budget")]
        public decimal advertisebudget { get; set; }
        [Required(ErrorMessage = "Please enter your contact info")]
        public string contactno { get; set; }
        




    }
}