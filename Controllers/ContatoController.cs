using Contato.Filters;
using Contato.Helper;
using Contato.Models;
using Contato.Repository;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Contato.Controllers
{
    [PaginaUsuarioLogado]
    public class ContatoController : Controller
    {
        private readonly IContatoRepository _contatoRepository;
        private readonly ISessao _sessao;
        public ContatoController(IContatoRepository contatoRepository, ISessao sessao)
        {
            _contatoRepository = contatoRepository;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            
            List<ContatoModel> contatos = _contatoRepository.BuscarTodos();
            return View(contatos);
        }
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepository.ListarPorId(id);
            return View(contato);
        }
        public IActionResult Apagar(int id)
        {
            ContatoModel contato = _contatoRepository.ListarPorId(id);
            return View(contato);
        }

        public IActionResult Excluir(int id) 
        {
            try
            {
                bool apagado = _contatoRepository.Excluir(id);
                if(apagado) 
                {
                    TempData["MensagemSucesso"] = "Contato Apagado com Sucesso";
                }
                else 
                {
                    TempData["MensagemErro"] = "Não Conseguimos Apagar seu Contato";
                }
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Não Conseguimos Cadastrar seu Contato, Tente Novamente, Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscaSessaoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                    _contatoRepository.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato Cadastrado com Sucesso";
                    return RedirectToAction("Index");
                }
                return View(contato);
            }
            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Não Conseguimos Cadastrar seu Contato, Tente Novamente, Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscaSessaoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                    _contatoRepository.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato Alterado com Sucesso";
                    return RedirectToAction("Index");
                }
                return View("Editar", contato);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não Conseguimos Alterar seu Contato, Tente Novamente, Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }

    }
}
