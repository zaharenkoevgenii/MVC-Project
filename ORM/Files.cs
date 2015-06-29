using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Files
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ContentType { get; set; }

        public bool Private { get; set; }

        public bool Approved { get; set; }

        public int Rating { get; set; }

        public DateTime CreationTime { get; set; }

        public byte[] File { get; set; }



        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual Users User { get; set; }
    }
}
