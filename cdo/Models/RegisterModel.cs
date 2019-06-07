using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace cdo.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }



        public string Snils { get; set; }

        [Compare("Sogl", ErrorMessage = "Не подтверждено согласие на обработку персональных знаний")]
        public string Sogl { get; set; }

        [Compare("mo_s", ErrorMessage = "Не выбран регион")]
        public string mo_s { get; set; }
    }
}
