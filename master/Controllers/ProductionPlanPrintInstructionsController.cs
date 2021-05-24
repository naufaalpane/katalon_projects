using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using GPPSU.Commons.Controllers;
using GPPSU.Models.ProductionPlanPrintInstructions;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;

namespace GPPSU.Controllers
{
    public class ProductionPlanPrintInstructionsController : BaseController
    {
        public ProductionPlanPrintInstructionsRepository pRepo = ProductionPlanPrintInstructionsRepository.Instance;
        public DatabaseManager databaseManager = DatabaseManager.Instance;

        protected override void Startup()
        {
            Settings.Title = "WA0AU306 - Production Plan Print Instructions";
            ProductionPlanPrintInstructions data = new ProductionPlanPrintInstructions();
        }

        public ActionResult Search(ProductionPlanPrintInstructions data)
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

        public void DoSearch(ProductionPlanPrintInstructions data)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            IList<ProductionPlanPrintInstructions> ListData = pRepo.Search(db, data);
            ViewData["ListData"] = ListData;

        }

        #region Execute
        public JsonResult Print(ProductionPlanPrintInstructions data)
        {
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();
            GPPSU.Commons.Models.RepoResult repoResult = null;

            IDBContext db = databaseManager.GetContext();

            try
            {
                db.BeginTransaction();

                repoResult = pRepo.Print(data, GetLoginUserId());

                CopyPropertiesRepoToAjaxResult(repoResult, ajaxResult);

                if (GPPSU.Commons.Models.AjaxResult.VALUE_SUCCESS.Equals(ajaxResult.Result))
                {
                    db.CommitTransaction();
                }
                else
                {
                    db.AbortTransaction();
                }
            }
            catch (Exception e)
            {
                db.AbortTransaction();

                ajaxResult.Result = GPPSU.Commons.Models.AjaxResult.VALUE_ERROR;
                ajaxResult.ErrMesgs = new string[] {
                    string.Format("ERROR : {0}", e.Message)
                };
            }
            finally
            {
                db.Close();
            }

            return Json(ajaxResult);
        }
        #endregion
    }
}