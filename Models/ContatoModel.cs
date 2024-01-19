using System.ComponentModel.DataAnnotations;

namespace Contato.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o Nome do Contato")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o Email do Contato")]
        [EmailAddress(ErrorMessage = "O Email Informado não é Válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite o Celular do Contato")]
        [Phone(ErrorMessage = "O Número Informado não é Válido")]
        public string Celular { get; set; }

        public int? UsuarioId { get; set; }

        public UsuarioModel? Usuario { get; set; }
    }
}
