using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Xml;
using GPPSU.Models.DayByDayDeliveryPackingCoeficient;
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
    public class DayByDayDeliveryPackingCoeficientController : BaseController
    {
        //DayByDayDeliveryPackingCoeficientController
        public DayByDayDeliveryPackingCoeficientRepository dayRepo = DayByDayDeliveryPackingCoeficientRepository.Instance;
        public DatabaseManager databaseManager = DatabaseManager.Instance;

        public const string downloadFileName = "TemplateDaybyDayDeliveryPackingCoeficient.xls";

        public const int DATA_ROW_INDEX_START = 4;

        protected override void Startup()
        {
            Settings.Title = "WA0AU107 - Set the Day by Day Delivery/Packing Coefficient";
        }


        #region Search & Count
        public ActionResult Search(DayByDayDeliveryPackingCoeficient data, int currentPage, int rowsPerPage, string screenMode = null)
        {
            try
            {
                DoSearch(data, currentPage, rowsPerPage, screenMode);
            }
            catch (Exception e)
            {
                return Json("Error: " + e.Message, JsonRequestBehavior.AllowGet);
            }

            return PartialView("_GridView");
        }

        public void DoSearch(DayByDayDeliveryPackingCoeficient data, int currentPage, int rowsPerPage, string screenMode)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();

            GPPSU.Models.Common.PagingModel paging = new GPPSU.Models.Common.PagingModel(dayRepo.SearchCount(db, data), currentPage, rowsPerPage);
            ViewData["paging"] = paging;

            

            IList<DayByDayDeliveryPackingCoeficient> lineMMData = dayRepo.Search(db, data, paging.StartData, paging.EndData);
            ViewData["DaybyDayDPCData"] = lineMMData;

            //if (lineMMData.Count > 0)
            //{
            if(screenMode == null)
            {
                IList<DailyHeaderList> dynamicDailyHeader = dayRepo.getHeader(db, data);
                ViewData["dynamicDailyHeader"] = dynamicDailyHeader;
            }
                
            //}
        }

        #endregion

        #region Add/Edit Save

        public JsonResult AddEditSave(string screenMode, DayByDayDeliveryPackingCoeficient data)
        {
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();
            GPPSU.Commons.Models.RepoResult repoResult = null;
            IDBContext db = databaseManager.GetContext();

            string noreg = Lookup.Get<User>().RegistrationNumber;

            try
            {
                db.BeginTransaction();
                if (screenMode.Equals("Upload"))
                {
                    foreach (DayByDayDeliveryPackingCoeficient list in data.listData)
                    {
                        repoResult = dayRepo.AddEditSave(list, GetLoginUserId(),screenMode);
                    }
                }
                else
                {
                    repoResult = dayRepo.AddEditSave(data, GetLoginUserId(),screenMode);

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

        #region Delete

        public JsonResult Delete(DayByDayDeliveryPackingCoeficient data)
        {
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();
            GPPSU.Commons.Models.RepoResult repoResult = null;

            IDBContext db = databaseManager.GetContext();

            try
            {
                db.BeginTransaction();

                repoResult = dayRepo.Delete(db, data);

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

        #region Panel

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

        public ActionResult download()
        {
            var MSGSave = "MCSTSTD006I" + ": " + "File download completed successfully";

            List<string> SIABEUL = new List<string>();
            SIABEUL.Add(MSGSave);
            return ShowInfoMessagesSuccess(SIABEUL);

        }


        #endregion

         #region downloadDataToExcel
        public string DownloadFileExcel(DayByDayDeliveryPackingCoeficient deliveryPackingData, int? PageFlag, GPPSU.Commons.Models.BaseModel bm)
        {
            byte[] result = null;
            string fileName = null;
            string messg = "";
            IDBContext db = DatabaseManager.Instance.GetContext();
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();
            PageFlag = 0;
            try
            {
                if (PageFlag == 0)
                {
                    bm.RowsPerPage = Int32.MaxValue;
                }

                IList<DayByDayDeliveryPackingCoeficient> data =
                    dayRepo.Search(db, deliveryPackingData, bm.CurrentPage, bm.RowsPerPage);

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

                fileName = string.Format("DayByDayDeliveryPackingCoeficient-{0}.xls",
                    DateTime.Now.ToString("yyyyMMddHHmmss"));
                result = dayRepo.GenerateDownloadFile(data, deliveryPackingData);
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
            string path = HttpContext.Request.MapPath("~" + CommonConstant.TEMPLATE_EXCEL_DIR + "/" + downloadFileName);
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] result = null;

            HSSFWorkbook workBook = new HSSFWorkbook(fileStream);
            HSSFSheet sheet = (HSSFSheet)workBook.GetSheetAt(0);
            ICellStyle cellStyle = NPOIWriter.createCellStyleData(workBook, true);

            int row = DATA_ROW_INDEX_START;
            int lenghtColumn = 34;
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
                                Hrow.GetCell(x).SetCellValue("DEST_CD");
                                break;
                            case 2:
                                Hrow.GetCell(x).SetCellValue("PROCESS_CD");
                                break;
                            case 3:
                                Hrow.GetCell(x).SetCellValue("YYMM");
                                break;
                            case 4:
                                Hrow.GetCell(x).SetCellValue("Day 1");
                                break;
                            case 5:
                                Hrow.GetCell(x).SetCellValue("Day 2");
                                break;
                            case 6:
                                Hrow.GetCell(x).SetCellValue("Day 3");
                                break;
                            case 7:
                                Hrow.GetCell(x).SetCellValue("Day 4");
                                break;
                            case 8:
                                Hrow.GetCell(x).SetCellValue("Day 5");
                                break;
                            case 9:
                                Hrow.GetCell(x).SetCellValue("Day 6");
                                break;
                            case 10:
                                Hrow.GetCell(x).SetCellValue("Day 7");
                                break;
                            case 11:
                                Hrow.GetCell(x).SetCellValue("Day 8");
                                break;
                            case 12:
                                Hrow.GetCell(x).SetCellValue("Day 9");
                                break;
                            case 13:
                                Hrow.GetCell(x).SetCellValue("Day 10");
                                break;
                            case 14:
                                Hrow.GetCell(x).SetCellValue("Day 11");
                                break;
                            case 15:
                                Hrow.GetCell(x).SetCellValue("Day 12");
                                break;
                            case 16:
                                Hrow.GetCell(x).SetCellValue("Day 13");
                                break;
                            case 17:
                                Hrow.GetCell(x).SetCellValue("Day 14");
                                break;
                            case 18:
                                Hrow.GetCell(x).SetCellValue("Day 15");
                                break;
                            case 19:
                                Hrow.GetCell(x).SetCellValue("Day 16");
                                break;
                            case 20:
                                Hrow.GetCell(x).SetCellValue("Day 17");
                                break;
                            case 21:
                                Hrow.GetCell(x).SetCellValue("Day 18");
                                break;
                            case 22:
                                Hrow.GetCell(x).SetCellValue("Day 19");
                                break;
                            case 23:
                                Hrow.GetCell(x).SetCellValue("Day 20");
                                break;
                            case 24:
                                Hrow.GetCell(x).SetCellValue("Day 21");
                                break;
                            case 25:
                                Hrow.GetCell(x).SetCellValue("Day 22");
                                break;
                            case 26:
                                Hrow.GetCell(x).SetCellValue("Day 23");
                                break;
                            case 27:
                                Hrow.GetCell(x).SetCellValue("Day 24");
                                break;
                            case 28:
                                Hrow.GetCell(x).SetCellValue("Day 25");
                                break;
                            case 29:
                                Hrow.GetCell(x).SetCellValue("Day 26");
                                break;
                            case 30:
                                Hrow.GetCell(x).SetCellValue("Day 27");
                                break;
                            case 31:
                                Hrow.GetCell(x).SetCellValue("Day 28");
                                break;
                            case 32:
                                Hrow.GetCell(x).SetCellValue("Day 29");
                                break;
                            case 33:
                                Hrow.GetCell(x).SetCellValue("Day 30");
                                break;
                            case 34:
                                Hrow.GetCell(x).SetCellValue("Day 31");
                                break;

                        }
                    }
                    else
                    {
                        
                        Hrow.GetCell(x).CellStyle = cellStyle;
                        
                    }
                }
                if (i != 1)
                {
                    Hrow.GetCell(0).SetCellValue(i - 1);
                }
                row++;
            }

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
                IList<DayByDayDeliveryPackingCoeficient> data = getDataLocalUploadExcel(file, errMesg, exception, db);

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

        private IList<DayByDayDeliveryPackingCoeficient> getDataLocalUploadExcel(HttpPostedFileBase file, IList<string> errMesgs, IList<string> exception, IDBContext db)
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
            IList<DayByDayDeliveryPackingCoeficient> listDay = new List<DayByDayDeliveryPackingCoeficient>();

            ISheet sheet = hssfwb.GetSheetAt(0);
            if (!sheet.SheetName.Equals("TemplateGPPSU"))
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
                        DayByDayDeliveryPackingCoeficient dayDelivery = new DayByDayDeliveryPackingCoeficient();

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
                            getCell(x, indexRow, cell, row, sheet, errMesgs, exception, dayDelivery, db);
                        }
                        listDay.Add(dayDelivery);
                    }
                }
            }
            return listDay;
        }

        private void getCell(int x, int indexRow, ICell cell, IRow row, ISheet sheet, IList<string> errMesgs, IList<string> exception,
            DayByDayDeliveryPackingCoeficient dayMaster, IDBContext db)
        {
            try
            {
                cell = row.GetCell(x);
                if (cell == null || cell.CellType == CellType.Blank)
                {
                    if (x == 1 || x == 2 || x == 3)
                    {
                        row = sheet.GetRow(DATA_ROW_INDEX_START);
                        errMesgs.Add(string.Format(row.GetCell(x) + " at Row " + (indexRow - 4) + " Is Empty"));
                    }
                }
                else
                {
                    row = sheet.GetRow(DATA_ROW_INDEX_START);
                    switch (x)
                    {
                        case 1:
                            dayMaster.DEST_CD = cell.StringCellValue;
                            break;
                        case 2:
                            dayMaster.PROCESS_CD = cell.StringCellValue;
                            break;
                        case 3:
                            //dayMaster.PROCESS_CD = Convert.ToInt32(cell.NumericCellValue);
                            dayMaster.YYMM = cell.NumericCellValue.ToString();
                            break;
                        case 4:
                            dayMaster.DAY_ALOC_RATIO_01 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 5:
                            dayMaster.DAY_ALOC_RATIO_02 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 6:
                            dayMaster.DAY_ALOC_RATIO_03 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 7:
                            dayMaster.DAY_ALOC_RATIO_04 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 8:
                            dayMaster.DAY_ALOC_RATIO_05 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 9:
                            dayMaster.DAY_ALOC_RATIO_06 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 10:
                            dayMaster.DAY_ALOC_RATIO_07 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 11:
                            dayMaster.DAY_ALOC_RATIO_08 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 12:
                            dayMaster.DAY_ALOC_RATIO_09 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 13:
                            dayMaster.DAY_ALOC_RATIO_10 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 14:
                            dayMaster.DAY_ALOC_RATIO_11 = Convert.ToInt32(cell.NumericCellValue);
                            break;                   
                        case 15:                    
                            dayMaster.DAY_ALOC_RATIO_12 = Convert.ToInt32(cell.NumericCellValue);
                            break;                   
                        case 16:                     
                            dayMaster.DAY_ALOC_RATIO_13 = Convert.ToInt32(cell.NumericCellValue);
                            break;                   
                        case 17:                     
                            dayMaster.DAY_ALOC_RATIO_14 = Convert.ToInt32(cell.NumericCellValue);
                            break;                   
                        case 18:                     
                            dayMaster.DAY_ALOC_RATIO_15 = Convert.ToInt32(cell.NumericCellValue);
                            break;                   
                        case 19:                     
                            dayMaster.DAY_ALOC_RATIO_16 = Convert.ToInt32(cell.NumericCellValue);
                            break;                   
                        case 20:                     
                            dayMaster.DAY_ALOC_RATIO_17 = Convert.ToInt32(cell.NumericCellValue);
                            break;                   
                        case 21:                     
                            dayMaster.DAY_ALOC_RATIO_18 = Convert.ToInt32(cell.NumericCellValue);
                            break;                   
                        case 22:                     
                            dayMaster.DAY_ALOC_RATIO_19 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 23:
                            dayMaster.DAY_ALOC_RATIO_20 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 24:
                            dayMaster.DAY_ALOC_RATIO_21 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 25:
                            dayMaster.DAY_ALOC_RATIO_22 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 26:
                            dayMaster.DAY_ALOC_RATIO_23 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 27:
                            dayMaster.DAY_ALOC_RATIO_24 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 28:
                            dayMaster.DAY_ALOC_RATIO_25 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 29:
                            dayMaster.DAY_ALOC_RATIO_26 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 30:
                            dayMaster.DAY_ALOC_RATIO_27 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 31:
                            dayMaster.DAY_ALOC_RATIO_28 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 32:
                            dayMaster.DAY_ALOC_RATIO_29 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 33:
                            dayMaster.DAY_ALOC_RATIO_30 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 34:
                            dayMaster.DAY_ALOC_RATIO_31 = Convert.ToInt32(cell.NumericCellValue);
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