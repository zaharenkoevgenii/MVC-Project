using System;

namespace DAL.Interface.DTO
{
    public class DalFile : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public Guid OwnerId { get; set; }
    }
}
