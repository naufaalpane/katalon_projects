using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPPSU.Models.DestinationMasterSettingsScreen;

using Toyota.Common.Credential;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;
using GPPSU.Commons.Models;
using Toyota.Common.Download;

using GPPSU.Commons.Controllers;

using TDKUtility;
using GPPSU.Commons.Constants;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Globalization;
using NPOI.SS.Util;

namespace GPPSU.Controllers
{

    public class DestinationMasterSettingsScreenController : BaseController
    {
        public DestinationMasterSettingsScreenRepository DestGroupRepo = DestinationMasterSettingsScreenRepository.Instance;
        public DatabaseManager databaseManager = DatabaseManager.Instance;

        public const string downloadFileName = "DestinationMasterSettingTemplate.xls";

        public const int DATA_ROW_INDEX_START = 4;

        protected override void Startup()
        {
            Settings.Title = "WA0AUC07 - Destination Master Settings Screen";
            DestinationMasterSettingsScreen DestData = new DestinationMasterSettingsScreen();
            DoSearch(DestData, 1, 10);
        }

        public ActionResult Search(DestinationMasterSettingsScreen data, int currentPage, int rowsPerPage)
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

        public void DoSearch(DestinationMasterSettingsScreen data, int currentPage, int rowsPerPage)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();

            GPPSU.Models.Common.PagingModel paging = new GPPSU.Models.Common.PagingModel(DestGroupRepo.SearchCount(db, data), currentPage, rowsPerPage);
            ViewData["paging"] = paging;

            IList<DestinationMasterSettingsScreen> DestGroupData = DestGroupRepo.Search(db, data, paging.StartData, paging.EndData);
            ViewData["DestGroupData"] = DestGroupData;
        }

        public ActionResult ShowInfoMessagesSuccess(List<string> messages)
        {
            ViewData["Messages"] = messages;
            return PartialView("~/Views/Shared/_MessagePanel.cshtml");
        }

