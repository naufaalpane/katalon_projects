using GPPSU.Commons.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPPSU.Models.OrderUpdateScreen;
using Toyota.Common.Credential;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;
using Toyota.Common.Download;
using GPPSU.Models.Common;

using TDKUtility;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using GPPSU.Commons.Models;
using System.Globalization;

using GPPSU.Commons.Constants;

namespace GPPSU.Controllers
{
    public class OrderUpdateScreenController : BaseController
    {
        public OrderUpdateScreenRepository orderRepo = OrderUpdateScreenRepository.Instance;
        public DatabaseManager databaseManager = DatabaseManager.Instance;

        public const string downloadFileName = "Template_OrderUpdateScreen.xls";
        public const int DATA_ROW_INDEX_START = 4;

        // GET: OrderUpdateScreen
        protected override void Startup()
        {
            Settings.Title = "WA0AU104 - Order Update";
            OrderUpdateScreen ord = new OrderUpdateScreen();
        }

        public ActionResult Search(OrderUpdateScreen data, int currentPage, int rowsPerPage)
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

        private void DoSearch(OrderUpdateScreen data, int currentPage, int rowsPerPage)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();

            GPPSU.Models.Common.PagingModel paging = new GPPSU.Models.Common.PagingModel(orderRepo.SearchCount(db, data), currentPage, rowsPerPage);
            ViewData["paging"] = paging;

            IList<OrderUpdateScreen> OrderGroupData = orderRepo.Search(db, data, paging.StartData, paging.EndData);
            ViewData["OrderGroupData"] = OrderGroupData.Count > 0 ? OrderGroupData : null;

            IList<DailyHeaderList> dynamicDailyHeader = orderRepo.getHeader(db, data);
            ViewData["dynamicDailyHeader"] = dynamicDailyHeader;
        }

