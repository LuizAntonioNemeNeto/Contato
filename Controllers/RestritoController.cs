using Contato.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Contato.Controllers
{
    [PaginaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
