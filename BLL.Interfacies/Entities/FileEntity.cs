using System;
using BLL.Interfacies.Entities;

namespace BLL.Interface.Entities
{
    public class FileEntity:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public int Rating { get; set; }
        public bool Private { get; set; }
        public bool Approved { get; set; }
        public DateTime CreationTime { get; set; }
        public byte[] File { get; set; }

        public int UserId { get; set; }
    }
}