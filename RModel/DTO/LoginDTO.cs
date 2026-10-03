using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace RModel.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Please Enter Email")]
        [Display(Name="Email")]
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Mobile { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        public string? Password { get; set; }
        

        [Display(Name="Remember me")]
        public bool IsRemember { get; set; }
        public string? Enrollment { get; set; }
        public string? OTP { get; set; }
        public string? RollNo { get; set; }
        

    }

    public class ForgetPasswordDTO
    {
        [Required(ErrorMessage = "Please Enter Enrollment Number / First Year Registration Number")]
        [Display(Name = "Enrollment")]
        public string? Enrollment { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        public string? Name { get; set; }
        public string? OTP { get; set; }


    }
   
}