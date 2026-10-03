using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RDATA.Entities
{
    public class AdminMaster
    {
        //public AdminMaster()
        //{
        //    Roles = new HashSet<Role>();
        //}

        [Key]
        public int AdminId { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public string? ProfilePic { get; set; }
        public string? VerificationUrl { get; set; }
        public int? BranchID { get; set; }
        public int? MailSentCounter { get; set; }
        public string? EncriptedKey { get; set; }

        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> Islive { get; set; }

        public DateTime CreatedDate { get; set; }

        public Nullable<bool> IsVerified { get; set; }
        public Nullable<DateTime> VerificationDate { get; set; }

        

        public virtual ICollection<Role>? Roles { get; set; }


        public virtual ICollection<AdminMasterRoles>? AdminMasterRoles { get; set; }
    }
}
