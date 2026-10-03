using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RModel.DTO.Result
{
    public class StudentMarksDTO
    {


        public long RecId { get; set; }
        public long StudentId { get; set; }
        public long StudentResultId { get; set; }
        public long CSPId { get; set; }
        public string? RollNo { get; set; }
        public string? PaperCode { get; set; }
        public string? SubjectName { get; set; }
        public string? PaperTitle { get; set; }
        public string? SubjectType { get; set; }
        public string? CatchNo { get; set; }
        public int? PrintSrNo { get; set; }
        public string? CodedRollNo { get; set; }
        public string? LotNo { get; set; }
        public string? MarksObt { get; set; }
        public string? InternalMarksObt { get; set; }
        public string? SessionalMarksObt { get; set; }
        public string? PaperTotalMarksObt { get; set; }
        public string? ReBackRemark { get; set; }
        public int? Grace { get; set; }
        public string? SubjectGrace { get; set; }
        public string? TheoryGrace { get; set; }
        public string? Remarks { get; set; }
        public string? UFM { get; set; }
        public string? PaperResult { get; set; }
        public string? SubjectResult { get; set; }
        public string? Grade { get; set; }
        public int? Credit { get; set; }
        public int? EarnCredit { get; set; }
        public decimal? Point { get; set; }
        public decimal? GradePoint { get; set; }
        public string? MarksAvrgRemarks { get; set; }
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
