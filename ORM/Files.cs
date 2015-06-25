namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Files
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool Private { get; set; }

        public int Rating { get; set; }

        public int UserId { get; set; }

        public DateTime CreationTime { get; set; }

        public byte[] File { get; set; }

        public virtual Users Users { get; set; }
    }
}
