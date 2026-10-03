using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace RModel.DTO
{
    public class ResultReprtDTO
    {

        public string? Flag { get; set; }
        public string? CourseIDs { get; set; }
        public string? Sem { get; set; }
        public string? Session { get; set; }


    }


    public class PendingReprtDTO
    {

        public string? Flag { get; set; }
        public string SessionP { get; set; }
        public string CourseidsP { get; set; }
        public string SemesterS { get; set; }
        public string PaperType { get; set; }
        public bool IsCtypeU { get; set; }
        public bool IsCtypeP { get; set; }
        public bool IsCtypeA { get; set; }
        public bool IsCtypeS { get; set; }


    }

}