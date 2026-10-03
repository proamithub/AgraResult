using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RModel.DTO
{
    public class Ledger_AMDTO
    {
        public int EntryID { get; set; }
        public string InvoiceNo { get; set; }
        public string? LedgerName { get; set; }
        public DateTime? PurchasedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal Amount { get; set; }
        public int? CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public bool? IsActive { get; set; }

        public List<TblLedgerFile_AM> FileList { get; set; }
        public string? IpAddress { get; set; }
        public string? Attachments { get; set; }

        
    }

}
