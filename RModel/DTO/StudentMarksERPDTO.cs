using RDATA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RModel.DTO
{
    public class StudentMarksERPNepDTO
    {
        public int? MasterId { get; set; }
        public int? ResultId { get; set; }
        public int? ExternalId { get; set; }

        public int? SubjectSerial { get; set; }
        public int? PaperSerial { get; set; }
        public string? SubjectName { get; set; }
        public string? SubjectType { get; set; }

        public string? PaperCode { get; set; }
        public string? PaperNumber { get; set; }
        public string? PaperName { get; set; }
        public string? TheoryMinMarks { get; set; }
        public string? TheoryMaxMarks { get; set; }
        public string? InternalMaxMarks { get; set; }
        public string? PracticalMaxMarks { get; set; }
        public string? TotalMinMarks { get; set; }
        public string? TotalMaxMarks { get; set; }
        public string? TheoryMarks { get; set; }
        public string? InternalMarks { get; set; }
        public string? PracticalMarks { get; set; }
        public string? TotalMarks { get; set; }
        public string? Credit { get; set; }
        public string? CreditPoint { get; set; }
        public string? Grade { get; set; }
        public string? GradePoint { get; set; }
        public string? PaperStatus { get; set; }
        public string? PaperCodeN { get; set; }
        

    }

    public class StudentMarksERPDTO
    {
        public int? MasterId { get; set; }
        public int? ResultId { get; set; }
        public int? ExternalId { get; set; }

        public int? SubjectSerial { get; set; }
        public int? PaperSerial { get; set; }
        public string? SubjectName { get; set; }
        public string? SubjectType { get; set; }

        public string? PaperCode { get; set; }
        public string? PaperNumber { get; set; }
        public string? PaperName { get; set; }
        public string? TheoryMinMarks { get; set; }
        public string? TheoryMaxMarks { get; set; }
        public string? InternalMaxMarks { get; set; }
        public string? PracticalMaxMarks { get; set; }
        public string? TotalMinMarks { get; set; }
        public string? TotalMaxMarks { get; set; }
        public string? TheoryMarks { get; set; }
        public string? InternalMarks { get; set; }
        public string? PracticalMarks { get; set; }
        public string? TotalMarks { get; set; }
        public string? Credit { get; set; }
        public string? CreditPoint { get; set; }
        public string? Grade { get; set; }
        public string? GradePoint { get; set; }
        public string? PaperStatus { get; set; }
        public string? PaperCodeN { get; set; }
    }
}