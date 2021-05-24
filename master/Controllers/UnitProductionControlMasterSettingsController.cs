using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using GPPSU.Commons.Controllers;
using GPPSU.Models.UnitProductionControlMasterSettings;
using GPPSU.Commons.Models;
using GPPSU.Commons.Constants;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;


namespace GPPSU.Controllers
{
    public class UnitProductionControlMasterSettingsController : BaseController
    {
        public UnitProductionControlMasterSettingsRepository UnitProdRepo = UnitProductionControlMasterSettingsRepository.Instance;
        public DatabaseManager databaseManager = DatabaseManager.Instance;
        public const string downloadFileName = "UnitProductionControl_UploadTemplate.xls";
        public const int DATA_ROW_INDEX_START = 4;

        #region Startup
        protected override void Startup()
        {
            Settings.Title = "WA0AUC02 - Unit Production Control Master Settings";
            UnitProductionControlMasterSettings UnitProdData = new UnitProductionControlMasterSettings();
            DoSearch(UnitProdData, 1, 10);
        }
        #endregion

        #region Search
        public ActionResult Search(UnitProductionControlMasterSettings data, int currentPage, int rowsPerPage)
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

        public void DoSearch(UnitProductionControlMasterSettings data, int currentPage, int rowsPerPage)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();

            GPPSU.Models.Common.PagingModel paging = new GPPSU.Models.Common.PagingModel(UnitProdRepo.SearchCount(db, data), currentPage, rowsPerPage);
            ViewData["Paging"] = paging;

            IList<UnitProductionControlMasterSettings> UnitProdData = UnitProdRepo.Search(db, data, paging.StartData, paging.EndData);
            ViewData["UnitProdRepoData"] = UnitProdData;
        }

        #endregion

