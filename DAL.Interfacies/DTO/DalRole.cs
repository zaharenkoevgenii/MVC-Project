using System.Collections.Generic;

namespace DAL.Interfacies.DTO
{
    public class DalRole:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<DalUser> Users { get; set; }
    }
}
