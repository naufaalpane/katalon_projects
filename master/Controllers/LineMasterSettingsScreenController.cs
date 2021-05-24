using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Xml;
using GPPSU.Models.LineMasterSettingsScreen;
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
    public class LineMasterSettingsScreenController : BaseController
    {
        //LineMasterSettingsScreen
        public LineMasterSettingsScreenRepository line_repo = LineMasterSettingsScreenRepository.Instance;
        public DatabaseManager databaseManager = DatabaseManager.Instance;

        public const string downloadFileName = "TemplateLineMaster.xls";

        public const int DATA_ROW_INDEX_START = 4;

        protected override void Startup()
        {
            Settings.Title = "WA0AUC08 - Line Master Settings Screen";
            LineMasterSettingsScreen lineMM = new LineMasterSettingsScreen();
            DoSearch(lineMM, 1, 10);
        }

        #region Search & Count
        public ActionResult Search(LineMasterSettingsScreen data, int currentPage, int rowsPerPage)
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

        public void DoSearch(LineMasterSettingsScreen data, int currentPage, int rowsPerPage)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();

            GPPSU.Models.Common.PagingModel paging = new GPPSU.Models.Common.PagingModel(line_repo.SearchCount(db, data), currentPage, rowsPerPage);
            ViewData["paging"] = paging;

            IList<LineMasterSettingsScreen> lineMMData = line_repo.Search(db, data, paging.StartData, paging.EndData);
            ViewData["lineMMData"] = lineMMData;
        }

        #endregion

        #region Add/Edit & GetByKey
        public JsonResult SaveAddEdit(string screenMode, LineMasterSettingsScreen data)
        {
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();
            //ICS.Commons.Models.RepoResult repoResult = null;

            GPPSU.Commons.Models.RepoResult repoResult = null;

            IDBContext db = databaseManager.GetContext();

            string noreg = Lookup.Get<User>().RegistrationNumber;

            try
            {
                db.BeginTransaction();
                if (screenMode == "Upload")
                {
                    int i = 0;
                    foreach (LineMasterSettingsScreen x in data.listData)
                    {
                        repoResult = line_repo.SaveAddEdit(x, "User WOT", screenMode);
                        if (Commons.Models.AjaxResult.VALUE_SUCCESS.Equals(repoResult.Result))
                        {
                            db.CommitTransaction();
                        }
                        else
                        {
                            if (ajaxResult.ErrMesgs == null) ajaxResult.ErrMesgs = new string[data.listData.Count];
                            ajaxResult.ErrMesgs[i] = repoResult.ErrMesgs[0];
                            db.AbortTransaction();
                        }
                        i++;
                    }
                }
                else
                {
                    repoResult = line_repo.SaveAddEdit(data, "User WOT", screenMode);
                }

                if (!screenMode.Equals("Upload"))
                {
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

        public JsonResult GetByKey(LineMasterSettingsScreen data)
        {
            LineMasterSettingsScreen result = null;
            IDBContext db = DatabaseManager.Instance.GetContext();
            result = line_repo.GetByKey(db, data);
            db.Close();
            return Json(result);
        }

        #endregion

        #region Delete

        public JsonResult Delete(LineMasterSettingsScreen data)
        {
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();
            GPPSU.Commons.Models.RepoResult repoResult = null;

            IDBContext db = databaseManager.GetContext();

            try
            {
                db.BeginTransaction();

                repoResult = line_repo.Delete(db, data);

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

        public ActionResult download()
        {
            var MSGSave = "MCSTSTD006I" + ": " + "File download completed successfully";

            List<string> SIABEUL = new List<string>();
            SIABEUL.Add(MSGSave);
            return ShowInfoMessagesSuccess(SIABEUL);

        }

        public ActionResult errorDownload()
        {
            var MSGSave = "ERROR : No Data Found.";

            List<string> SIABEUL = new List<string>();
            SIABEUL.Add(MSGSave);
            return ShowInfoMessagesSuccess(SIABEUL);

        }


        public ActionResult save()
        {
            var MSGSave = "I: Data updated successfully";

            List<string> SIABEUL = new List<string>();
            SIABEUL.Add(MSGSave);
            return ShowInfoMessagesSuccess(SIABEUL);

        }

        public ActionResult ShowInfoMessagesSuccess(List<string> messages)
        {
            ViewData["Messages"] = messages;
            return PartialView("~/Views/Shared/_MessagePanel.cshtml");
        }

        #region downloadDataToExcel
        public string DownloadFileExcel(LineMasterSettingsScreen line, int? PageFlag, GPPSU.Commons.Models.BaseModel bm)
        {
            byte[] result = null;
            string fileName = null;
            string messg = "";
            IDBContext db = DatabaseManager.Instance.GetContext();
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();

            try
            {
                if (PageFlag == 0)
                {
                    bm.RowsPerPage = Int32.MaxValue;
                }

                IList<LineMasterSettingsScreen> data =
                    line_repo.Search(db, line, bm.CurrentPage, bm.RowsPerPage);

                if (data.Count < 1)
                {
                    messg = "ERROR";
                    ajaxResult.Result = GPPSU.Commons.Models.AjaxResult.VALUE_ERROR;
                    ajaxResult.ErrMesgs = new string[] { messg };

                    List<string> ErrorMessages = new List<string>();

                    ErrorMessages.Add(messg);
                    ajaxResult.ErrMesgs = new string[] {
                        string.Format("ERROR : {0}", ErrorMessages)
                    };

                    return messg;
                }

                fileName = string.Format("LineMasterSettingsScreen-{0}.xls",
                    DateTime.Now.ToString("yyyyMMddHHmmss"));
                result = line_repo.GenerateDownloadFile(data);
                messg = "SUCCESS";
            }
            catch (Exception e)
            {
                //
            }

            this.SendDataAsAttachment(fileName, result);
            return messg;
        }
        #endregion


        #region DownloadTemplate
        public void DownloadTemplate()
        {
            string path = HttpContext.Request.MapPath("~" + CommonConstant.TEMPLATE_EXCEL_DIR + "/Template.xls");
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] result = null;

            HSSFWorkbook workBook = new HSSFWorkbook(fileStream);
            HSSFSheet sheet = (HSSFSheet)workBook.GetSheetAt(0);

            IDataFormat format = workBook.CreateDataFormat();
            ICellStyle cellStyle = NPOIWriter.createCellStyleData(workBook, true);
            ICellStyle cellStyleDateFormat = NPOIWriter.createCellStyleDataDate(workBook, format.GetFormat("yyyy/MM/dd"));

            int row = DATA_ROW_INDEX_START;
            int lenghtColumn = 7;
            IRow Hrow, title, CompanyCd;

            title = sheet.CreateRow(1);
            title.CreateCell(0);
            title.GetCell(0).SetCellValue("GLOBAL PRODUCTION & LOGISTIC SYSTEM TEMPLATE");
            title.GetCell(0).CellStyle = NPOIWriter.createMergedStyleTitle(workBook, true);

            var mergedTitle = new NPOI.SS.Util.CellRangeAddress(1, 1, 0, lenghtColumn);
            sheet.AddMergedRegion(mergedTitle);

            CompanyCd = sheet.CreateRow(3);
            CompanyCd.CreateCell(0);
            CompanyCd.GetCell(0).SetCellValue("Company Code : 807B");
            CompanyCd.GetCell(0).CellStyle = NPOIWriter.createMergedStyleTitle(workBook, false);

            for (int i = 1; i <= 100; i++)
            {
                Hrow = sheet.CreateRow(row);
                for (int x = 0; x <= lenghtColumn; x++)
                {
                    Hrow.CreateCell(x);
                    if (i == 1)
                    {
                        Hrow.GetCell(x).CellStyle = NPOIWriter.createCellStyleColumnHeader(workBook);
                        switch (x)
                        {
                            case 0:
                                Hrow.GetCell(x).SetCellValue("No");
                                break;
                            case 1:
                                Hrow.GetCell(x).SetCellValue("COMPANY_CD");
                                break;
                            case 2:
                                Hrow.GetCell(x).SetCellValue("LINE_CD");
                                break;
                            case 3:
                                Hrow.GetCell(x).SetCellValue("SEQ_NO");
                                break;
                            case 4:
                                Hrow.GetCell(x).SetCellValue("TC_FROM");
                                break;
                            case 5:
                                Hrow.GetCell(x).SetCellValue("TC_TO");
                                break;
                            case 6:
                                Hrow.GetCell(x).SetCellValue("LINE_NAME");
                                break;
                            case 7:
                                Hrow.GetCell(x).SetCellValue("PROCESS_CD");
                                break;
                        }
                    }
                    else
                    {
                        if (x == 4 || x == 5)
                        {
                            Hrow.GetCell(x).CellStyle = cellStyleDateFormat;
                        }
                        else
                        {
                            Hrow.GetCell(x).CellStyle = cellStyle;
                        }
                    }
                }
                if (i != 1)
                {
                    Hrow.GetCell(0).SetCellValue(i - 1);
                }
                row++;
            }

            workBook.SetSheetName(0, "LineMasterSettingTemplate");
            fileStream.Close();
            using (MemoryStream ms = new MemoryStream())
            {
                workBook.Write(ms);
                result = ms.GetBuffer();
            }
            this.SendDataAsAttachment(downloadFileName, result);
        }
        #endregion


        #region uploadProcess
        public ActionResult UploadDataFile(HttpPostedFileBase file, string uploadMode)
        {
            AjaxResult ajaxResult = new AjaxResult();
            IDBContext db = databaseManager.GetContext();
            try
            {
                IList<string> errMesg = new List<string>();
                IList<string> exception = new List<string>();
                IList<LineMasterSettingsScreen> data = getDataLocalUploadExcel(file, errMesg, exception, db);

                setAjaxResultMsg(errMesg, GetMemberName(() => errMesg), ajaxResult);
                setAjaxResultMsg(exception, GetMemberName(() => exception), ajaxResult);

                ajaxResult.Result = AjaxResult.VALUE_SUCCESS;
                ajaxResult.Params = new object[] { data };

            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                ajaxResult.Result = AjaxResult.VALUE_ERROR;
                ajaxResult.ExceptionErrors = new string[] { string.Format(ex.GetType().FullName + " : <br>" + ex.Message) };
            }
            finally
            {
                db.Close();
            }
            return Json(ajaxResult, JsonRequestBehavior.AllowGet);
        }

        private void setAjaxResultMsg(IList<string> message, string varName, AjaxResult ajaxResult)
        {
            if (message.Count >= 1)
            {
                string[] mesg = new string[message.Count];
                for (int i = 0; i < message.Count; i++)
                {
                    mesg[i] = message[i];
                    if (varName.Equals("errMesg"))
                    {
                        ajaxResult.ErrMesgs = mesg;
                    }
                    else if (varName.Equals("exception"))
                    {
                        ajaxResult.ExceptionErrors = mesg;
                    }
                }
            }
        }

        private IList<LineMasterSettingsScreen> getDataLocalUploadExcel(HttpPostedFileBase file, IList<string> errMesgs, IList<string> exception, IDBContext db)
        {
            HSSFWorkbook hssfwb = null;

            using (System.IO.Stream file2 = file.InputStream)
            {
                hssfwb = new HSSFWorkbook(file2);
            }

            if (hssfwb == null)
            {
                throw new ArgumentNullException("Cannot create workbook object from excel file " + file.FileName);
            }

            IRow row = null;
            ICell cell = null;
            IList<LineMasterSettingsScreen> listLine = new List<LineMasterSettingsScreen>();

            ISheet sheet = hssfwb.GetSheetAt(0);
            if (!sheet.SheetName.Equals("LineMasterSettingTemplate"))
            {
                throw new Exception("The template doesn't match, please download the template first");
            }
            else
            {
                for (int indexRow = DATA_ROW_INDEX_START + 1; indexRow <= sheet.LastRowNum; indexRow++)
                {
                    row = sheet.GetRow(indexRow);
                    if (row != null)
                    {
                        LineMasterSettingsScreen lineMaster = new LineMasterSettingsScreen();

                        //Check if the excel has the last data or not
                        List<int> isFirstRowEmpty = new List<int>();
                        List<int> isSecondRowEmpty = new List<int>();
                        for (int i = 1; i < row.Cells.Count; i++)
                        {
                            if (row.GetCell(i) == null || row.GetCell(i).CellType == CellType.Blank)
                            {
                                isFirstRowEmpty.Add(i);
                            }
                        }
                        if (isFirstRowEmpty.Count == (row.Cells.Count) - 1)
                        {
                            row = sheet.GetRow(indexRow + 1);
                            for (int j = 1; j < row.Cells.Count; j++)
                            {
                                if (row.GetCell(j) == null || row.GetCell(j).CellType == CellType.Blank)
                                {
                                    isSecondRowEmpty.Add(j);
                                }
                            }
                            if (isSecondRowEmpty.Count == (row.Cells.Count) - 1)
                            {
                                break;
                            }
                            else
                            {
                                row = sheet.GetRow(indexRow);
                            }
                        }
                        for (int x = 1; x < row.Cells.Count; x++)
                        {
                            getCell(x, indexRow, cell, row, sheet, errMesgs, exception, lineMaster, db);
                        }
                        listLine.Add(lineMaster);
                    }
                }
            }
            return listLine;
        }

        private void getCell(int x, int indexRow, ICell cell, IRow row, ISheet sheet, IList<string> errMesgs, IList<string> exception,
            LineMasterSettingsScreen lineMaster, IDBContext db)
        {
            try
            {
                cell = row.GetCell(x);
                if (cell == null || cell.CellType == CellType.Blank)
                {
                    row = sheet.GetRow(DATA_ROW_INDEX_START);
                    errMesgs.Add(string.Format(row.GetCell(x) + " at Row " + (indexRow - 4) + " Is Empty"));
                }
                else
                {
                    row = sheet.GetRow(DATA_ROW_INDEX_START);
                    switch (x)
                    {
                        case 1:
                            lineMaster.COMPANY_CD = cell.StringCellValue;
                            break;
                        case 2:
                            lineMaster.LINE_CD = cell.StringCellValue;
                            break;
                        case 3:
                            lineMaster.SEQ_NO = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 4:
                            lineMaster.TC_FROM= (cell.DateCellValue).ToString("yyyy/MM/dd");
                            break;
                        case 5:
                            lineMaster.TC_TO = (cell.DateCellValue).ToString("yyyy/MM/dd");
                            break;
                        case 6:
                            lineMaster.LINE_NAME = cell.StringCellValue;
                            break;
                        case 7:
                            lineMaster.PROCESS_CD = cell.StringCellValue;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                row = sheet.GetRow(DATA_ROW_INDEX_START);
                errMesgs.Add(string.Format(row.GetCell(x) + " at Row " + (indexRow - 4) + " Is Empty, Error Mesg : " + ex.Message));
            }
        }
        #endregion


    }
}