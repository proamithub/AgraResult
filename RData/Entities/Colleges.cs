using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RDATA.Entities
{
    public class Colleges
    {

        [Key]
        public int CollegeID { get; set; }
        //public byte? CollegeType { get; set; }

        //public string? CollegeName { get; set; }
        //public string? CollegeCode { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? NameHindi { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? LoginPassword { get; set; }
        public bool? IsActive { get; set; }
        public string? CITY { get; set; }


    }
    public class CollegesAgra
    {

        
        public string  INSTCODE { get; set; }
        public string INSTNAME { get; set; }


    }
}