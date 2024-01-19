using Contato.Models;

namespace Contato.Helper
{
    public interface ISessao
    {
        void CriarSessaoUsuario(UsuarioModel usuario);
        void RemoverSessaoUsuario();
        UsuarioModel BuscaSessaoUsuario();
    }
}
