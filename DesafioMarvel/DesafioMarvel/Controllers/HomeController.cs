using System.Web.Mvc;
using DesafioMarvel.Infra;
using DesafioMarvel.Models;

namespace DesafioMarvel.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Keys keys)
        {
            if (ModelState.IsValid)
            {
                SessionContext.PrivateKey = keys.PrivateKey;
                SessionContext.PublicKey = keys.PublicKey;

                return RedirectToAction("Index", "Personagem");
            }

            return View();
        }
    }
}