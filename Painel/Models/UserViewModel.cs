using System.ComponentModel.DataAnnotations;

namespace Painel.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Usuario é obrigatorio")]
        [DataType(DataType.Text)]
        [Display(Name = "Usuario")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Senha é obrigatoria")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
    }
}