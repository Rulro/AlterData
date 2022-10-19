using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace ControleDev.Models
{
    public class Developer
    {
        [Display(Name = "Data Criação")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage ="{0} obrigatório!")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0}: Tamanho do nome deve ter entre {2} e {1} caracteres")]
        [Display(Name = "Nome")]

        public string name { get; set; }

        public string Avatar { get; set; }

        public string Squad { get; set; }

        public string Login { get; set; }

        [Required(ErrorMessage = "{0} obrigatório!")]
        [EmailAddress(ErrorMessage ="Entre com um email válido!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Id")]
        //[StringLength(3, ErrorMessage = "O número só pode ter até {1} dígitos")]
        public int id { get; set; }

        public int Id { get; set; }

    }

    
}
