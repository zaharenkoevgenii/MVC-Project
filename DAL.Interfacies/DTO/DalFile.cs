using System;

namespace DAL.Interfacies.DTO
{
    public class DalFile : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public bool Private { get; set; }
        public bool Approved { get; set; }
        public int Rating { get; set; }
        public DateTime CreationTime { get; set; }
        public byte[] File { get; set; }

        public int UserId { get; set; }
        public DalUser User { get; set; }
    }
}
