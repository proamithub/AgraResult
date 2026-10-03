using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RModel.DTO.Result
{
    public class StudentMasterDTO_g
    {
        public long RecId { get; set; }
        public string? CollegeCode { get; set; }
        public string? RegNo { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        public string? RollNo { get; set; }
        public string? EnrolmentNo { get; set; }
        public string? Name { get; set; }
        public string? FName { get; set; }
        public string? MName { get; set; }
        public string? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string? AadharNo { get; set; }
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
        public string? PhCategory { get; set; }
        public string? Religion { get; set; }
        public string? MobileNo { get; set; }
        public string? GuardianMobileNo { get; set; }
        public string? Email { get; set; }
        public string? PAddress1 { get; set; }
        public string? PAddress2 { get; set; }
        public string? PState { get; set; }
        public string? PDistrict { get; set; }
        public int? PPinCode { get; set; }
        public string? CAddress1 { get; set; }
        public string? CAddress2 { get; set; }
        public string? CState { get; set; }
        public string? CDistrict { get; set; }
        public int? CPinCode { get; set; }
        public string? PhotoPath { get; set; }
        public string? Remarks { get; set; }
        public string? UpdateRemarks { get; set; }
        public bool? IsSync { get; set; }
        public string? SyncBy { get; set; }
        public DateTime? SyncDate { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? MigratedTableRecId { get; set; }
        public string? MigratedTableName { get; set; }

    }

}
