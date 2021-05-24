using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPPSU.Commons.Controllers;

namespace GPPSU.Controllers
{
    public class ProductionPlanCorrectionController : BaseController
    {
        protected override void Startup()
        {
            Settings.Title = "WA0AU304 - Production Plan correction";
        }

        public ActionResult download()
        {
            var MSGSave = "MCSTSTD006I" + ": " + "File download completed successfully";

            List<string> SIABEUL = new List<string>();
            SIABEUL.Add(MSGSave);
            return ShowInfoMessagesSuccess(SIABEUL);

        }

        public ActionResult ShowInfoMessagesSuccess(List<string> messages)
        {
            ViewData["Messages"] = messages;
            return PartialView("~/Views/Shared/_MessagePanel.cshtml");
        }
    }
}