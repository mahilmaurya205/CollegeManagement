using CollegeManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CollegeManagement.Models
{
    public class CollegeManagementDBContext : DbContext
    {
        private readonly ILogger<CollegeManagementDBContext> _logger;

        public CollegeManagementDBContext(DbContextOptions<CollegeManagementDBContext> options, ILogger<CollegeManagementDBContext> logger)
            : base(options)
        {
            _logger = logger;
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving changes to the database.");
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Login>()
                .HasKey(l => l.LoginID);

            modelBuilder.Entity<Login>()
                .HasOne(l => l.StudentSignUp)
                .WithMany()
                .HasForeignKey(l => l.StudentID);

            modelBuilder.Entity<StudentSignUp>()
                .ToTable("StudentSignUps")  // Make sure to use the correct table name
                .HasKey(s => s.SID);  // SID as the primary key

            modelBuilder.Entity<TeacherSignUp>()
                .HasKey(t => t.TeacherID);

            modelBuilder.Entity<AccessKey>()
                .HasKey(k => k.KeyID);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<StudentSignUp> StudentSignUps { get; set; }
        public DbSet<TeacherSignUp> TeacherSignUps { get; set; }
        public DbSet<AccessKey> AccessKeys { get; set; }
    }
}
