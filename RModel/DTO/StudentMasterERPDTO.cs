using RDATA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RModel.DTO
{
    public class StudentMasterERPDTO
    {
        public int? MasterId { get; set; }
        public string? StudentName { get; set; }
        public string? FatherName { get; set; }

        public string? MotherName { get; set; }
        public string? GenderCode { get; set; }
        public DateTime? Dob { get; set; }
        public string? CategoryCode { get; set; }

        public string? CollegeCode { get; set; }
        public string? CollegeName { get; set; }
        public int? CourseId { get; set; }
        public string? CourseName { get; set; }
        public string? AdmissionSession { get; set; }
        public string? EnrollmentNo { get; set; }
        public string? RollNo1 { get; set; }
        public string? RollNo2 { get; set; }
        public string? RollNo3 { get; set; }
        public string? RollNo4 { get; set; }
        public string? RollNo5 { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? PhotoUrl { get; set; }
        public string? SignatureUrl { get; set; }
        public string? Subject1 { get; set; }
        public string? Subject2 { get; set; }
        public string? Subject3 { get; set; }
        public string? SourceType { get; set; }
        public bool? IsPhotoExist { get; set; }
        public string? mpath { get; set; }
        public bool? IsPhoto { get; set; }
        public bool? IsEnrollmentNo { get; set; }
        public string? HeldIn { get; set; }
        public string? ResultDate { get; set; }
        public bool? IsNep { get; set; }
        public string? LastSession { get; set; }
        public byte[]? qcore { get; set; }
        

        public string? CorrectEnrollmentNo { get; set; }
        public string? FinalDivision { get; set; }
        public string? FinalResult { get; set; }
        public List<StudentMarksERPNepDTO> Marks1ERPList { get; set; }
        public List<StudentMarksERPNepDTO> Marks2ERPList { get; set; }
        public List<StudentMarksERPNepDTO> Marks3ERPList { get; set; }
        public List<StudentMarksERPNepDTO> Marks4ERPList { get; set; }
        public List<StudentMarksERPNepDTO> Marks5ERPList { get; set; }
        public List<StudentMarksERPNepDTO> Marks6ERPList { get; set; }

        public StudentResultERPDTO Result1ERPList { get; set; }
        public StudentResultERPDTO Result2ERPList { get; set; }
        public StudentResultERPDTO Result3ERPList { get; set; }
        public StudentResultERPDTO Result4ERPList { get; set; }
        public StudentResultERPDTO Result5ERPList { get; set; }
        public StudentResultERPDTO Result6ERPList { get; set; }

    }
}