using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public partial class Files
    {
        [Key]
        public Guid FileId { get; set; }
        [Required]
        [StringLength(50)]
        public string FileName { get; set; }
        public DateTime CreationTime { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
    }
}
