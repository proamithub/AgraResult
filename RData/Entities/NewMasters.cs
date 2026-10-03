using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RDATA.Entities
{
    public class NewMasters
    {
        [Key]
        public long NewsID { get; set; }
        public string? NewsTitle { get; set; }
        public string? NewsDisplay { get; set; }
        public string? Description { get; set; }
        public string? NewsLink { get; set; }
        public DateTime PublishDate { get; set; }
        [NotMapped]
        public string? PublishDateStr { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string? FileName { get; set; }
        public bool IsPublic { get; set; }
        public int Position { get; set; }
        
        public decimal Sequence { get; set; }
        [NotMapped]
        public int CollegeID { get; set; }
        public string Colleges { get; set; }
        [NotMapped] 
        public string OldPath { get; set; }
    
        public bool IsNew { get; set; }
        public string? Color { get; set; }

    }
}
