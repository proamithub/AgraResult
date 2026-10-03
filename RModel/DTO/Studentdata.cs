using RDATA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RModel.DTO
{
    public class Studentdata
    {
       
        public string ENTRY { get; set; }
        public string? ROLLNO { get; set; }
        public string EN_NO { get; set; }
        public string NAME { get; set; }
        public string FNAME { get; set; }
        public string MNAME { get; set; }
        public string INSTCODE { get; set; }
        public string SESSION { get; set; }
        public string Regno { get; set; }
        public string Gender { get; set; }
        public string sdob { get; set; }
        public string CAT { get; set; }
        public string COURSENAME { get; set; }
        public string IsVerified { get; set; }
        public string WRN { get; set; }
        public string IsVerifiedForPayment { get; set; }
        public string BatchNo { get; set; }
        public string FeeAmount { get; set; }
        public string Added { get; set; }
        

    }
}