namespace ORM
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Profiles
    {
        [Key, ForeignKey("Users")]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int Age { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public virtual Users Users { get; set; }
    }
}
