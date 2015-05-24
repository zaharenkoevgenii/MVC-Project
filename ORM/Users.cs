namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Users
    {
        public Users()
        {
            UsersInRoles = new HashSet<UsersInRoles>();
            Roles = new HashSet<Roles>();
            Files = new HashSet<Files>();
        }


        [Key]
        public Guid UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }


        public bool IsAnonymous { get; set; }
        public DateTime LastActivityDate { get; set; }
        public virtual ICollection<Files> Files { get; set; }

        public Guid ApplicationId { get; set; }
        public virtual Applications Applications { get; set; }
        public virtual Memberships Memberships { get; set; }
        public virtual Profiles Profiles { get; set; }

        public virtual ICollection<Roles> Roles { get; set; }
        public virtual ICollection<UsersInRoles> UsersInRoles { get; set; }
    }
}
