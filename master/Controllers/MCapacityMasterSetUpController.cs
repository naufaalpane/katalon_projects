using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using GPPSU.Models.Common;
using GPPSU.Commons.Controllers;
using GPPSU.Commons.Models;
using GPPSU.Models.MCapacityMasterSetUp;
using GPPSU.Commons.Constants;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;


namespace GPPSU.Controllers
{
    public class MCapacityMasterSetUpController : BaseController
    {
        public MCapacityMasterSetUpRepository MCapacityRepo = MCapacityMasterSetUpRepository.Instance;
        public DatabaseManager databaseManager = DatabaseManager.Instance;
        public const string downloadFileName = "MCapacityMaster_UploadTemplate.xls";
        public const int DATA_ROW_INDEX_START = 4;

        #region StartUp()
        protected override void Startup()
        {
            Settings.Title = "WA0AUC04 - K / M Capacity Master Set Up";
            MCapacityMasterSetUp MCapacityData = new MCapacityMasterSetUp();
            DoSearch(MCapacityData, 1, 10);
        }
        #endregion

        #region Search
        public ActionResult Search(MCapacityMasterSetUp param, int currentPage, int rowsPerPage)
        {
            try
            {
                DoSearch(param, currentPage, rowsPerPage);
            }
            catch (Exception e)
            {
                return Json("Error: " + e.Message, JsonRequestBehavior.AllowGet);
            }

            return PartialView("_GridView");
        }

        public void DoSearch(MCapacityMasterSetUp param, int currentPage, int rowsPerPage)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();

            GPPSU.Models.Common.PagingModel paging = new GPPSU.Models.Common.PagingModel(MCapacityRepo.Count(db, param), currentPage, rowsPerPage);
            ViewData["Paging"] = paging;

            IList<MCapacityMasterSetUp> MCapacityData = MCapacityRepo.Search(db, param, paging.StartData, paging.EndData);
            ViewData["Detail"] = MCapacityData;
        }
        #endregion

        #region AddEditSave
        public ActionResult AddEditSave(string screenMode, MCapacityMasterSetUp data)
        {
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();
            RepoResult repoResult = null;

            IDBContext db = databaseManager.GetContext();

            Toyota.Common.Credential.User u = Lookup.Get<Toyota.Common.Credential.User>();
            string userId = u.Username;
            string companyCode = u.Company.Id;

            try
            {
                db.BeginTransaction();
                if (screenMode.Equals("Upload"))
                {
                    int x = 0;
                    foreach (MCapacityMasterSetUp i in data.listData)
                    {
                        repoResult = MCapacityRepo.SaveAddEdit(i, userId, companyCode, screenMode);
                        if (Commons.Models.AjaxResult.VALUE_SUCCESS.Equals(repoResult.Result))
                        {
                            db.CommitTransaction();
                        }
                        else
                        {
                            if (ajaxResult.ErrMesgs == null) ajaxResult.ErrMesgs = new string[data.listData.Count];
                            ajaxResult.ErrMesgs[x] = repoResult.ErrMesgs[0];
                            db.AbortTransaction();
                        }
                        x++;
                    }
                }
                else if (screenMode.Equals("ADD"))
                {
                    repoResult = MCapacityRepo.SaveAddEdit(data, userId, companyCode, screenMode);
                }
                else if (screenMode.Equals("EDIT"))
                {
                    repoResult = MCapacityRepo.SaveAddEdit(data, userId, companyCode, screenMode);
                }

                if (!screenMode.Equals("Upload"))
                {
                    CopyPropertiesRepoToAjaxResult(repoResult, ajaxResult);
                    if (GPPSU.Commons.Models.AjaxResult.VALUE_ERROR.Equals(ajaxResult.Result))
                    {
                        db.AbortTransaction();
                    }
                    else
                    {
                        db.CommitTransaction();
                    }
                }
                
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                ajaxResult.Result = GPPSU.Commons.Models.AjaxResult.VALUE_ERROR;
                ajaxResult.ErrMesgs = new string[]{string.Format("{0} = {1}", ex.GetType().FullName, ex.Message)};
            }
            finally
            {
                db.Close();
            }

            return Json(ajaxResult);
        }

        public JsonResult GetByKey(MCapacityMasterSetUp param)
        {
            MCapacityMasterSetUp result = null;
            IDBContext db = databaseManager.GetContext();
            result = MCapacityRepo.GetByKey(db, param);

            return Json(result);
        }

        #endregion

