using Painel.Entities;
using System.ComponentModel.DataAnnotations;

namespace Painel.Models
{
    public class PersonViewModel
    {
        [Required(ErrorMessage = "Codigo é obrigatório")]
        [DataType(DataType.Text)]
        [Display(Name = "Código")]
        public int Code { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [DataType(DataType.Text)]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        [DataType(DataType.Text)]
        [RegularExpression("^\\d{3}\\.\\d{3}\\.\\d{3}\\-\\d{2}$", ErrorMessage = "CPF Inválido ex: 111.111.111-11")]
        [Display(Name = "CPF")]
        public string Document { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Endereço")]
        public string Address { get; set; }

        [DataType(DataType.Text)]
        [RegularExpression("^\\(?[1-9]{2}\\)? ?(?:[2-8]|9[1-9])[0-9]{3}\\-?[0-9]{4}$", ErrorMessage = "Telefone Inválido ex: (xx) xxxxx-xxxx")]
        [Display(Name = "Telefone")]
        public string PhoneNumber { get; set; }

        public static explicit operator PersonViewModel(Person person)
        {
            return new()
            {
                Code = person.Code,
                Name = person.Name,
                Document = person.Document,
                Address = person.Address,
                PhoneNumber = person.PhoneNumber,
            };
        }
    }
}