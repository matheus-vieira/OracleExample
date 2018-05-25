namespace UI.Web.Models
{
    public class Course : BaseClass
    {
        public override string RouteUrl => "api/Courses";

        public string CourseName { get; set; }
        public System.Collections.Generic.IList<StudentCourse> StudentCourses { get; set; }
    }

}
