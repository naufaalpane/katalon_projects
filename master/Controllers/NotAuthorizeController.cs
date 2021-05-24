using Toyota.Common.Web.Platform;

namespace GPPSU.Controllers
{
    public class NotAuthorizeController : PageController
    {
        protected override void Startup()
        {
            //Settings.Title = "Sorry... You are not authorized<br> <br>Please, contact administrator!";
            Settings.Title = "Not Authorized";
        }

    }
}
