namespace UI.Web.Models
{
    public class Student : BaseClass
    {
        public override string RouteUrl => "api/Courses";

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public byte[] Photo { get; set; }
        public decimal Height { get; set; }
        public float Weight { get; set; }

        public int GradeId { get; set; }
        public Grade Grade { get; set; }

        public StudentAddress Address { get; set; }

        public System.Collections.Generic.IList<StudentCourse> StudentCourses { get; set; }

    }
}
