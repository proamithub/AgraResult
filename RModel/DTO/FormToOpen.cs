using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;

namespace RModel
{
    public class FormToOpen
    {

        public string EN_NO { get; set; }
        public string? NAME { get; set; }
        public string? FNAME { get; set; }
        public string? MNAME { get; set; }
        public string? INSTCODE { get; set; }
        public string? SESSION { get; set; }
        public string? Regno { get; set; }
        public string? Gender { get; set; }
        public string? COURSENAME { get; set; }
        public string? WRN { get; set; }

    }
}
