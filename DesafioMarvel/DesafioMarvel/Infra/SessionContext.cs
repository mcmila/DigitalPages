using System.Web;

namespace DesafioMarvel.Infra
{
    public static class SessionContext
    {
        public static string PublicKey
        {
            get { return HttpContext.Current.Session["PublicKey"].ToString(); }
            set { HttpContext.Current.Session["PublicKey"] = value; }
        }

        public static string PrivateKey
        {
            get { return HttpContext.Current.Session["PrivateKey"].ToString(); }
            set { HttpContext.Current.Session["PrivateKey"] = value; }
        }
    }
}