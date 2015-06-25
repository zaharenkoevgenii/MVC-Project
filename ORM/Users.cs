namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        public Users()
        {
            Files = new HashSet<Files>();
            Roles = new HashSet<Roles>();
        }

        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime CreationTime { get; set; }

        public virtual ICollection<Files> Files { get; set; }

        public virtual Profiles Profiles { get; set; }

        public virtual ICollection<Roles> Roles { get; set; }
    }
}
