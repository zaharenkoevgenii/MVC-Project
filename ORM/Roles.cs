using System.Collections.Generic;

namespace ORM
{
    using System.ComponentModel.DataAnnotations;

    public class Roles
    {
        public Roles()
        {
            Users = new HashSet<Users>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
