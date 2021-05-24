using Toyota.Common.Web.Platform;

namespace ProjectStarter.Controllers
{
    public class BlankController : PageController
    {
        protected override void Startup()
        {
            Settings.Title = "Blank";
        }

    }
}
