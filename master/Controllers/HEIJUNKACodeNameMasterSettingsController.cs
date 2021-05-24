using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPPSU.Commons.Controllers;


using GPPSU.Models.HEIJUNKACodeNameMasterSettings;
using Toyota.Common;
using Toyota.Common.Credential;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;

namespace GPPSU.Controllers { 
public class HEIJUNKACodeNameMasterSettingsController : BaseController
    {
    public HEIJUNKACodeNameMasterSettingsRepo oRepo = HEIJUNKACodeNameMasterSettingsRepo.Instance;
    public DatabaseManager databaseManager = DatabaseManager.Instance;

    protected override void Startup()
        {
            Settings.Title = "WA0AUC11 - HEIJUNKA Code Name Master Settings";
            HEIJUNKACodeNameMasterSettings data = new HEIJUNKACodeNameMasterSettings();


            DoSearch(data, 1, 10);
        }

        public ActionResult Search(HEIJUNKACodeNameMasterSettings data, int currentPage, int rowsPerPage)
        {
            try
            {
                DoSearch(data, currentPage, rowsPerPage);


            }
            catch (Exception e)
            {
                return Json("Error: " + e.Message, JsonRequestBehavior.AllowGet);
            }

            return PartialView("_GridView");
        }

        public void DoSearch(HEIJUNKACodeNameMasterSettings data, int currentPage, int rowsPerPage)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            if (currentPage == 0 && rowsPerPage == 0)
            {
                ViewData["HEIJUNKAData"] = null;
                ViewData["Paging"] = null;

            }
            else
            {
                Models.Common.PagingModel paging = new Models.Common.PagingModel(oRepo.SearchCount(db, data), currentPage, rowsPerPage);
                ViewData["paging"] = paging;

                IList<HEIJUNKACodeNameMasterSettings> HEIJUNKAData = oRepo.Search(db, data, paging.StartData, paging.EndData);
                ViewData["HEIJUNKAData"] = HEIJUNKAData;
            }
        }

        public ActionResult ShowInfoMessagesSuccess(List<string> messages)
        {
            ViewData["Messages"] = messages;
            return PartialView("~/Views/Shared/_MessagePanel.cshtml");
        }

        #region SAVEADDEDIT
        public JsonResult AddEditSave(string screenMode, HEIJUNKACodeNameMasterSettings data)
        {
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();
            GPPSU.Commons.Models.RepoResult repoResult = null;
            IDBContext db = databaseManager.GetContext();

            Toyota.Common.Credential.User u = Lookup.Get<Toyota.Common.Credential.User>();
            string userId = u.Username;
            string companyCode = u.Company.Id;

            try
            {
                db.BeginTransaction();
                if (screenMode.Equals("Add"))
                {
                    repoResult = oRepo.SaveAddEdit(data, userId,companyCode, screenMode);
                }
                else
                {
                    repoResult = oRepo.SaveAddEdit(data, userId,companyCode, screenMode);
                }
             

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
                    string.Format("{0} = {1}", e.GetType().FullName, e.Message)
                };
            }
            finally
            {
                db.Close();
            }

            return Json(ajaxResult);
        }
        #endregion

        #region delete
        public JsonResult Delete(HEIJUNKACodeNameMasterSettings data)
        {
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();
            GPPSU.Commons.Models.RepoResult repoResult = null;

            IDBContext db = databaseManager.GetContext();

            try
            {
                db.BeginTransaction();

                repoResult = oRepo.Delete(db, data);

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