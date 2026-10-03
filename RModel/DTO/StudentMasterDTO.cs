using RDATA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RModel.DTO
{
    public class StudentMasterDTO
    {
        public long? ENTRYID { get; set; }
        public string? CCODE { get; set; }
        public string? CNAME { get; set; }

        public string? REG_NO { get; set; }
        public int? COURSE_ID { get; set; }
        public string? CourseName { get; set; }
        public string? DisplayCourseName { get; set; }

        public string? ROLL_NO { get; set; }
        public string? ENROLLMENT_NO { get; set; }
        public string? NAME { get; set; }
        public string? FNAME { get; set; }
        public string? MNAME { get; set; }
        public string? HELD_IN { get; set; }
        public string? EXAM_TYPE { get; set; }
        public string? SEX { get; set; }
        public DateTime? DOB { get; set; }
        public string? MOBILE_NO { get; set; }
        public string? MPATH { get; set; }
        public string? PHOTOGRAPH { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public bool? IsActive { get; set; }
        public string? SEM_NO { get; set; }
        public string? EncrptedRoll { get; set; }
        public string? TemplateName { get; set; }
        public string? ImageURL { get; set; }
        public byte[] qcore { get; set; }
        public string? DisplaySemester { get; set; }
        public string? STD_Remarks { get; set; }
        public string? SessionName { get; set; }

        public List<STUDENT_MARKS_AMDTO> MarksList { get; set; }
        public List<STUDENT_RESULT_AMDTO> ResultList { get; set; }
        public List<CSP_MASTER_AMDTO> CSPList { get; set; }

        public string? PNAME { get; set; }
        public DateTime? ResultDate { get; set; }
        public string? Rules { get; set; }
        public string? RESULT { get; set; }
        public string? FINAL_RESULT { get; set; }
        public string? CourseType { get; set; }
        public bool? IsLive { get; set; }
        public int? GroupID { get; set; }
        public string? GroupName { get; set; }
        public bool? IS_BE_LITERAL { get; set; }
        public string? DisplayRemarks { get; set; }
        public bool? AllowForConsolidate { get; set; }
        public bool? IsImgAnimate { get; set; }
    }
}