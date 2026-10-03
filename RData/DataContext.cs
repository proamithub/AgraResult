using Microsoft.EntityFrameworkCore;
using RDATA.Entities;
using System;

namespace RDATA
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AdminMaster> AdminMasters { get; set; }
        public DbSet<NewMasters> NewMasters { get; set; }
        public DbSet<AdminMasterRoles> AdminMasterRoles { get; set; }
         public DbSet<Student> Students { get; set; }
        
         public DbSet<Courses> Courses { get; set; }
        
         public DbSet<Colleges> Colleges { get; set; }
         public DbSet<FormToOpenLogs> FormToOpenLogs { get; set; }


        //////////////////////////////////////////////////
       
        //////////////////////////////////////////////////

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentRoles>().HasKey(e => new { e.StudId, e.RoleId });
            //modelBuilder.Entity<MemberRoles>()
            //            .HasOne(mr => mr.Member)
            //            .WithMany(m => m.MemberRoles)
            //            .HasForeignKey(mr => mr.MemberId);
            //modelBuilder.Entity<MemberRoles>()
            //            .HasOne(mr => mr.Role)
            //            .WithMany(r => r.MemberRoles)
            //            .HasForeignKey(mr => mr.RoleId);

            modelBuilder.Entity<AdminMasterRoles>().HasKey(e => new { e.AdminId, e.RoleId });
            modelBuilder.Entity<AdminMasterRoles>()
                        .HasOne(am => am.Admin)
                        .WithMany(m => m.AdminMasterRoles)
                        .HasForeignKey(mr => mr.AdminId);
            modelBuilder.Entity<AdminMasterRoles>()
                        .HasOne(mr => mr.Role)
                        .WithMany(r => r.AdminMasterRoles)
                        .HasForeignKey(mr => mr.RoleId);

        }
    }
}