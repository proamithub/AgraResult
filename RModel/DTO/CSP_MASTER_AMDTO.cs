using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RModel.DTO
{
    public class CSP_MASTER_AMDTO
    {
        public string? CSP_ID { get; set; }
        public string? COURSE_NAME { get; set; }
        public string? EXAM_TYPE { get; set; }
        public string? YEAR_SEMESTER { get; set; }
        public string? PCODE { get; set; }
        public string? PNAME { get; set; }
        public string? SUBJECT_NAME { get; set; }
        public string? PNAME_R { get; set; }
        public string? PTYPE { get; set; }
        public string? PGROUP_NO { get; set; }
        public decimal? THEORY_MAX { get; set; }
        public decimal? THEORY_MIN { get; set; }
        public decimal? PRACTICAL_MAX { get; set; }
        public decimal? PRACTICAL_MIN { get; set; }
        public decimal? INTERNAL_MAX { get; set; }
        public decimal? INTERNAL_MIN { get; set; }
        public decimal? SESSIONAL_MAX { get; set; }
        public decimal? SESSIONAL_MIN { get; set; }
        public decimal? PAPER_TOTAL_MIN { get; set; }
        
        public string? PNAME_H { get; set; }
        public string? REMARKS { get; set; }
        public string? COURSE_ID { get; set; }
        public string? PCODE_N { get; set; }
        public string? PaperCredit { get; set; }
        public bool? IsActive { get; set; }
        public int? PrintSrNo { get; set; }
        public int? PaperNo { get; set; }
        public decimal? MIN_INT_EXT_TOT  { get; set; }
        public decimal? MAX_INT_EXT_TOT  { get; set; }
        public string? Session { get; set; }
        public string? COURSE_TYPE { get; set; }
        public string? HELD_IN { get; set; }


    }

}
