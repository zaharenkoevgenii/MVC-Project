using System;

namespace BLL.Interface.Entities
{
    public class FileEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string OwnerId { get; set; }
    }
}