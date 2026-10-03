using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDATA.Entities
{
    public class Student
    {
        public Student()
        {
            Roles = new HashSet<Role>();
        }

        [Key]
        public Int64 StudId { get; set; }
        public string EnrollNo { get; set; }
        public string? RollNo { get; set; }        
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public DateTime? DOB { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public string? AddressLocation { get; set; }        
        public int? GenderId { get; set; }
        
        public int CourseID { get; set; }
        [NotMapped]
        public string? CandidateName { get; set; }

        public virtual ICollection<Role> Roles { get; set; }


        public virtual ICollection<StudentRoles> StudentRoles { get; set; }
    }
}