using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    using System.ComponentModel.DataAnnotations;

    public class Profiles
    {
        [Key,ForeignKey("User")]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public DateTime LastUpdateDate { get; set; }

        public virtual Users User { get; set; }
    }
}
