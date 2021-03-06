﻿using System;
using System.Collections.Generic;

namespace BLL.Interfacies.Entities
{
    public class UserEntity:IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationTime { get; set; }
        public ProfileEntity Profile { get; set; }
        public List<RoleEntity> Roles { get; set; }
        public List<FileEntity> Files { get; set; }
    }
}
