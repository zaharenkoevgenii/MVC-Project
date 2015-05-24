using System;

namespace MvcPL.Models
{
    public class FileViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string OwnerId { get; set; }
    }
}
