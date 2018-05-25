namespace DataAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.ConcurrencyCheck]
        public string Name { get; set; }
        public System.DateTime? DateOfBirth { get; set; }
        public byte[] Photo { get; set; }
        public decimal Height { get; set; }
        public float Weight { get; set; }

        public int GradeId { get; set; }
        public Grade Grade { get; set; }

        
        public virtual StudentAddress Address { get; set; }

        public virtual System.Collections.Generic.ICollection<Course> Courses { get; set; }
    }
}