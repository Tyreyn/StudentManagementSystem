using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentManagementSystem.Models
{
    public class StudentDatabaseInitializer : DropCreateDatabaseIfModelChanges<StudentContext>
    {
        protected override void Seed(StudentContext context)
        {
            GetStudents().ForEach(c => context.Students.Add(c));
        }

        private static List<Student> GetStudents()
        {
            System.Diagnostics.Debug.WriteLine("[DatabaseInitializer] Get students");

            return new List<Student>
        {
            new Student { StudentId = Guid.NewGuid(), FirstName = "Anna", LastName = "Kowalska", Gender = "Female", DateOfBirth = new DateTime(2003, 5, 12), IsStudent = true, Age = 21 },
            new Student { StudentId = Guid.NewGuid(), FirstName = "Piotr", LastName = "Nowak", Gender = "Male", DateOfBirth = new DateTime(2002, 7, 20), IsStudent = true, Age = 22 },
            new Student { StudentId = Guid.NewGuid(), FirstName = "Katarzyna", LastName = "Wiśniewska", Gender = "Female", DateOfBirth = new DateTime(2004, 2, 15), IsStudent = true, Age = 20 },
            new Student { StudentId = Guid.NewGuid(), FirstName = "Michał", LastName = "Lewandowski", Gender = "Male", DateOfBirth = new DateTime(2001, 11, 5), IsStudent = true, Age = 23 },
            new Student { StudentId = Guid.NewGuid(), FirstName = "Magdalena", LastName = "Dąbrowska", Gender = "Female", DateOfBirth = new DateTime(2003, 1, 30), IsStudent = true, Age = 21 },
            new Student { StudentId = Guid.NewGuid(), FirstName = "Tomasz", LastName = "Wójcik", Gender = "Male", DateOfBirth = new DateTime(2002, 9, 10), IsStudent = true, Age = 22 },
            new Student { StudentId = Guid.NewGuid(), FirstName = "Ewa", LastName = "Kaczmarek", Gender = "Female", DateOfBirth = new DateTime(2004, 4, 22), IsStudent = true, Age = 20 },
            new Student { StudentId = Guid.NewGuid(), FirstName = "Jakub", LastName = "Mazur", Gender = "Male", DateOfBirth = new DateTime(2003, 8, 18), IsStudent = true, Age = 21 },
            new Student { StudentId = Guid.NewGuid(), FirstName = "Natalia", LastName = "Krawczyk", Gender = "Female", DateOfBirth = new DateTime(2005, 3, 3), IsStudent = true, Age = 19 },
            new Student { StudentId = Guid.NewGuid(), FirstName = "Marcin", LastName = "Zając", Gender = "Male", DateOfBirth = new DateTime(2002, 12, 8), IsStudent = true, Age = 22 }
        };
        }
    }
}