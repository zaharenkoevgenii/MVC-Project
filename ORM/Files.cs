using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    public class Files
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool Private { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public DateTime CrationTime { get; set; }

        public int UserRefId { get; set; }
        [ForeignKey("UserRefId")]
        public virtual Users User { get; set; }
    }
}
