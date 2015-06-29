namespace ORM
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public  class Roles
    {
        public Roles()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