        public JsonResult AddEditSave(string screenMode, DestinationMasterSettingsScreen data)
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
                    repoResult = DestGroupRepo.AddSave(data, GetLoginUserId(), screenMode);
                }
                else if (screenMode.Equals("Edit"))
                {
                    repoResult = DestGroupRepo.EditSave(data, GetLoginUserId(), screenMode);
                }
                else if (screenMode.Equals("Upload"))
                {
                    int i = 0;
                    foreach (DestinationMasterSettingsScreen list in data.listData)
                    {
                        repoResult = DestGroupRepo.AddSave(list, GetLoginUserId(), screenMode);
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

        public JsonResult Delete(DestinationMasterSettingsScreen data)
        {
            GPPSU.Commons.Models.AjaxResult ajaxResult = new GPPSU.Commons.Models.AjaxResult();
            GPPSU.Commons.Models.RepoResult repoResult = null;

            IDBContext db = databaseManager.GetContext();

            try
            {
                db.BeginTransaction();

                repoResult = DestGroupRepo.Delete(db, data);

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
        public string DownloadFileExcel(DestinationMasterSettingsScreen dest, int? PageFlag, GPPSU.Commons.Models.BaseModel bm)
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

                IList<DestinationMasterSettingsScreen> data =
                    DestGroupRepo.Search(db, dest, bm.CurrentPage, bm.RowsPerPage);

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

                fileName = string.Format("DestinationMasterSettingsScreen-{0}.xls",
                    DateTime.Now.ToString("yyyyMMddHHmmss"));
                result = DestGroupRepo.GenerateDownloadFile(data);
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
                                Hrow.GetCell(x).SetCellValue("Destination_Code");
                                break;
                            case 2:
                                Hrow.GetCell(x).SetCellValue("Destination_Name");
                                break;
                            case 3:
                                Hrow.GetCell(x).SetCellValue("Export_Code");
                                break;
                            case 4:
                                Hrow.GetCell(x).SetCellValue("Lead_Time");
                                break;
                            case 5:
                                Hrow.GetCell(x).SetCellValue("e_Kanban");
                                break;
                            case 6:
                                Hrow.GetCell(x).SetCellValue("TC_From");
                                break;
                            case 7:
                                Hrow.GetCell(x).SetCellValue("TC_To");
                                break;
                        }
                    }
                    else
                    {
                        if (x == 6 || x == 7)
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
            createComboBoxCell(DATA_ROW_INDEX_START, row, 3, sheet, new List<string>() { "E", "D" });
            createComboBoxCell(DATA_ROW_INDEX_START, row, 5, sheet, new List<string>() { "N", "Y" });

            workBook.SetSheetName(0, "DestinationMasterTemplate");

            fileStream.Close();
            using (MemoryStream ms = new MemoryStream())
            {
                workBook.Write(ms);
                result = ms.GetBuffer();
            }
            this.SendDataAsAttachment(downloadFileName, result);
        }

        private void createComboBoxCell(int firtsRow, int lastRow, int col, HSSFSheet sheet, List<string> List)
        {
            CellRangeAddressList addressList = new CellRangeAddressList(firtsRow + 1, lastRow - 1, col, col);
            DVConstraint constraint = null;

            constraint = DVConstraint.CreateExplicitListConstraint(List.ToArray());

            HSSFDataValidation validation = new HSSFDataValidation(addressList, constraint);
            sheet.AddValidationData(validation);
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
                IList<DestinationMasterSettingsScreen> data = getDataLocalUploadExcel(file, errMesg, exception, db);

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

        private IList<DestinationMasterSettingsScreen> getDataLocalUploadExcel(HttpPostedFileBase file, IList<string> errMesgs, IList<string> exception, IDBContext db)
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
            IList<DestinationMasterSettingsScreen> listDestination = new List<DestinationMasterSettingsScreen>();

            ISheet sheet = hssfwb.GetSheetAt(0);
            if (!sheet.SheetName.Equals("DestinationMasterTemplate"))
            {
                throw new Exception("The template doesn't match, please download the template first");
            }
            else
            {
                IList<Time_Control> timeControl = new List<Time_Control>();
                for (int indexRow = DATA_ROW_INDEX_START + 1; indexRow <= sheet.LastRowNum; indexRow++)
                {
                    row = sheet.GetRow(indexRow);
                    if (row != null)
                    {
                        DestinationMasterSettingsScreen destinationMaster = new DestinationMasterSettingsScreen();

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
                            getCell(x, indexRow, cell, row, sheet, errMesgs, exception, destinationMaster, db, timeControl);
                        }
                        listDestination.Add(destinationMaster);
                    }
                }
            }
            return listDestination;
        }

        private void getCell(int x, int indexRow, ICell cell, IRow row, ISheet sheet, IList<string> errMesgs, IList<string> exception,
            DestinationMasterSettingsScreen destinationMaster, IDBContext db, IList<Time_Control> timeControl)
        {
            try
            {
                Time_Control tc = new Time_Control();
                cell = row.GetCell(x);
                if (cell == null || cell.CellType == CellType.Blank)
                {
                    row = sheet.GetRow(DATA_ROW_INDEX_START);
                    errMesgs.Add(string.Format(row.GetCell(x) + " at Row " + (indexRow - DATA_ROW_INDEX_START) + " Is Empty"));
                }
                else
                {
                    row = sheet.GetRow(DATA_ROW_INDEX_START);
                    switch (x)
                    {
                        case 1:
                            if (cell.StringCellValue.Length > 4)
                            {
                                errMesgs.Add(string.Format(row.GetCell(x) + " at Row " + (indexRow - DATA_ROW_INDEX_START) + " Is Empty : The length should be less than 4. Your value is " + cell.StringCellValue));
                            }
                            else
                            {
                                destinationMaster.DESTINATION_CODE = cell.StringCellValue;
                            }
                            break;
                        case 2:
                            destinationMaster.DESTINATION_NAME = cell.StringCellValue;
                            break;
                        case 3:
                            destinationMaster.EXPORT_CODE = cell.StringCellValue;
                            break;
                        case 4:
                            if (cell.NumericCellValue > 20)
                            {
                                errMesgs.Add(string.Format(row.GetCell(x) + " at Row " + (indexRow - DATA_ROW_INDEX_START) + " Is Empty : The length should be less than 20. Your value is " + cell.NumericCellValue.ToString()));
                            }
                            else
                            {
                                destinationMaster.LEAD_TIME = cell.NumericCellValue.ToString();
                            }
                            break;
                        case 5:
                            destinationMaster.E_KANBAN = cell.StringCellValue;
                            break;
                        case 6:
                            destinationMaster.TC_FROM = (cell.DateCellValue).ToString("ddMMyyyy");
                            break;
                        case 7:
                            row = sheet.GetRow(indexRow);
                            if (destinationMaster.TC_FROM != null)
                            {
                                if (DateTime.Compare(row.GetCell(x - 1).DateCellValue, cell.DateCellValue) == 1 || DateTime.Compare(row.GetCell(x - 1).DateCellValue, cell.DateCellValue) == 0)
                                {
                                    destinationMaster.TC_FROM = null;
                                    errMesgs.Add(string.Format(sheet.GetRow(DATA_ROW_INDEX_START).GetCell(x - 1) + " at Row " + (indexRow - DATA_ROW_INDEX_START) + " Is Empty : TC From must be larger than TC To. Your TC_From value is " + row.GetCell(x - 1).DateCellValue.ToString("ddMMyyyy")));
                                }
                            }
                            destinationMaster.TC_TO = (cell.DateCellValue).ToString("ddMMyyyy");
                            break;
                    }
                    if (destinationMaster.TC_FROM != null && destinationMaster.TC_TO != null)
                    {
                        //Checking overlap in database
                        int result = Checking_Overlap(destinationMaster.TC_FROM, destinationMaster.TC_TO);
                        if (result == 1)
                        {
                            errMesgs.Add(string.Format(sheet.GetRow(DATA_ROW_INDEX_START).GetCell(6) + " at Row " + (indexRow - DATA_ROW_INDEX_START) + " Is Empty : Overlap found for T/C From and T/C to in database. Your TC_From value is " + destinationMaster.TC_FROM.ToString()));
                            destinationMaster.TC_FROM = null;

                            errMesgs.Add(string.Format(sheet.GetRow(DATA_ROW_INDEX_START).GetCell(7) + " at Row " + (indexRow - DATA_ROW_INDEX_START) + " Is Empty : Overlap found for T/C From and T/C to in database. Your TC_To value is " + destinationMaster.TC_TO.ToString()));
                            destinationMaster.TC_TO = null;
                        }
                        else
                        {
                            //Checking overlap in current file
                            tc.tcFrom = sheet.GetRow(indexRow).GetCell(6).DateCellValue;
                            tc.tcTo = sheet.GetRow(indexRow).GetCell(7).DateCellValue;

                            bool overlap = DoesNotOverlap(tc, timeControl);
                            if (overlap)
                            {
                                destinationMaster.TC_FROM = null;
                                errMesgs.Add(string.Format(sheet.GetRow(DATA_ROW_INDEX_START).GetCell(6) + " at Row " + (indexRow - DATA_ROW_INDEX_START) + " Is Empty : Overlap found for T/C From and T/C to in the current file. Your value is " + tc.tcFrom.ToString("ddMMyyyy")));

                                destinationMaster.TC_TO = null;
                                errMesgs.Add(string.Format(sheet.GetRow(DATA_ROW_INDEX_START).GetCell(7) + " at Row " + (indexRow - DATA_ROW_INDEX_START) + " Is Empty : Overlap found for T/C From and T/C to in the current file. Your value is " + tc.tcTo.ToString("ddMMyyyy")));
                            }
                        }
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

        public int Checking_Overlap(string tcFrom, string tcTo)
        {
            IDBContext db = databaseManager.GetContext();
            int result = db.SingleOrDefault<int>("DestinationMasterSettingsScreen/CheckingOverlap", new { v_tcFrom = tcFrom, v_tcTo = tcTo });
            return result;
        }

        public static bool DoesNotOverlap(Time_Control tc, IList<Time_Control> timeControl)
        {
            bool result = false;
            IEnumerable<Time_Control> list_tc = null;
            if (timeControl.Count > 0)
            {
                list_tc = timeControl.Where(i => tc.tcFrom <= i.tcTo && tc.tcTo >= i.tcFrom);
                if (list_tc.ToList().Count > 0)
                {
                    result = true;
                }
            }
            timeControl.Add(tc);
            return result;
        }
    }
}