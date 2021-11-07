using System.ComponentModel.DataAnnotations;

namespace Painel.Entities
{
    public class Person
    {
        [Key]
        public int Code { get; set; }

        public string Name { get; set; }
        public string Document { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}