using System;

namespace DAL.Interface.DTO
{
    public class DalUser : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}