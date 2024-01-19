using Contato.Data;
using Contato.Models;

namespace Contato.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BancoContext _bancoContext;  
        public UsuarioRepository(BancoContext bancoContext)
        {
            _bancoContext= bancoContext;
        }

        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
        }
        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios.ToList();
        }
        public UsuarioModel BuscaPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }
        public UsuarioModel BuscaPorEmailLogin(string email, string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == login.ToUpper());
        }
        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            usuario.SetSenhaHash();
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;


            //throw new NotImplementedException();
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = ListarPorId(usuario.Id);
            if (usuarioDB == null) 
            {
                throw new System.Exception("Erro na Atualização do Usuário");
            }

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Login = usuario.Login;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.DataAtualizacao = DateTime.Now;


            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDB = ListarPorId(alterarSenhaModel.Id);
            if(usuarioDB == null)
            {
                throw new System.Exception("Houve um Erro na Atualização da Senha, Usuário Não Encontrado");
            }
            if (!usuarioDB.SenhaValida(alterarSenhaModel.SenhaAtual)){
                throw new System.Exception("Senha Atual Não Confere");
            }
            if (usuarioDB.SenhaValida(alterarSenhaModel.NovaSenha))
            {
                throw new System.Exception("Nova Senha deve ser Diferente da Senha Atual");
            }

            usuarioDB.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDB.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDB) ;
            _bancoContext.SaveChanges();

            return usuarioDB;
        }

        public bool Excluir(int id)
        {
            UsuarioModel usuarioDB = ListarPorId(id);
            if (usuarioDB == null)
            {
                throw new System.Exception("Erro na Exclusão do Usuário");
            }

        _bancoContext.Usuarios.Remove(usuarioDB);
            _bancoContext.SaveChanges();
            return true;
        }


    }
}
