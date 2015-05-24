namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Roles
    {
        public Roles()
        {
            UsersInRoles = new HashSet<UsersInRoles>();
            Users = new HashSet<Users>();
        }

        [Key]
        public Guid RoleId { get; set; }
        [Required]
        [StringLength(256)]
        public string RoleName { get; set; }
        [StringLength(256)]
        public string Description { get; set; }


        public Guid ApplicationId { get; set; }
        public virtual Applications Applications { get; set; }
        public virtual ICollection<Users> Users { get; set; }
        public virtual ICollection<UsersInRoles> UsersInRoles { get; set; }
    }
}
