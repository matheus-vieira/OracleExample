namespace DataAPI.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Section { get; set; }

        public System.Collections.Generic.ICollection<Student> Students { get; set; }
    }
}