using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public class LogInViewModel
    {
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}