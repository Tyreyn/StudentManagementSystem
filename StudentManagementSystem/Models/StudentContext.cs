
using System.Data.Entity;

namespace StudentManagementSystem.Models
{
    public class StudentContext : DbContext
    {
        public StudentContext() : base("StudentContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<StudentContext>());
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            System.Diagnostics.Debug.WriteLine("[DatabaseContext] Creating model");

            modelBuilder.Entity<Student>()
                .HasKey(s => s.StudentId);

            base.OnModelCreating(modelBuilder);
        }
    }
}