using Contato.Enums;
using System.ComponentModel.DataAnnotations;

namespace Contato.Models
{
    public class UsuarioSemSenhaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o Nome do Usuário")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o Login do Usuário")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite o Email do Usuário")]
        [EmailAddress(ErrorMessage = "O Email Informado não é Válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe o Perfil do Usuário")]
        public PerfilEnum? Perfil { get; set; }
    }
}
