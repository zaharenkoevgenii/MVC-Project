using System;

namespace BLL.Interface.Entities
{
    public class FileEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public Guid OwnerId { get; set; }
    }
}