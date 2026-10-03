using RDATA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RModel.DTO
{
    public class StudentResultERPDTO
    {
        public int? ResultId { get; set; }
        public int? MasterId { get; set; }
        public string? Session { get; set; }

        public string? ExamType { get; set; }
        public int? YearSem { get; set; }
        public string? RollNo { get; set; }
        public string? RollNo1 { get; set; }

        public string? RollNo2 { get; set; }
        public string? RollNo3 { get; set; }
        public int? MinMarks { get; set; }
        public int? MaxMarks { get; set; }
        public int? ObtMarks { get; set; }
        public string? Result { get; set; }
        public string? Grace { get; set; }
        public string? Remarks { get; set; }
        public string? ResultWithNotification { get; set; }
        public string? OptionalPaperMarks { get; set; }
        public string? OptionalPaperRemarks { get; set; }
        public string? OptionalPaperStatus { get; set; }
        public int? OptionalPaperMaxMarks { get; set; }
        public string? Subject1 { get; set; }
        public string? Subject2 { get; set; }
        public string? Subject3 { get; set; }
        public string? CollegeCode { get; set; }
        public int? CourseId { get; set; }
        public string? ResultDate { get; set; }
        public int? PracticalMin { get; set; }
        public int? PracticalMax { get; set; }
        public int? PracticalObt { get; set; }
        public int? TheoryMin { get; set; }
        public int? TheoryMax { get; set; }
        public int? TheoryObt { get; set; }
        public string? MinorSubject { get; set; }
        public string? VocationalSubject { get; set; }
        public string? CoCurricularSubject { get; set; }
        public string? TotalCredit { get; set; }
        public string? EarnedCredit { get; set; }
        public string? TotalPoint { get; set; }
        public string? SGPA { get; set; }
        public string? HeldIn { get; set; }
        public int? InternalMin { get; set; }
        public int? InternalMax { get; set; }
        public int? InternalObt { get; set; }
        public string? TheoryDivision { get; set; }
        public string? PracticalDivision { get; set; }
        public string? ApplicationNumber { get; set; }
        public string? CGPA { get; set; }
        

    }
}