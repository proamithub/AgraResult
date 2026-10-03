using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RModel.DTO
{
    public class TemplateRules_AM
    {

        public int EntryID { get; set; }
        public string TName { get; set; }
        public string Rules { get; set; }

        public int RuleNo { get; set; }
        public bool IsActive { get; set; }
    }
}
