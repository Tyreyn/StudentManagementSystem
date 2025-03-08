using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StudentManagementSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace StudentManagementSystem
{
    public partial class EditStudent : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string data = Request.QueryString["data"];

            if (!string.IsNullOrEmpty(data))
            {
                string decodedData = Server.UrlDecode(data);

                var studentDetails = JsonConvert.DeserializeObject<object[]>(decodedData);

                if (studentDetails.Length == 6)
                {
                    IList<Student> student = new List<Student>();

                    student.Add(new Student
                    {
                        StudentId = Guid.Parse(studentDetails[0].ToString()),
                        FirstName = studentDetails[1].ToString(),
                        LastName = studentDetails[2].ToString(),
                        Gender = studentDetails[3].ToString(),
                        DateOfBirth = DateTime.Parse(studentDetails[4].ToString()),
                        Age = Convert.ToInt32(studentDetails[5])
                    });

                    GridViewStudents.DataSource = student;
                    GridViewStudents.DataBind();
                }
                else
                {
                    Response.Write("Nieprawidłowe dane.");
                }
            }
            else
            {
                Response.Write("Brak danych.");
            }
        }
    }
}