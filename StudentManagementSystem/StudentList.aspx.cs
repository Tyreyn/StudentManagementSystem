using StudentManagementSystem.Models;
using StudentManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using SimpleInjector;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;

namespace StudentManagementSystem
{
    public partial class _Default : Page
    {
        private static StudentService studentService;

        private IList<Student> studentList = new List<Student>();

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static void DeleteStudent(Guid id)
        {
            studentService.DeleteStudent(id);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            var scope = (Scope)HttpContext.Current.Items["ScopedContainer"];
            var container = scope.Container;
            studentService = container.GetInstance<StudentService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (studentList.Count == 0)
            {
                LoadStudents();
            }
        }

        protected void GridViewStudents_FocusedRowChanged(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
        }

        protected void GridViewStudents_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "Delete")
            {
                System.Diagnostics.Debug.WriteLine("chuj ją obchodzisz");
            }
            else if (e.ButtonID == "Edit")
            {
                System.Diagnostics.Debug.WriteLine("iks de");
            }

        }

        private void LoadStudents()
        {
            studentList = studentService.GetAllStudents();
            GridViewStudents.DataSource = studentList;
            GridViewStudents.DataBind();
        }
    }
}