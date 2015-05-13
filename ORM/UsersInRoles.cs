using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
        public partial class UsersInRoles
        {
            [Key]
            [Column(Order = 0)]
            public Guid UserId { get; set; }

            [Key]
            [Column(Order = 1)]
            public Guid RoleId { get; set; }

            public virtual Roles Roles { get; set; }

            public virtual Users Users { get; set; }

        }
    }
