using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RModel.DTO.Result
{
    public class ResultViewedDTO
    {
        public int? COURSE_ID { get; set; }
        public string COURSE_NAME { get; set; }
        public string SEM_NO { get; set; }
        public string Total { get; set; }
        public string NotLive { get; set; }
        public string Live { get; set; }
        public string Viewed { get; set; }
        public string Per { get; set; }
        public string Session { get; set; }
        public bool? IsTestingActive { get; set; }
        



    }

}
