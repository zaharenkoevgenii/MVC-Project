using System;

namespace DAL.Interface.DTO
{
    public class DalFile : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string OwnerId { get; set; }
    }
}
