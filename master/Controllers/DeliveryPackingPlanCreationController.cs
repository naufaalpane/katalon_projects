using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Xml;
using GPPSU.Models.DeliveryPackingPlanCreation;
using GPPSU.Models.Common;

using GPPSU.Commons.Controllers;
using Toyota.Common.Credential;

using GPPSU.Commons.Models;

using TDKUtility;
using GPPSU.Commons.Constants;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Globalization;

namespace GPPSU.Controllers
{
    public class DeliveryPackingPlanCreationController : BaseController
    {

        public DeliveryPackingPlanCreationRepository DPPC_repo = DeliveryPackingPlanCreationRepository.Instance;
        public DatabaseManager databaseManager = DatabaseManager.Instance;

        protected override void Startup()
        {
            Settings.Title = "WA0AUB03 - Delivery/Packing Plan Creation instructions";
        }

        #region Search & Count
        public ActionResult Search(DeliveryPackingPlanCreation data)
        {
            try
            {
                DoSearch(data);
            }
            catch (Exception e)
            {
                return Json("Error: " + e.Message, JsonRequestBehavior.AllowGet);
            }

            return PartialView("_GridView");
        }

        public void DoSearch(DeliveryPackingPlanCreation data)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();

          

            IList<DeliveryPackingPlanCreation> DPPCData = DPPC_repo.Search(db, data);
            ViewData["DPPCData"] = DPPCData;
        }

        #endregion
    }
}