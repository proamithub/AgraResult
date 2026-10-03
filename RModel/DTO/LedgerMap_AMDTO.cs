using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RModel.DTO
{
    public class LedgerMap_AMDTO
    {
        public int EntryID { get; set; }
        public int LedgerId { get; set; }
        public string? Path { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }
    }

    public class TblLedgerFile_AM
    {
        public string? FilePath { get; set; }
        public string? Name { get; set; }
    }

}
