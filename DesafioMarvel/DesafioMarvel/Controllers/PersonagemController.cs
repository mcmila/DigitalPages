using System.Threading.Tasks;
using System.Web.Mvc;
using CodeProject.MVC7Days.Filters;
using DesafioMarvel.Infra;
using Marvelous;

namespace DesafioMarvel.Controllers
{
    public class PersonagemController : Controller
    {
        [VerificaKeysFilter]
        public ActionResult Index(int? page)
        {
            if (!page.HasValue)
                page = 1;

            int offset = 10 * (page.Value - 1);

            MarvelClient marvel = new MarvelClient(SessionContext.PublicKey, SessionContext.PrivateKey);
            MarvelRoot<Character> personagens = marvel.Characters.FindAll(10, offset);

            int qtdPaginas = personagens.Data.Total / personagens.Data.Limit;

            if ((personagens.Data.Total % personagens.Data.Limit) > 0)
                qtdPaginas = qtdPaginas + 1;

            int pagAtual = page.Value;
            int pPaginaInicial, pPaginaFinal;

            Paginacao.ObterPaginacao(pagAtual, qtdPaginas, out pPaginaInicial, out pPaginaFinal);

            ViewBag.PagInicial = pPaginaInicial;
            ViewBag.PagFinal = pPaginaFinal;
            ViewBag.PagAtual = pagAtual;

            return View(personagens.Data.Results);
        }

        [VerificaKeysFilter]
        public async Task<ActionResult> Consultar(int id, int? page)
        {
            MarvelClient marvel = new MarvelClient(SessionContext.PublicKey, SessionContext.PrivateKey);
            MarvelRoot<Character> personagens = marvel.Characters.Find(id);

            ViewBag.Id = id;
            ViewBag.Name = personagens.Data.Results[0].Name;
            ViewBag.Descricao = personagens.Data.Results[0].Description;
            ViewBag.ImageUrl = personagens.Data.Results[0].Thumbnail.ToString(Image.PortraitSmall);

            if (!page.HasValue)
                page = 1;

            int offset = 10 * (page.Value - 1);

            MarvelRoot<Comic> comics = await marvel.CharactersComics.ComicsAsync(id, 10, offset);

            int qtdPaginas = comics.Data.Total / comics.Data.Limit;

            if ((comics.Data.Total % comics.Data.Limit) > 0)
                qtdPaginas = qtdPaginas + 1;

            int pagAtual = page.Value;
            int pPaginaInicial, pPaginaFinal;

            Paginacao.ObterPaginacao(pagAtual, qtdPaginas, out pPaginaInicial, out pPaginaFinal);

            ViewBag.PagInicial = pPaginaInicial;
            ViewBag.PagFinal = pPaginaFinal;
            ViewBag.PagAtual = pagAtual;

            return View(comics.Data.Results);
        }

        public new RedirectToRouteResult RedirectToAction(string action, string controller)
        {
            return base.RedirectToAction(action, controller);
        }
    }
}