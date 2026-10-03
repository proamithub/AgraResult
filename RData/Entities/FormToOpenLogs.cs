using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;

namespace RDATA.Entities
{
    public class FormToOpenLogs
    {
        [Key]
        public long EntryID { get; set; }
        public string WRN_OLD { get; set; }

        public string? INSTCODE { get; set; }
        public string? INSTCODE_OLD { get; set; }
        public string? COURSENAME { get; set; }
        public string? COURSENAME_OLD { get; set; }
        public string? WRN { get; set; }
        public bool? IsVerified { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? EntryBy { get; set; }

    }
}
