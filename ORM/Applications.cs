namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Applications
    {
        public Applications()
        {
            Memberships = new HashSet<Memberships>();
            Roles = new HashSet<Roles>();
            Users = new HashSet<Users>();
        }

        [Key]
        public Guid ApplicationId { get; set; }

        [Required]
        [StringLength(50)]
        public string ApplicationName { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public virtual ICollection<Memberships> Memberships { get; set; }

        public virtual ICollection<Roles> Roles { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
