using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPPSU.Models.Common;
using GPPSU.Commons.Controllers;
using GPPSU.Commons.Models;
using GPPSU.Models.DeliveryPackingPartsNoM_PxP;

using Toyota.Common.Database;
using Toyota.Common.Web.Platform;
using Toyota.Common.Credential;
using System.IO;
using GPPSU.Commons.Constants;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace GPPSU.Controllers
{
    public class DeliveryPackingPartsNoM_PxPController : BaseController
    {
    
        private DPPNoM_PxPRepo oRepo = DPPNoM_PxPRepo.Instance;
        private DatabaseManager databaseManager = DatabaseManager.Instance;
        public const string downloadFileName = "DeliveryPackinPartNo.xls";
        public const int DATA_ROW_INDEX_START = 4;
        protected override void Startup()
        {
            Settings.Title = "WA0AUC01 - Delivery/Packing Parts No. Master Set Up";
            DeliveryPackingPartsNoM_PxP data = new DeliveryPackingPartsNoM_PxP();
          
            getCmboxData();
            DoSearch(data, 1, 10);
        }

        public ActionResult Search(DeliveryPackingPartsNoM_PxP data, int currentPage, int rowsPerPage)
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

        public void DoSearch(DeliveryPackingPartsNoM_PxP data, int currentPage, int rowsPerPage)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            if (currentPage == 0 && rowsPerPage == 0)
            {
                ViewData["DelPartData"] = null;
                ViewData["Paging"] = null;

            }
            else
            {
                Models.Common.PagingModel paging = new Models.Common.PagingModel(oRepo.SearchCount(db, data), currentPage, rowsPerPage);
                ViewData["paging"] = paging;

                IList<DeliveryPackingPartsNoM_PxP> DelPartData = oRepo.Search(db, data, paging.StartData, paging.EndData);
                ViewData["DelPartData"] = DelPartData;
            }
        }

        public ActionResult ShowInfoMessagesSuccess(List<string> messages)
        {
            ViewData["Messages"] = messages;
            return PartialView("~/Views/Shared/_MessagePanel.cshtml");
        }

        

        #region getComboBoxData
        public void getCmboxData()
        {

            ViewData["ListDest"] = oRepo.GetDestCode("DeliveryPackingPartsNoM_PxP/GetDestCode");
           
        }
        #endregion

        #region SAVEADDEDIT
        public JsonResult AddEditSave(string screenMode, DeliveryPackingPartsNoM_PxP data)
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
                    repoResult = oRepo.AddSave(data, GetLoginUserId(), screenMode);
                }
                else if (screenMode.Equals("Edit"))
                {
                    repoResult = oRepo.EditSave(data, GetLoginUserId(), screenMode);
                }
                else if (screenMode.Equals("Upload"))
                {
                    int i = 0;
                    foreach (DeliveryPackingPartsNoM_PxP list in data.DPPartsNoM_PxPCollection)
                    {
                        repoResult = oRepo.AddSave(list, GetLoginUserId(), screenMode);
                        if (Commons.Models.AjaxResult.VALUE_SUCCESS.Equals(repoResult.Result))
                        {
                            db.CommitTransaction();
                        }
                        else
                        {
                            if (ajaxResult.ErrMesgs == null) ajaxResult.ErrMesgs = new string[data.DPPartsNoM_PxPCollection.Count];
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
        #endregion

        #region delete
        public JsonResult Delete(DeliveryPackingPartsNoM_PxP data)
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

        #region downloadDataToExcel
        public string DownloadFileExcel(DeliveryPackingPartsNoM_PxP dest, int? PageFlag, GPPSU.Commons.Models.BaseModel bm)
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

                IList<DeliveryPackingPartsNoM_PxP> data =
                    oRepo.Search(db, dest, bm.CurrentPage, bm.RowsPerPage);

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

                fileName = string.Format("DeliveryPackingPartsNoM_PxP-{0}.xls",
                    DateTime.Now.ToString("yyyyMMddHHmmss"));
                result = oRepo.GenerateDownloadFile(data);
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
            int lenghtColumn = 10;
            IRow Hrow, title;

            title = sheet.CreateRow(1);
            title.CreateCell(0);
            title.GetCell(0).SetCellValue("GLOBAL PRODUCTION & LOGISTIC SYSTEM TEMPLATE");
            title.GetCell(0).CellStyle = NPOIWriter.createMergedStyleTitle(workBook, true);

            var mergedTitle = new NPOI.SS.Util.CellRangeAddress(1, 1, 0, lenghtColumn);
            sheet.AddMergedRegion(mergedTitle);

      

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
                                Hrow.GetCell(x).SetCellValue("CFC");
                                break;
                            case 3:
                                Hrow.GetCell(x).SetCellValue("Part_No");
                                break;
                            case 4:
                                Hrow.GetCell(x).SetCellValue("Status_Code");
                                break;
                            case 5:
                                Hrow.GetCell(x).SetCellValue("Process_Code");
                                break;
                            case 6:
                                Hrow.GetCell(x).SetCellValue("Part_Name");
                                break;
                            case 7:
                                Hrow.GetCell(x).SetCellValue("LOT_Size");
                                break;
                            case 8:
                                Hrow.GetCell(x).SetCellValue("Sel_Match_Ratio");
                                break;
                            case 9:
                                Hrow.GetCell(x).SetCellValue("TC_From");
                                break;
                            case 10:
                                Hrow.GetCell(x).SetCellValue("TC_To");
                                break;
                        }
                    }
                    else
                    {
                        if (x == 9 || x == 10)
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

            workBook.SetSheetName(0, "TemplateDeliveryPackingPartNo");

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
                IList<DeliveryPackingPartsNoM_PxP> data = getDataLocalUploadExcel(file, errMesg, exception, db);

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

        private IList<DeliveryPackingPartsNoM_PxP> getDataLocalUploadExcel(HttpPostedFileBase file, IList<string> errMesgs, IList<string> exception, IDBContext db)
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
            IList<DeliveryPackingPartsNoM_PxP> listDestination = new List<DeliveryPackingPartsNoM_PxP>();

            ISheet sheet = hssfwb.GetSheetAt(0);
            if (!sheet.SheetName.Equals("TemplateDeliveryPackingPartNo"))
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
                        DeliveryPackingPartsNoM_PxP deliveryPartMaster = new DeliveryPackingPartsNoM_PxP();

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
                            getCell(x, indexRow, cell, row, sheet, errMesgs, exception, deliveryPartMaster, db);
                        }
                        listDestination.Add(deliveryPartMaster);
                    }
                }
            }
            return listDestination;
        }

        private void getCell(int x, int indexRow, ICell cell, IRow row, ISheet sheet, IList<string> errMesgs, IList<string> exception,
            DeliveryPackingPartsNoM_PxP deliveryPartMaster, IDBContext db)
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
                            deliveryPartMaster.DEST_CD = cell.StringCellValue;
                            break;
                        case 2:
                            deliveryPartMaster.CFC = cell.StringCellValue;
                            break;
                        case 3:
                            deliveryPartMaster.PART_NO = cell.StringCellValue;
                            break;
                        case 4:
                            deliveryPartMaster.STATUS_CD = cell.StringCellValue;
                            break;
                        case 5:
                            deliveryPartMaster.PROCESS_CD = cell.StringCellValue;
                            break;
                        case 6:
                            deliveryPartMaster.PART_NAME = cell.StringCellValue;
                            break;
                        case 7:
                            deliveryPartMaster.LOT_SIZE = cell.StringCellValue;
                            break;
                        case 8:
                            deliveryPartMaster.SEL_MATCH_RATIO = cell.StringCellValue;
                            break;
                        case 9:
                            deliveryPartMaster.TC_FROM = (cell.DateCellValue).ToString("ddMMyyyy");
                            break;
                        case 10:
                            deliveryPartMaster.TC_TO = (cell.DateCellValue).ToString("ddMMyyyy");
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