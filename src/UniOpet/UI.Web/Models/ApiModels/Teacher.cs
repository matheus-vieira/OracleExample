namespace UI.Web.Models.ApiModels
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.InverseProperty("OnlineTeacher")]
        public System.Collections.Generic.ICollection<Course> OnlineCourses { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.InverseProperty("ClassRoomTeacher")]
        public System.Collections.Generic.ICollection<Course> ClassRoomCourses { get; set; }
    }
}