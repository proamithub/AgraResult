using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RModel.DTO.Result
{
    public class ResultMasterDTO_AM
    {
        public int RId { get; set; }
        public int CSPId { get; set; }
        public string? DisplayName { get; set; }
        public string? CourseType { get; set; }
        public string? Semester { get; set; }
        public string? DisplaySemester { get; set; }
        public string? Session { get; set; }
        public decimal? Sequence { get; set; }
        public DateTime? PublishDate { get; set; }
        public int TempleteTypeID { get; set; }
        public string? FilePath { get; set; }
        public bool? IsPg { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }

    }

}
