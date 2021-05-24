using Toyota.Common.Web.Platform;

namespace GPPSU.Controllers
{
    public class LoginController : LoginPageController
    {
        protected override void Startup()
        {
            base.Startup();
            Settings.Title = "Login";
            string autolog = Request.Params["autologin"];
            string username = Request.Params["username"];
            string password = Request.Params["password"];
            ViewData["autologin"] = autolog;
            ViewData["username"] = username;
            ViewData["password"] = password;
        }

    }
}
