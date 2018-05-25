namespace UI.Web.Models
{
    public class Grade : BaseClass
    {
        public override string RouteUrl => "api/Grades";

        public string GradeName { get; set; }
        public string Section { get; set; }

        public System.Collections.Generic.ICollection<Student> Students { get; set; }
    }

}
