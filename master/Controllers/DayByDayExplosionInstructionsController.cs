using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPPSU.Commons.Controllers;
using GPPSU.Models.DayByDayExplosionInstructions;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;

namespace GPPSU.Controllers
{
    public class DayByDayExplosionInstructionsController : BaseController
    {
        public DayByDayExplosionInstructionsRepo oRepo = DayByDayExplosionInstructionsRepo.Instance;
        public DatabaseManager databaseManager = DatabaseManager.Instance;

        protected override void Startup()
        {
            Settings.Title = "WA0AUB09 -Day By Day Explosion Instructions";
            DayByDayExplosionInstructions data = new DayByDayExplosionInstructions();


        }

        public ActionResult SearchKeihen(DayByDayExplosionInstructions data)
        {
            try
            {
                DoSearchKeihen(data);


            }
            catch (Exception e)
            {
                return Json("Error: " + e.Message, JsonRequestBehavior.AllowGet);
            }

            return PartialView("_TableKeihen");
        }

        public void DoSearchKeihen(DayByDayExplosionInstructions data)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
                IList<DayByDayExplosionInstructions> ListData = oRepo.SearchKeihen(db, data);
                ViewData["ListDataKeihen"] = ListData;
           
        }

        public ActionResult Search(DayByDayExplosionInstructions data)
        {
            try
            {
                DoSearch(data);


            }
            catch (Exception e)
            {
                return Json("Error: " + e.Message, JsonRequestBehavior.AllowGet);
            }

            return PartialView("_TableMonthly");
        }

        public void DoSearch(DayByDayExplosionInstructions data)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            IList<DayByDayExplosionInstructions> ListData = oRepo.Search(db, data);
            ViewData["ListDataMonthly"] = ListData;

        }

        public ActionResult ShowInfoMessagesSuccess(List<string> messages)
        {
            ViewData["Messages"] = messages;
            return PartialView("~/Views/Shared/_MessagePanel.cshtml");
        }
        #region Execute
        public JsonResult Execute(DayByDayExplosionInstructions data)
        {
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();
            GPPSU.Commons.Models.RepoResult repoResult = null;

            IDBContext db = databaseManager.GetContext();

            try
            {
                db.BeginTransaction();

                repoResult = oRepo.Execute(data, GetLoginUserId());

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