using Microsoft.AspNetCore.Http;
using RDATA.Entities;
using RModel.CustomAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RModel.DTO
{
    public class AdminMasterDTO
    {
        public int AdminId { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        [Display(Name = "Full Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Password")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
        public string NewPassword { get; set; }
        [Required(ErrorMessage = " ")]
        [Display(Name = "Confirm Password")]
        [StringLength(50)]
        [Compare("Password", ErrorMessage = "Confirm Password does not matched")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter your email id")]
        [Display(Name = "Email")]
        [StringLength(50)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your mobile number")]
        [Display(Name = "Mobile Number")]
        [StringLength(20)]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$", ErrorMessage = "Please enter a valid contact number")]
        public string MobileNo { get; set; }

        public string ProfilePic { get; set; }
        public string ProfilePicDomain { get; set; }
        

        [Display(Name = "Profile Picture")]
        [ValidatePhoto]
        public IFormFile File { get; set; }

        public int? MailSentCounter { get; set; }

        public string EncriptedKey { get; set; }

        public string VerificationUrl { get; set; }

        public Nullable<bool> IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }

        public Nullable<bool> IsVerified { get; set; }
        public Nullable<DateTime> VerificationDate { get; set; }
        public bool Islive { get; set; }

        public string[] Roles { get; set; }
        public string DisplayRoles { get; set; }
        public int RoleId { get; set; }
        public int? BranchID { get; set; }
    }


    public class AdminMasterRolesDTO
    {
        public int AdminId { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
      
    }
    }
