using RDATA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Diagnostics;
using System.IO.Pipelines;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RModel.DTO
{
    public class STUDENT_MARKS_AMDTOold
    {
        public string? ROLL_NO { get; set; }
        public string? PCODE { get; set; }
        public int? MARKS_OBT { get; set; }
        public int? PRAC_OBT { get; set; }
        public int? INT_OBT { get; set; }
        public int? SES_OBT { get; set; }
        public int? TOT_INT_SES_OBT { get; set; }
        public int? PAPER_TOT_MARKS_OBT { get; set; }
        public string? SUBJECT_NAME { get; set; }
        public string? CODE { get; set; }
        public string? LOT_NO { get; set; }
        public string? GRACE { get; set; }
        public string? REMARKS { get; set; }
        public string? SUBJ_GRACE { get; set; }
        public string? THEORY_GRACE { get; set; }
        public string? UFM { get; set; }
        public string? COURSE_ID { get; set; }
        public string? EXAM_TYPE { get; set; }
        public string? SEM_NO { get; set; }
        public string? PAPER_RESULT { get; set; }
        public string? SUBJECT_RESULT { get; set; }
        public string? COURSE_NAME { get; set; }
        public string? Grade { get; set; }
        public int? Credit { get; set; }
        public decimal? EarnCredit { get; set; }
        public decimal? Point { get; set; }
        public int? IsNotOk { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UPDATEON { get; set; }
        public string? PCODE_N { get; set; }
        public decimal? GradePoint { get; set; }
        public int? MARKS_OBT_NEW { get; set; }
        public int? MaxInt { get; set; }
        public int? MaxExt { get; set; }
        public int? MaxTot { get; set; }
        public int? MinInt { get; set; }
        public int? MinExt { get; set; }
        public int? MinTot { get; set; }
        public bool? IsStarTh { get; set; }
        public bool? IsStarInt { get; set; }
        public string? SUBJECT_TYPE { get; set; }
        //-------med-----------
        //public int? P1_MED { get; set; }
        //public int? P2_MED { get; set; }
        //public int? PTOT_MED { get; set; }
        //public int? PP1_MED { get; set; }
        //public int? TOT_MED { get; set; }
    }
    public class STUDENT_MARKS_AMDTO
    {

        public string? STD_MARKS_ID { get; set; }
        public string? COURSE_NAME { get; set; }
        public string? SEM_NO { get; set; }
        public string? COURSE_TYPE { get; set; }
        public string? SESSION { get; set; }
        public string? ROLL_NO { get; set; }
        public string? PCODE { get; set; }
        public string? SUBJECT_NAME { get; set; }
        public string? PNAME_R { get; set; }
        public string? MARKS_OBT { get; set; }
        public string? INT_OBT { get; set; }
        public string? SES_OBT { get; set; }
        public string? PAPER_TOT_MARKS_OBT { get; set; }
        public string? PAPER_RESULT { get; set; }
        public string? SUBJECT_RESULT { get; set; }
        public string? SUBJECT_TYPE { get; set; }
        public string? Grade { get; set; }
        public string? Credit { get; set; }
        public string? EarnCredit { get; set; }
        public string? Point { get; set; }
        public string? GradePoint { get; set; }
        public string? CODE { get; set; }
        public string? PAPER { get; set; }
        public string? PRINT_SRNO { get; set; }
        public string? LOT_NO { get; set; }
        public string? GRACE { get; set; }
        public string? SUBJ_GRACE { get; set; }
        public string? THEORY_GRACE { get; set; }
        public string? UFM { get; set; }
        public string? REMARKS { get; set; }
        public string? ChangedMarks { get; set; }
        public string? RE_EX { get; set; }
        public string? COURSE_ID { get; set; }
        public string? EXAM_TYPE { get; set; }
        public string? IsNotOk { get; set; }
        public string? CreatedOn { get; set; }
        public string? UPDATEON { get; set; }
        public string? Marks_Avrg_Remarks { get; set; }
        public string? CREATEDBY { get; set; }
        public string? IS_NOT_PROCESS { get; set; }
        public string? StudentCourseSubject_ID { get; set; }
        public string? LAST_UPDATED { get; set; }
        public string? UPDATED_BY { get; set; }
        public string? WebCourseName { get; set; }
        public string? HELD_IN { get; set; }
        public string? SUBJECT_ID { get; set; }
        public string? STD_COURSE_SUB_ID { get; set; }
        public string? ISRWMARKS { get; set; }
        public string? UFMRemark { get; set; }

    }
}