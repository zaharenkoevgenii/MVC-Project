using System;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public int Age { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
