namespace DataAPI.Context
{
    public partial class UniOpetDbContext : System.Data.Entity.DbContext
    {
        public UniOpetDbContext() : base("UniOpetConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public static UniOpetDbContext Create()
        {
            return new UniOpetDbContext();
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("UniOpet");

            // Configure Asp Net Identity Tables
            modelBuilder.Entity<Models.User>().ToTable("Users");
            modelBuilder.Entity<Models.Role>().ToTable("Roles");
        }

        public System.Data.Entity.Infrastructure.DbQuery<T> Query<T>() where T : class
        {
            return Set<T>().AsNoTracking();
        }
    }
}