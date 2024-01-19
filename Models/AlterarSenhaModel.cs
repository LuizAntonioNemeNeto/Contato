using System.ComponentModel.DataAnnotations;

namespace Contato.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite a atual Senha")]
        public string SenhaAtual { get; set; }
        [Required(ErrorMessage = "Digite a nova Senha")]
        public string NovaSenha { get; set; }
        [Required(ErrorMessage = "Confirme a nova Senha")]
        [Compare("NovaSenha", ErrorMessage = "Senha não Confere")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
