using System;

namespace MvcPL.Models
{
    public class FileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Private { get; set; }
        public int Rating { get; set; }
        public DateTime Created { get; set; }
        public int OwnerId { get; set; }
    }
}
