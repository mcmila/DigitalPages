using System;
using System.Web;
using System.Web.Mvc;
using DesafioMarvel.Controllers;
using DesafioMarvel.Infra;

namespace CodeProject.MVC7Days.Filters
{
    public class VerificaKeysFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (string.IsNullOrEmpty(SessionContext.PrivateKey) && string.IsNullOrEmpty(SessionContext.PublicKey))
            {
                var controller = (PersonagemController)filterContext.Controller;
                filterContext.Result = controller.RedirectToAction("Index", "Home");
            }
        }
    }
}