namespace UI.Web.Models
{
    public class StudentAddress : BaseClass
    {
        public override string RouteUrl => "api/Courses";

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }

}