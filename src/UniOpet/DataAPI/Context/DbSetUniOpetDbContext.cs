namespace DataAPI.Context
{
    public partial class UniOpetDbContext
    {
        public System.Data.Entity.DbSet<Models.Course> Courses { get; set; }
        public System.Data.Entity.DbSet<Models.Grade> Grades { get; set; }
        public System.Data.Entity.DbSet<Models.Student> Students { get; set; }
        public System.Data.Entity.DbSet<Models.StudentAddress> StudentAddresses { get; set; }
        public System.Data.Entity.DbSet<Models.Teacher> Teachers { get; set; }

    }
}