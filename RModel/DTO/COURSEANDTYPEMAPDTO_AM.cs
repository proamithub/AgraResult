using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RModel.DTO
{
    public class COURSEANDTYPEMAPDTO_AM
    {

        public int EntryID { get; set; }
        public int CourseID { get; set; }
        public int CTYPEID { get; set; }


    }
}
