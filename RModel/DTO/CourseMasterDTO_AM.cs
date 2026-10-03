using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RModel.DTO
{
    public class CourseMasterDTO_AM
    {
        
        public int EntryID { get; set; }
        public int CourseID { get; set; }
        public string? CourseName { get; set; }
        public string? DisplayName { get; set; }
 
        public string? CourseType { get; set; }
        public string? CourseNameReal { get; set; }
        public string? CollegeCode { get; set; }
        public string? Semester { get; set; }
        public string? DisplaySemester { get; set; }
        
        public string? Session { get; set; }
        
        public decimal Sequence { get; set; }
        public bool IsActive { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? TemplateName { get; set; }
        public string? LinkPath { get; set; }
        public bool? IsPG { get; set; }
        public string? ImageURL { get; set; }
        public bool? IsNew { get; set; }
        public string? Rules { get; set; }
        public string? CtypeID { get; set; }
        public string? TotalCount { get; set; }
        public string? NotLivedCount { get; set; }
        public string? LivedCount { get; set; }
        public bool IsCount { get; set; } = false;
         public bool? AllowForConsolidate { get; set; }
        public bool? IsTestingActive { get; set; }
        public List<COURSEANDTYPEMAPDTO_AM> CtypeMapList { get; set; }
        //public Enumerable<SelectListItem> CtMaster { get; set; }
        

    }

    public class SearchDTO
    {
        public int CourseID { get; set; }
        public string? CourseName { get; set; }
        
        public string? CourseType { get; set; }
        public DateTime? DOB { get; set; }
        public string? DOBstr { get; set; }
        public string? FName { get; set; }
        public string? RollNumber { get; set; }
        public string? ExamTypeName { get; set; }
        public string? SessionName { get; set; }
        public string? HELD_IN { get; set; }
        public string? IsAdmin { get; set; }
        

    }
}
