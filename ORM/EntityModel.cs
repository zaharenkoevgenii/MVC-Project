namespace ORM
{
    using System.Data.Entity;

    public class EntityModel : DbContext
    {
        public EntityModel(): base("name=EntityModel"){}

        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Profiles> Profiles { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder){}
    }
}