        public JsonResult Create(OrderUpdateScreen data)
        {
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();
            GPPSU.Commons.Models.RepoResult repoResult = null;
            IDBContext db = databaseManager.GetContext();

            string noreg = Lookup.Get<User>().RegistrationNumber;

            try
            {
                repoResult = orderRepo.Create(data, GetLoginUserId());

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

        public JsonResult Prev(OrderUpdateScreen data)
        {
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();
            GPPSU.Commons.Models.RepoResult repoResult = null;
            IDBContext db = databaseManager.GetContext();

            string noreg = Lookup.Get<User>().RegistrationNumber;

            try
            {
                repoResult = orderRepo.Prev(data, GetLoginUserId());

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

        public JsonResult AddEditSave(string screenMode, OrderUpdateScreen data)
        {
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();
            GPPSU.Commons.Models.RepoResult repoResult = null;
            IDBContext db = databaseManager.GetContext();

            string noreg = Lookup.Get<User>().RegistrationNumber;

            try
            {
                db.BeginTransaction();
                if (screenMode.Equals("Add"))
                {
                    repoResult = orderRepo.AddSave(data, GetLoginUserId());
                }
                else if (screenMode.Equals("Edit"))
                {
                    repoResult = orderRepo.EditSave(data, GetLoginUserId());
                }
                else if (screenMode.Equals("Upload"))
                {
                    foreach (OrderUpdateScreen list in data.listData)
                    {
                        repoResult = orderRepo.UploadSave(list, GetLoginUserId());
                    }
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

        public JsonResult Delete(OrderUpdateScreen data)
        {
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();
            GPPSU.Commons.Models.RepoResult repoResult = null;

            IDBContext db = databaseManager.GetContext();

            try
            {
                db.BeginTransaction();

                repoResult = orderRepo.Delete(db, data);

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

        #region downloadDataToExcel
        public string DownloadFileExcel(OrderUpdateScreen order, int? PageFlag, GPPSU.Commons.Models.BaseModel bm)
        {
            byte[] result = null;
            string fileName = null;
            string messg = "";
            IDBContext db = DatabaseManager.Instance.GetContext();
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();

            try
            {
                //if (PageFlag == 0)
                //{
                bm.RowsPerPage = Int32.MaxValue;
                //}

                IList<OrderUpdateScreen> data =
                    orderRepo.Search(db, order, bm.CurrentPage, bm.RowsPerPage);

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

                fileName = string.Format("OrderUpdateScreen-{0}.xls",
                    DateTime.Now.ToString("yyyyMMddHHmmss"));
                result = orderRepo.GenerateDownloadFile(data);
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
        public void DownloadTemplate(OrderUpdateScreen data)
        {
            string path = HttpContext.Request.MapPath("~" + CommonConstant.TEMPLATE_EXCEL_DIR + "/" + downloadFileName);
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] result = null;

            HSSFWorkbook workBook = new HSSFWorkbook(fileStream);
            HSSFSheet sheet = (HSSFSheet)workBook.GetSheetAt(0);
            ICellStyle cellStyle = NPOIWriter.createCellStyleData(workBook, true);

            int row = DATA_ROW_INDEX_START;
            int lenghtColumn = 45;
            IRow Hrow, title;

            title = sheet.CreateRow(1);
            title.CreateCell(0);
            title.GetCell(0).SetCellValue("GLOBAL PRODUCTION & LOGISTIC SYSTEM TEMPLATE");
            title.GetCell(0).CellStyle = NPOIWriter.createMergedStyleTitle(workBook, true);

            var mergedTitle = new NPOI.SS.Util.CellRangeAddress(1, 1, 0, lenghtColumn);
            sheet.AddMergedRegion(mergedTitle);

            
            for (int i = 1; i <= 26; i++)
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
                                Hrow.GetCell(x).SetCellValue("'Company Code");
                                break;
                            case 2:
                                Hrow.GetCell(x).SetCellValue("'Importer Code");
                                break;
                            case 3:
                                Hrow.GetCell(x).SetCellValue("'Exporter Code");
                                break;
                            case 4:
                                Hrow.GetCell(x).SetCellValue("'Order Type");
                                break;
                            case 5:
                                Hrow.GetCell(x).SetCellValue("'Year Month");
                                break;
                            case 6:
                                Hrow.GetCell(x).SetCellValue("'CFC");
                                break;
                            case 7:
                                Hrow.GetCell(x).SetCellValue("Status Code");
                                break;
                            case 8:
                                Hrow.GetCell(x).SetCellValue("'Parts No");
                                break;
                            case 9:
                                Hrow.GetCell(x).SetCellValue("'Re-Exp");
                                break;
                            case 10:
                                Hrow.GetCell(x).SetCellValue("Lot Size");
                                break;
                            case 11:
                                Hrow.GetCell(x).SetCellValue("Total 01");
                                break;
                            case 12:
                                Hrow.GetCell(x).SetCellValue("Total 02");
                                break;
                            case 13:
                                Hrow.GetCell(x).SetCellValue("Total 03");
                                break;
                            case 14:
                                Hrow.GetCell(x).SetCellValue("Total 04");
                                break;
                            case 15:
                                Hrow.GetCell(x).SetCellValue("Day 1");
                                break;
                            case 16:
                                Hrow.GetCell(x).SetCellValue("Day 2");
                                break;
                            case 17:
                                Hrow.GetCell(x).SetCellValue("Day 3");
                                break;
                            case 18:
                                Hrow.GetCell(x).SetCellValue("Day 4");
                                break;
                            case 19:
                                Hrow.GetCell(x).SetCellValue("Day 5");
                                break;
                            case 20:
                                Hrow.GetCell(x).SetCellValue("Day 6");
                                break;
                            case 21:
                                Hrow.GetCell(x).SetCellValue("Day 7");
                                break;
                            case 22:
                                Hrow.GetCell(x).SetCellValue("Day 8");
                                break;
                            case 23:
                                Hrow.GetCell(x).SetCellValue("Day 9");
                                break;
                            case 24:
                                Hrow.GetCell(x).SetCellValue("Day 10");
                                break;
                            case 25:
                                Hrow.GetCell(x).SetCellValue("Day 11");
                                break;
                            case 26:
                                Hrow.GetCell(x).SetCellValue("Day 12");
                                break;
                            case 27:
                                Hrow.GetCell(x).SetCellValue("Day 13");
                                break;
                            case 28:
                                Hrow.GetCell(x).SetCellValue("Day 14");
                                break;
                            case 29:
                                Hrow.GetCell(x).SetCellValue("Day 15");
                                break;
                            case 30:
                                Hrow.GetCell(x).SetCellValue("Day 16");
                                break;
                            case 31:
                                Hrow.GetCell(x).SetCellValue("Day 17");
                                break;
                            case 32:
                                Hrow.GetCell(x).SetCellValue("Day 18");
                                break;
                            case 33:
                                Hrow.GetCell(x).SetCellValue("Day 19");
                                break;
                            case 34:
                                Hrow.GetCell(x).SetCellValue("Day 20");
                                break;
                            case 35:
                                Hrow.GetCell(x).SetCellValue("Day 21");
                                break;
                            case 36:
                                Hrow.GetCell(x).SetCellValue("Day 22");
                                break;
                            case 37:
                                Hrow.GetCell(x).SetCellValue("Day 23");
                                break;
                            case 38:
                                Hrow.GetCell(x).SetCellValue("Day 24");
                                break;
                            case 39:
                                Hrow.GetCell(x).SetCellValue("Day 25");
                                break;
                            case 40:
                                Hrow.GetCell(x).SetCellValue("Day 26");
                                break;
                            case 41:
                                Hrow.GetCell(x).SetCellValue("Day 27");
                                break;
                            case 42:
                                Hrow.GetCell(x).SetCellValue("Day 28");
                                break;
                            case 43:
                                Hrow.GetCell(x).SetCellValue("Day 29");
                                break;
                            case 44:
                                Hrow.GetCell(x).SetCellValue("Day 30");
                                break;
                            case 45:
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
                IList<OrderUpdateScreen> data = getDataLocalUploadExcel(file, errMesg, exception, db);

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

        private IList<OrderUpdateScreen> getDataLocalUploadExcel(HttpPostedFileBase file, IList<string> errMesgs, IList<string> exception, IDBContext db)
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
            IList<OrderUpdateScreen> listOrder = new List<OrderUpdateScreen>();

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
                        OrderUpdateScreen orderMaster = new OrderUpdateScreen();

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
                            getCell(x, indexRow, cell, row, sheet, errMesgs, exception, orderMaster, db);
                        }
                        listOrder.Add(orderMaster);
                    }
                }
            }
            return listOrder;
        }

        private void getCell(int x, int indexRow, ICell cell, IRow row, ISheet sheet, IList<string> errMesgs, IList<string> exception,
            OrderUpdateScreen orderMaster, IDBContext db)
        {
            try
            {
                cell = row.GetCell(x);
                if (cell == null || cell.CellType == CellType.Blank)
                {
                    if (x == 1 || x == 2 || x == 3 || x == 4 || x==5 || x == 6 || x == 7 || x == 8 || x == 9 || x == 10)
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
                            orderMaster.COMPANY_CD = cell.StringCellValue;
                            break;
                        case 2:
                            orderMaster.IMPORTER_CD = cell.StringCellValue;
                            break;
                        case 3:
                            orderMaster.EXPORTER_CD = cell.StringCellValue;
                            break;
                        case 4:
                            orderMaster.ORDER_TYPE = cell.StringCellValue;
                            break;
                        case 5:
                            orderMaster.PACK_MONTH = cell.StringCellValue;
                            break;
                        case 6:
                            orderMaster.CFC = cell.StringCellValue;
                            break;
                        case 7:
                            orderMaster.STATUS_CD = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 8:
                            orderMaster.PART_NO = cell.StringCellValue;
                            break;
                        case 9:
                            orderMaster.RE_EXPORT_CD = cell.StringCellValue;
                            break;
                        case 10:
                            orderMaster.LOT_SIZE = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 11:
                            orderMaster.TOTAL_MONTHLY_01 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 12:
                            orderMaster.TOTAL_MONTHLY_02 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 13:
                            orderMaster.TOTAL_MONTHLY_03 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 14:
                            orderMaster.TOTAL_MONTHLY_04 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 15:
                            orderMaster.DAY_ORD_VOL_01 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 16:
                            orderMaster.DAY_ORD_VOL_02 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 17:
                            orderMaster.DAY_ORD_VOL_03 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 18:
                            orderMaster.DAY_ORD_VOL_04 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 19:
                            orderMaster.DAY_ORD_VOL_05 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 20:
                            orderMaster.DAY_ORD_VOL_06 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 21:
                            orderMaster.DAY_ORD_VOL_07 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 22:
                            orderMaster.DAY_ORD_VOL_08 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 23:
                            orderMaster.DAY_ORD_VOL_09 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 24:
                            orderMaster.DAY_ORD_VOL_10 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 25:
                            orderMaster.DAY_ORD_VOL_11 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 26:
                            orderMaster.DAY_ORD_VOL_12 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 27:
                            orderMaster.DAY_ORD_VOL_13 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 28:
                            orderMaster.DAY_ORD_VOL_14 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 29:
                            orderMaster.DAY_ORD_VOL_15 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 30:
                            orderMaster.DAY_ORD_VOL_16 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 31:
                            orderMaster.DAY_ORD_VOL_17 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 32:
                            orderMaster.DAY_ORD_VOL_18 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 33:
                            orderMaster.DAY_ORD_VOL_19 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 34:
                            orderMaster.DAY_ORD_VOL_20 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 35:
                            orderMaster.DAY_ORD_VOL_21 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 36:
                            orderMaster.DAY_ORD_VOL_22 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 37:
                            orderMaster.DAY_ORD_VOL_23 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 38:
                            orderMaster.DAY_ORD_VOL_24 = Convert.ToInt32(cell.NumericCellValue);
                            break; ;
                        case 39:
                            orderMaster.DAY_ORD_VOL_25 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 40:
                            orderMaster.DAY_ORD_VOL_26 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 41:
                            orderMaster.DAY_ORD_VOL_27 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 42:
                            orderMaster.DAY_ORD_VOL_28 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 43:
                            orderMaster.DAY_ORD_VOL_29 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 44:
                            orderMaster.DAY_ORD_VOL_30 = Convert.ToInt32(cell.NumericCellValue);
                            break;
                        case 45:
                            orderMaster.DAY_ORD_VOL_31 = Convert.ToInt32(cell.NumericCellValue);
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