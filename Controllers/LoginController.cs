using Contato.Helper;
using Contato.Models;
using Contato.Repository;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Contato.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISessao _sessao;
        private readonly IEmail _email;
        public LoginController(IUsuarioRepository usuarioRepository, ISessao sessao, IEmail email)
        {
            _usuarioRepository = usuarioRepository;
            _sessao = sessao;
            _email = email;
        }
        public IActionResult Index()
        {
            //se o usuário estiver logado redirecionar para home
            if (_sessao.BuscaSessaoUsuario() != null){
            return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult RedefinirSenha() {
            return View();
        }
        public IActionResult Sair() 
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index","Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel){
            try
            {
                if (ModelState.IsValid) 
                {
                    UsuarioModel usuario = _usuarioRepository.BuscaPorLogin(loginModel.Login);

                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = $"Senha Inválida, Tente Novamente";
                    }
                    TempData["MensagemErro"] = $"Usuário e/ou Senha Inválidos, Tente Novamente";
                }
                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não Conseguimos Realizar seu Login, Tente Novamente, Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult EnviarLinkRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepository.BuscaPorEmailLogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        
                        string mensagem = $"Sua Nova Senha é: {novaSenha}";

                        bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de Contatos - Nova Senha", mensagem);
                        if(emailEnviado) {
                            _usuarioRepository.Atualizar(usuario);
                            TempData["MensagemSucesso"] = $"Enviamos para Seu Email Cadastrado uma nova Senha";
                        }
                        else {
                            TempData["MensagemErro"] = $"Não Conseguimos Enviar o Email, Tente Novamente";
                        }
                        return RedirectToAction("Index", "Login");
                    }
                    TempData["MensagemErro"] = $"Não Conseguimos Redefinir sua Senha, Verificar Dados Informados";
                }
                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não Conseguimos Redefinir sua Senha, Tente Novamente, Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
