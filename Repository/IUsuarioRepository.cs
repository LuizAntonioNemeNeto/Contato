using Contato.Models;

namespace Contato.Repository
{
    public interface IUsuarioRepository
    {
        UsuarioModel BuscaPorLogin(string login);
        UsuarioModel BuscaPorEmailLogin(string email, string login);
        UsuarioModel ListarPorId(int id);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel Adicionar(UsuarioModel usuario);
        UsuarioModel Atualizar(UsuarioModel usuario);
        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel);
        bool Excluir(int id);
    }
}
