using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagementSystem.Services
{
    public class StudentService
    {
        private readonly StudentContext _context;

        public StudentService(StudentContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddStudent(string firstName, string lastName)
        {
            var student = new Student
            {
                StudentId = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Gender = "Unknown", // Domyślna wartość, bo nie podajesz w metodzie
                DateOfBirth = DateTime.Now, // Domyślna wartość
                IsStudent = true,
                Age = 0 // Domyślna wartość
            };
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void DeleteStudent(Guid studentId)
        {
            Student studentToDelete = new Student { StudentId = studentId };
            _context.Students.Attach(studentToDelete);
            _context.Students.Remove(studentToDelete);
            _context.SaveChanges();
        }

        public IList<Student> GetAllStudents()
        {
            return _context.Students.AsNoTracking().ToList();
        }

    }
}