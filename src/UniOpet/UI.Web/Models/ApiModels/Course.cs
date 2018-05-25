namespace UI.Web.Models.ApiModels
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("OnlineTeacher")]
        public int? OnlineTeacherId { get; set; }
        public Teacher OnlineTeacher { get; set; }


        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ClassRoomTeacher")]
        public int? ClassRoomTeacherId { get; set; }
        public Teacher ClassRoomTeacher { get; set; }

        public virtual System.Collections.Generic.ICollection<Student> Students { get; set; }
    }
}