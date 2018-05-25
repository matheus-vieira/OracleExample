namespace UI.Web.Models
{
    public class StudentCourse : BaseClass
    {
        public override string RouteUrl => "api/Courses";

        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}