        #region Delete
        public JsonResult Delete(MCapacityMasterSetUp data)
        {
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();
            GPPSU.Commons.Models.RepoResult repoResult = null;

            IDBContext db = databaseManager.GetContext();

            try
            {
                db.BeginTransaction();

                repoResult = MCapacityRepo.Delete(db, data);

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

        #region DownloadFileExcel
        public string DownloadFileExcel(MCapacityMasterSetUp datas, int? PageFlag, GPPSU.Commons.Models.BaseModel bm)
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

                IList<MCapacityMasterSetUp> data = MCapacityRepo.Search(db, datas, bm.CurrentPage, bm.RowsPerPage);

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

                fileName = string.Format("M Capacity Master-{0}.xls",DateTime.Now.ToString("yyyyMMddHHmmss"));
                result = MCapacityRepo.GenerateDownloadFile(data);
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
            int lenghtColumn = 6;
            IRow Hrow, title, CompanyCd;

            title = sheet.CreateRow(1);
            title.CreateCell(0);
            title.GetCell(0).SetCellValue("GLOBAL PRODUCTION & LOGISTIC SYSTEM TEMPLATE");
            title.GetCell(0).CellStyle = NPOIWriter.createMergedStyleTitle(workBook, true);

            var mergedTitle = new NPOI.SS.Util.CellRangeAddress(1, 1, 0, lenghtColumn);
            sheet.AddMergedRegion(mergedTitle);

            Toyota.Common.Credential.User u = Lookup.Get<Toyota.Common.Credential.User>();
            string companyCode = u.Company.Id;

            CompanyCd = sheet.CreateRow(3);
            CompanyCd.CreateCell(0);
            CompanyCd.GetCell(0).SetCellValue("Company Code : " + companyCode);
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
                                Hrow.GetCell(x).SetCellValue("PROCESS_CODE");
                                break;
                            case 2:
                                Hrow.GetCell(x).SetCellValue("LINE_CODE");
                                break;
                            case 3:
                                Hrow.GetCell(x).SetCellValue("HEIJUNKA_CODE");
                                break;
                            case 4:
                                Hrow.GetCell(x).SetCellValue("CAPACITY");
                                break;
                            case 5:
                                Hrow.GetCell(x).SetCellValue("TC_FROM");
                                break;
                            case 6:
                                Hrow.GetCell(x).SetCellValue("TC_TO");
                                break;
                        }
                    }
                    else
                    {
                        if (x == 5 || x == 6)
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

            workBook.SetSheetName(0, "TemplateMCapacityMaster");

            fileStream.Close();
            using (MemoryStream ms = new MemoryStream())
            {
                workBook.Write(ms);
                result = ms.GetBuffer();
            }
            this.SendDataAsAttachment(downloadFileName, result);
        }

        #endregion

        #region Procces Upload
        public ActionResult UploadDataFile(HttpPostedFileBase file, string uploadMode)
        {
            AjaxResult ajaxResult = new AjaxResult();
            IDBContext db = databaseManager.GetContext();
            try
            {
                IList<string> errMesg = new List<string>();
                IList<string> exception = new List<string>();
                IList<MCapacityMasterSetUp> data = getDataLocalUploadExcel(file, errMesg, exception, db);

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


        private IList<MCapacityMasterSetUp> getDataLocalUploadExcel(HttpPostedFileBase file, IList<string> errMesgs, IList<string> exception, IDBContext db)
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
            IList<MCapacityMasterSetUp> listCapacity = new List<MCapacityMasterSetUp>();

            ISheet sheet = hssfwb.GetSheetAt(0);
            if (!sheet.SheetName.Equals("TemplateMCapacityMaster"))
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
                        MCapacityMasterSetUp capacityMaster = new MCapacityMasterSetUp();

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
                            getCell(x, indexRow, cell, row, sheet, errMesgs, exception, capacityMaster, db);
                        }
                        listCapacity.Add(capacityMaster);
                    }
                }
            }
            return listCapacity;
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
                            MCapacityMasterSetUp capacityMaster, IDBContext db)
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
                            capacityMaster.PROCESS_CD = cell.StringCellValue;
                            break;
                        case 2:
                            capacityMaster.LINE_CD = cell.StringCellValue;
                            break;
                        case 3:
                            capacityMaster.HEIJUNKA_CD = cell.StringCellValue;
                            break;
                        case 4:
                            capacityMaster.CAPACITY = cell.NumericCellValue.ToString();
                            break;
                        case 5:
                            capacityMaster.TC_FROM = (cell.DateCellValue).ToString("yyyy/MM/dd");
                            break;
                        case 6:
                            capacityMaster.TC_TO = (cell.DateCellValue).ToString("yyyy/MM/dd");
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