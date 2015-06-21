namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Users
    {
        public Users()
        {
            Files = new List<Files>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }

        public virtual Profiles Profile { get; set; }
        public virtual ICollection<Files> Files { get; set; }
        public virtual ICollection<Roles> Roles { get; set; }
    }
}
