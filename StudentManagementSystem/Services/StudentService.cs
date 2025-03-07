using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagementSystem.Services
{
    public class StudentService : IDisposable
    {
        private readonly StudentContext _context;

        public StudentService()
        {
            _context = new StudentContext();
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

        public List<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}