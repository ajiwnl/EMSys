using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Reflection.Metadata;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<SubjectSched> SSFSched { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Credentials> Credentials { get; set; }
        public DbSet<EnrollmentDetails> EnrollmentDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>()
                .HasKey(s => new { s.SUBJCODE, s.SUBJCOURSECODE, s.SUBJCATEGORY, s.SUBJCODEPREQ });
            modelBuilder.Entity<EnrollmentDetails>()
                .HasKey(s => new { s.ENRDFSTUDID, s.ENRDFSTUDEDPCODE });
            modelBuilder.Entity<Credentials>()
            .Property(c => c.UserName)
            .UseCollation("SQL_Latin1_General_CP1_CS_AS");
        }
    }
}
