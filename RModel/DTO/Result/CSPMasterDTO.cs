using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RModel.DTO.Result
{
    public class CSPMasterDTO
    {
        public long RecId { get; set; }
        public string? Session { get; set; }
        public int? CourseId { get; set; }
        public string? CourseName { get; set; }
        public int? SemesterYear { get; set; }
        public string? SubjectName { get; set; }
        public string? PaperCode { get; set; }
        public string? PaperName { get; set; }
        public string? PaperTitle { get; set; }
        public string? PaperType { get; set; }
        public string? CatchNo { get; set; }
        public string? PaperGroup { get; set; }
        public int? TheoryMaxMarks { get; set; }
        public int? TheoryMinMarks { get; set; }
        public int? InternalMaxMarks { get; set; }
        public int? InternalMinMarks { get; set; }
        public int? SessionalMaxMarks { get; set; }
        public int? SessionalMinMarks { get; set; }
        public string? PaperNameInHindi { get; set; }
        public int? Credit { get; set; }
        public string? Remarks { get; set; }
        public string? PaperCodeNew { get; set; }
        public int? NoOfPaperInSubject { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }

}
