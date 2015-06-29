using System.Collections.Generic;
using BLL.Interface.Entities;
using BLL.Interfacies.Entities;

namespace MvcPL.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<RoleEntity> Roles { get; set; }
        public List<FileEntity> Files { get; set; }
        public ProfileEntity Profile { get; set; }
    }
}