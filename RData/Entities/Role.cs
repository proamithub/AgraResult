using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RDATA.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Student> Student { get; set; }
        public virtual ICollection<AdminMaster> AdminMaster { get; set; }

        [NotMapped]
        public int AdminId { get; set; }
        // public virtual ICollection<MemberRoles> MemberRoles { get; set; }

        public virtual ICollection<AdminMasterRoles> AdminMasterRoles { get; set; }
    }
}
