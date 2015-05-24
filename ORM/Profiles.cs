namespace ORM
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Profiles
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        public string PropertyNames { get; set; }
        [Required]
        public string PropertyValueStrings { get; set; }
        [Required]
        public byte[] PropertyValueBinary { get; set; }
        public DateTime LastUpdatedDate { get; set; }


        public virtual Users Users { get; set; }
    }
}
