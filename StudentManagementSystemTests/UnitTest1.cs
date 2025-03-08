using Moq;
using NUnit.Framework;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
#pragma warning disable NUnit1032

namespace StudentManagementSystem.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        private Mock<DbSet<Student>> _mockStudents;
        private Mock<StudentContext> _mockContext;
        private StudentService _service;

        [SetUp]
        public void Init()
        {
            _mockContext = new Mock<StudentContext>();

            _mockStudents = new Mock<DbSet<Student>>();

            var data = GetStudents();
            var queryableData = data.AsQueryable();

            _mockStudents.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            _mockStudents.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            _mockStudents.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            _mockStudents.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());

            _mockStudents.Setup(m => m.Add(It.IsAny<Student>())).Callback<Student>(student => data.Add(student));

            _mockContext.Setup(m => m.Set<Student>()).Returns(_mockStudents.Object);

            _mockContext.Setup(m => m.SaveChanges()).Returns(1);

            _service = new StudentService();
            var contextField = typeof(StudentService).GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
            contextField.SetValue(_service, _mockContext.Object);
        }

        [Test]
        public void GetAllStudents_ShouldReturnInitialStudents()
        {
            // Act
            var students = _service.GetAllStudents();

            // Assert
            Assert.That(students.Count().Equals(10));
            Assert.That(students.Any(s => s.FirstName == "Anna" && s.LastName == "Kowalska") == true);
            Assert.That(students.Any(s => s.FirstName == "Piotr" && s.LastName == "Nowak") == true);
        }

        [Test]
        public void AddStudent_ShouldIncreaseStudentCount()
        {
            // Arrange
            int initialCount = _service.GetAllStudents().Count();

            // Act
            _service.AddStudent("Jan", "Kowalski");

            // Assert
            int newCount = _service.GetAllStudents().Count();
            Assert.That(initialCount + 1 == newCount);
        }

        [Test]
        public void AddStudent_ShouldAddCorrectStudent()
        {
            // Act
            _service.AddStudent("Ewa", "Test");

            // Assert
            var students = _service.GetAllStudents();
            Assert.That(students.Any(s => s.FirstName == "Ewa" && s.LastName == "Test") == true);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }


        private static List<Student> GetStudents()
        {
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