        #region SaveAddEdit
        public JsonResult SaveAddEdit(string screenMode, UnitProductionControlMasterSettings data)
        {
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();
            GPPSU.Commons.Models.RepoResult repoResult = null;
            IDBContext db = databaseManager.GetContext();

            //Toyota.Common.Credential.User u = Lookup.Get<Toyota.Common.Credential.User>();
            //string userId = u.Username;
            //string companyCode = u.Company.Id;

            try
            {
                db.BeginTransaction();
                if (screenMode.Equals("ADD"))
                {
                    repoResult = UnitProdRepo.SaveAddEdit(data, GetLoginUserId(), screenMode/*, companyCode*/);
                }
                else if (screenMode.Equals("EDIT"))
                {
                    repoResult = UnitProdRepo.SaveAddEdit(data, GetLoginUserId(), screenMode);
                }
                else if (screenMode.Equals("Upload"))
                {
                    int i = 0;
                    foreach (UnitProductionControlMasterSettings list in data.listData)
                    {
                        repoResult = UnitProdRepo.SaveAddEdit(list, GetLoginUserId(), screenMode);
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
                ajaxResult.ErrMesgs = new string[] {string.Format("{0} = {1}", e.GetType().FullName, e.Message)
                };
            }
            finally
            {
                db.Close();
            }

            return Json(ajaxResult);
        }

        #endregion

        #region Delete
        public JsonResult Delete(UnitProductionControlMasterSettings data)
        {
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();
            GPPSU.Commons.Models.RepoResult repoResult = null;

            IDBContext db = databaseManager.GetContext();

            try
            {
                db.BeginTransaction();

                repoResult = UnitProdRepo.Delete(db, data);

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
                ajaxResult.ErrMesgs = new string[] {string.Format("ERROR : {0}", e.Message)};
            }
            finally
            {
                db.Close();
            }
            return Json(ajaxResult);
        }
        #endregion

        #region Download Data to Excell
        public string DownloadFileExcel(UnitProductionControlMasterSettings datas, int? PageFlag, GPPSU.Commons.Models.BaseModel bm)
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

                IList<UnitProductionControlMasterSettings> data = UnitProdRepo.Search(db, datas, bm.CurrentPage, bm.RowsPerPage);

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

                fileName = string.Format("Unit Production Control-{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmss"));
                result = UnitProdRepo.GenerateDownloadFile(data);
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

        #region Download Template for Upload
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
            int lenghtColumn = 8;
            IRow Hrow, title, CompanyCd;

            title = sheet.CreateRow(1);
            title.CreateCell(0);
            title.GetCell(0).SetCellValue("GLOBAL PRODUCTION & LOGISTIC SYSTEM TEMPLATE");
            title.GetCell(0).CellStyle = NPOIWriter.createMergedStyleTitle(workBook, true);

            var mergedTitle = new NPOI.SS.Util.CellRangeAddress(1, 1, 0, lenghtColumn);
            sheet.AddMergedRegion(mergedTitle);

            //Toyota.Common.Credential.User u = Lookup.Get<Toyota.Common.Credential.User>();
            //string companyCode = u.Company.Id;

            CompanyCd = sheet.CreateRow(3);
            CompanyCd.CreateCell(0);
            CompanyCd.GetCell(0).SetCellValue("Company Code : 807B" /*companyCode*/);
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
                                Hrow.GetCell(x).SetCellValue("CAR_FAMILY_CD");
                                break;
                            case 2:
                                Hrow.GetCell(x).SetCellValue("PART_NO");
                                break;
                            case 3:
                                Hrow.GetCell(x).SetCellValue("LINE_CD");
                                break;
                            case 4:
                                Hrow.GetCell(x).SetCellValue("STATUS_CD");
                                break;
                            case 5:
                                Hrow.GetCell(x).SetCellValue("PART_NAME");
                                break;
                            case 6:
                                Hrow.GetCell(x).SetCellValue("UNIT_SIGN");
                                break;
                            case 7:
                                Hrow.GetCell(x).SetCellValue("TC_FROM");
                                break;
                            case 8:
                                Hrow.GetCell(x).SetCellValue("TC_TO");
                                break;
                        }
                    }
                    else
                    {
                        if (x == 7 || x == 8)
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

            workBook.SetSheetName(0, "UnitProductionControlTemplate");
            fileStream.Close();
            using (MemoryStream ms = new MemoryStream())
            {
                workBook.Write(ms);
                result = ms.GetBuffer();
            }
            this.SendDataAsAttachment(downloadFileName, result);
        }

        #endregion

        #region Upload
        public ActionResult UploadDataFile(HttpPostedFileBase file, string uploadMode)
        {
            AjaxResult ajaxResult = new AjaxResult();
            IDBContext db = databaseManager.GetContext();
            try
            {
                IList<string> errMesg = new List<string>();
                IList<string> exception = new List<string>();
                IList<UnitProductionControlMasterSettings> data = getDataLocalUploadExcel(file, errMesg, exception, db);

                setAjaxResultMsg(errMesg, GetMemberName(() => errMesg), ajaxResult);
                setAjaxResultMsg(exception, GetMemberName(() => exception), ajaxResult);

                ajaxResult.Result = GPPSU.Commons.Models.AjaxResult.VALUE_SUCCESS;
                ajaxResult.Params = new object[] { data };

            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                ajaxResult.Result = GPPSU.Commons.Models.AjaxResult.VALUE_ERROR;
                ajaxResult.ExceptionErrors = new string[] { string.Format(ex.GetType().FullName + " : <br>" + ex.Message) };
            }
            finally
            {
                db.Close();
            }
            return Json(ajaxResult, JsonRequestBehavior.AllowGet);
        }


        private IList<UnitProductionControlMasterSettings> getDataLocalUploadExcel(HttpPostedFileBase file, IList<string> errMesgs, IList<string> exception, IDBContext db)
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
            IList<UnitProductionControlMasterSettings> listUnitProd = new List<UnitProductionControlMasterSettings>();

            ISheet sheet = hssfwb.GetSheetAt(0);
            if (!sheet.SheetName.Equals("UnitProductionControlTemplate"))
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
                        UnitProductionControlMasterSettings unitProdMaster = new UnitProductionControlMasterSettings();

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
                            getCell(x, indexRow, cell, row, sheet, errMesgs, exception, unitProdMaster, db);
                        }
                        listUnitProd.Add(unitProdMaster);
                    }
                }
            }
            return listUnitProd;
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

        private void getCell(int x, int indexRow, ICell cell, IRow row, ISheet sheet, IList<string> errMesgs, IList<string> exception,
                            UnitProductionControlMasterSettings unitProdMaster, IDBContext db)
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
                            unitProdMaster.CFC = cell.StringCellValue;
                            break;
                        case 2:
                            unitProdMaster.PART_NO = cell.StringCellValue;
                            break;
                        case 3:
                            unitProdMaster.LINE_CD = cell.StringCellValue;
                            break;
                        case 4:
                            unitProdMaster.STATUS_CD = cell.NumericCellValue.ToString();
                            break;
                        case 5:
                            unitProdMaster.PART_NAME = cell.StringCellValue;
                            break;
                        case 6:
                            unitProdMaster.UNIT_SIGN = cell.StringCellValue;
                            break;
                        case 7:
                            unitProdMaster.TC_FROM = (cell.DateCellValue).ToString("yyyy/MM/dd");
                            break;
                        case 8:
                            unitProdMaster.TC_TO = (cell.DateCellValue).ToString("yyyy/MM/dd");
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