using System;

namespace DAL.Interface.DTO
{
    public class DalFile : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Private { get; set; }
        public int Rating { get; set; }
        public DateTime CreationTime { get; set; }
        public byte[] File { get; set; }
        public int UserRefId { get; set; }
    }
}
