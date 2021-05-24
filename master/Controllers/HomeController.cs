using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;
using System.Web.Mvc;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;
using Toyota.Common.Credential;
using Toyota.Common.Utilities;
using System.Web.UI;

namespace GPPSU.Controllers
{
    public class HomeController : PageController
    {
        public DatabaseManager databaseManager = DatabaseManager.Instance;
        protected override void Startup()
        {
            Settings.Title = "Dashboard";
            ViewData["ListFunction"] = null;

        }

        public ActionResult WidgetSettings()
        {
            return PartialView("_WidgetSettings");
        }
    }
}
