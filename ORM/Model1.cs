namespace ORM
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EntityModel : DbContext
    {
        public EntityModel()
            : base("name=EntityModel")
        {
            
        }

        public virtual DbSet<Applications> Applications { get; set; }
        public virtual DbSet<Memberships> Memberships { get; set; }
        public virtual DbSet<Profiles> Profiles { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersInRoles> UsersInRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applications>()
                .HasMany(e => e.Memberships)
                .WithRequired(e => e.Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Applications>()
                .HasMany(e => e.Roles)
                .WithRequired(e => e.Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Applications>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Roles>()
                           .HasMany(e => e.UsersInRoles)
                           .WithRequired(e => e.Roles)
                           .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<Users>()
                .HasOptional(e => e.Memberships)
                .WithRequired(e => e.Users);

            modelBuilder.Entity<Users>()
                .HasOptional(e => e.Profiles)
                .WithRequired(e => e.Users);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.UsersInRoles)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId);

        }
    }
}
