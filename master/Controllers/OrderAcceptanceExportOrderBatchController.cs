using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyota.Common.Database;
using GPPSU.Models.OrderAcceptanceExportOrderBatch;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using GPPSU.Commons.Controllers;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;
using iText.Layout.Borders;
using iText.Kernel.Geom;
using GPPSU.Commons.Models;
using Toyota.Common.Web.Platform;

namespace GPPSU.Controllers
{
    /// <summary>
    /// ////GPPSU-20 DR-01 UISS DESIGN-U01-002 Order acceptance (Export order) Batch - V2.0.xlsx
    /// </summary>
    /// //// public class OrderAcceptanceExportOrderBatch : BaseController
    public class OrderAcceptanceExportOrderBatchController : BaseController
    {
        // OrderAcceptanceExportOrderBatch/DownloadResult

        public DatabaseManager databaseManager = DatabaseManager.Instance;

        public OrderAcceptanceExportOrderBatchRepo ToRepo = OrderAcceptanceExportOrderBatchRepo.Instance;
        protected override void Startup()
        {
            Settings.Title = "U01-002 Order acceptance (Export order) Batch";
        }

        public ActionResult DownloadTemplate()
        {
            string fullName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/Content/Documents", "Template - Export Order.xlsx");

            byte[] fileBytes = GetFile(fullName);
           // return Json("SUKSES");
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Template - Export Order.xlsx");

        }

        public ActionResult UploadFileSave()     //HttpPostedFileBase file, string scrmId, string row
        {
            AjaxResult ajaxResult = new AjaxResult();
            var file = Request.Files[0];
            var supportedTypes = new[] { "xlsx" };
            string savefile = null;
            string filename = null;
            if (file.FileName.EndsWith(".xlsx"))
            {
                string startupPath = AppDomain.CurrentDomain.BaseDirectory;     //C:\\Users\\Whiteopen\\Documents\\GPPS_U_DEV\\
                string downdir = startupPath + "/Content/Documents/Upload";

                if (!Directory.Exists(downdir))
                {
                    Directory.CreateDirectory(downdir);
                }

                filename = System.IO.Path.GetFileName(file.FileName);

                savefile = System.IO.Path.Combine(startupPath + "/Content/Documents/Upload", filename);

                // string resultFilePath = System.IO.Path.Combine("/Content/Documents/Upload", filename);
                // string fullpath = startupPath + resultFilePath;
                file.SaveAs(savefile);
            }
            else
            {
                ajaxResult.Result = ajaxResult.ValueError;
                ajaxResult.ErrMesgs = new string[] { "File must be (.xlsx) format" };
                return Json(ajaxResult);
            }

            ajaxResult.Result = ajaxResult.ValueSuccess;
            ajaxResult.SuccMesgs = new string[] { "Upload Success. It is ready to be executed.", filename };
            return Json(ajaxResult);
        }

        public ActionResult Execute(string Company_CD, string Version, string FileName)     //HttpPostedFileBase file, string scrmId, string row
        {
            AjaxResult ajaxResult = new AjaxResult();
            try
            {
                IList<DataToTempModel> listdataexcel = new List<DataToTempModel>();

                listdataexcel = getLocalUploadExcel(FileName);

                string OCN = listdataexcel[0].IMPORTERDCD + @"/" + listdataexcel[0].EXPORTERCD + @"/" + listdataexcel[0].PCMN + @"/" + listdataexcel[0].ODRTYPE;
                ToRepo.DeleteTemp();

                int NOMOR = 0;
                foreach (DataToTempModel data in listdataexcel)
                {
                    NOMOR++;
                    ToRepo.uploadFile(data, NOMOR);
                }

                string ResultCheck = ToRepo.CheckHeaderInformationAndFormatCheck();

                if (ResultCheck == "ERROR")
                {
                    string res = ToRepo.AssignNewReceive();
                    IDBContext db = databaseManager.GetContext();
                    try
                    {
                        db.BeginTransaction();
                        ToRepo.CreateNewOrderControlTable(GetLoginUserId(), Company_CD, db);
                        db.CommitTransaction();
                    }
                    catch (Exception ae) { db.AbortTransaction(); }
                    List<DataErrorReport> DataError = new List<DataErrorReport>();
                    DataError = ToRepo.GetDataError();
                    OrderErrorListController createPDF = new OrderErrorListController();
                    createPDF.generatePDFError(DataError);
                    //  generatePDFError(DataError);
                    ToRepo.ExecutionResult(Company_CD, "ERR" , GetLoginUserId()); 
                }

                else
                {
                    string res = ToRepo.AssignNewReceive();
                    string res1 = ToRepo.OutputOrder(GetLoginUserId(), Company_CD);
                    IDBContext db = databaseManager.GetContext();
                    try
                    {
                        db.BeginTransaction();
                        ToRepo.CreateNewOrderControlTableNoError(GetLoginUserId(), Company_CD, db);
                        db.CommitTransaction();
                    }
                    catch (Exception ae) { db.AbortTransaction(); }

                    OrderErrorListController createPDF = new OrderErrorListController();
                    createPDF.generatePDFNoError(ToRepo.GetOCNAndDate());

                   // generatePDFNoError(ToRepo.GetOCNAndDate());

                    ToRepo.ExecutionResult(Company_CD, "OK" , GetLoginUserId());
                }

            }
            catch (Exception ex)
            {
                ajaxResult.Result = ajaxResult.ValueError;
                ajaxResult.ErrMesgs = new string[] { ex.Message.ToString() };
                return Json(ajaxResult);
            }

            ajaxResult.Result = ajaxResult.ValueSuccess;
            ajaxResult.SuccMesgs = new string[] { "Execute Success" };
            return Json(ajaxResult);

        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
            { throw new System.IO.IOException(s); }
            fs.Close();
            fs.Dispose();
            return data;
        }

        public ActionResult DownloadResult()
        {
            string fullName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/Content/Documents/PDF", "Template - OrderAcceptanceExportOrderBatch.pdf");

            byte[] fileBytes = GetFile(fullName);
            this.SendDataAsAttachment("Order Error List.pdf", fileBytes);
            System.IO.File.Delete(fullName);
            return Json("SUKSES");
            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Order Error List.pdf");

        }
    
        private IList<DataToTempModel> getLocalUploadExcel(string FileName)
        {
            IList<DataToTempModel> listData = new List<DataToTempModel>();

            IRow row = null;
            ICell cell = null;
            IWorkbook workbook = null;

            string savefile = null;
            string startupPath = AppDomain.CurrentDomain.BaseDirectory;     //C:\\Users\\Whiteopen\\Documents\\GPPS_U_DEV\\
            string resultFilePath = System.IO.Path.Combine(startupPath + "/Content/Documents/Upload", FileName);

            FileStream fs = new FileStream(resultFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            workbook = new XSSFWorkbook(fs);

            ISheet sheet = workbook.GetSheetAt(0);
            int DATA_ROW_START = 2;
            for (int indexRow = DATA_ROW_START; indexRow <= sheet.LastRowNum; indexRow++)
            {
                row = sheet.GetRow(indexRow);

                DataToTempModel data = new DataToTempModel();

                // cell = row.GetCell(0);
                //row.GetCell().StringCellValue;
                //  NumericCellValue
                data.DATAID = row.GetCell(0).CellType == CellType.String ? row.GetCell(0).StringCellValue.ToString() : row.GetCell(0).NumericCellValue.ToString();              //StringCellValue;
                data.VERSION = row.GetCell(1).CellType == CellType.String ? row.GetCell(1).StringCellValue.ToString() : row.GetCell(1).NumericCellValue.ToString();
                data.REVISIONNO = row.GetCell(2).CellType == CellType.String ? row.GetCell(2).StringCellValue.ToString() : row.GetCell(2).NumericCellValue.ToString();
                data.IMPORTERDCD = row.GetCell(3).CellType == CellType.String ? row.GetCell(3).StringCellValue.ToString() : row.GetCell(3).NumericCellValue.ToString();
                data.EXPORTERCD = row.GetCell(4).CellType == CellType.String ? row.GetCell(4).StringCellValue.ToString() : row.GetCell(4).NumericCellValue.ToString();
                data.ODRTYPE = row.GetCell(5).CellType == CellType.String ? row.GetCell(5).StringCellValue.ToString() : row.GetCell(5).NumericCellValue.ToString();
                data.PCMN = row.GetCell(6).CellType == CellType.String ? row.GetCell(6).StringCellValue.ToString() : row.GetCell(6).NumericCellValue.ToString();
                data.CFC = row.GetCell(7).CellType == CellType.String ? row.GetCell(7).StringCellValue.ToString() : row.GetCell(7).NumericCellValue.ToString();
                data.REEXPCD = row.GetCell(8).CellType == CellType.String ? row.GetCell(8).StringCellValue.ToString() : row.GetCell(8).NumericCellValue.ToString();
                data.AICO_CEPT = row.GetCell(9).CellType == CellType.String ? row.GetCell(9).StringCellValue.ToString() : row.GetCell(9).NumericCellValue.ToString();
                data.PXPLAG = row.GetCell(10).CellType == CellType.String ? row.GetCell(10).StringCellValue.ToString() : row.GetCell(10).NumericCellValue.ToString();
                data.IMPORTERNAME = row.GetCell(11).CellType == CellType.String ? row.GetCell(11).StringCellValue.ToString() : row.GetCell(11).NumericCellValue.ToString();
                data.EXPORTERNAME = row.GetCell(12).CellType == CellType.String ? row.GetCell(12).StringCellValue.ToString() : row.GetCell(12).NumericCellValue.ToString();
                data.SSNO = row.GetCell(13).CellType == CellType.String ? row.GetCell(13).StringCellValue.ToString() : row.GetCell(13).NumericCellValue.ToString();
                data.LINECD = row.GetCell(14).CellType == CellType.String ? row.GetCell(14).StringCellValue.ToString() : row.GetCell(14).NumericCellValue.ToString();
                data.PARTNO = row.GetCell(15).CellType == CellType.String ? row.GetCell(15).StringCellValue.ToString() : row.GetCell(15).NumericCellValue.ToString();
                data.Lot_Code = row.GetCell(16).CellType == CellType.String ? row.GetCell(16).StringCellValue.ToString() : row.GetCell(16).NumericCellValue.ToString();
                data.Exterior_Color = row.GetCell(17).CellType == CellType.String ? row.GetCell(17).StringCellValue.ToString() : row.GetCell(17).NumericCellValue.ToString();
                data.Interior_Color = row.GetCell(18).CellType == CellType.String ? row.GetCell(18).StringCellValue.ToString() : row.GetCell(18).NumericCellValue.ToString();
                data.Control_Mode_Code = row.GetCell(19).CellType == CellType.String ? row.GetCell(19).StringCellValue.ToString() : row.GetCell(19).NumericCellValue.ToString();
                data.Display_Mode_Code = row.GetCell(20).CellType == CellType.String ? row.GetCell(20).StringCellValue.ToString() : row.GetCell(20).NumericCellValue.ToString();
                data.ODRLOT = row.GetCell(21).CellType == CellType.String ? row.GetCell(21).StringCellValue.ToString() : row.GetCell(21).NumericCellValue.ToString();
                data.MONTH_ORDER_VOLUME_Nmonth = row.GetCell(22).CellType == CellType.String ? row.GetCell(22).StringCellValue.ToString() : row.GetCell(22).NumericCellValue.ToString();
                data.MONTH_ORDER_VOLUME_Nmonth_PLUS_1 = row.GetCell(23).CellType == CellType.String ? row.GetCell(23).StringCellValue.ToString() : row.GetCell(23).NumericCellValue.ToString();
                data.MONTH_ORDER_VOLUME_Nmonth_PLUS_2 = row.GetCell(24).CellType == CellType.String ? row.GetCell(24).StringCellValue.ToString() : row.GetCell(24).NumericCellValue.ToString();
                data.MONTH_ORDER_VOLUME_Nmonth_PLUS_3 = row.GetCell(25).CellType == CellType.String ? row.GetCell(25).StringCellValue.ToString() : row.GetCell(25).NumericCellValue.ToString();
                data.DAILY_VOLUME_1N = row.GetCell(26).CellType == CellType.String ? row.GetCell(26).StringCellValue.ToString() : row.GetCell(26).NumericCellValue.ToString();
                data.DAILY_VOLUME_2N = row.GetCell(27).CellType == CellType.String ? row.GetCell(27).StringCellValue.ToString() : row.GetCell(27).NumericCellValue.ToString();
                data.DAILY_VOLUME_3N = row.GetCell(28).CellType == CellType.String ? row.GetCell(28).StringCellValue.ToString() : row.GetCell(28).NumericCellValue.ToString();
                data.DAILY_VOLUME_4N = row.GetCell(29).CellType == CellType.String ? row.GetCell(29).StringCellValue.ToString() : row.GetCell(29).NumericCellValue.ToString();
                data.DAILY_VOLUME_5N = row.GetCell(30).CellType == CellType.String ? row.GetCell(30).StringCellValue.ToString() : row.GetCell(30).NumericCellValue.ToString();
                data.DAILY_VOLUME_6N = row.GetCell(31).CellType == CellType.String ? row.GetCell(31).StringCellValue.ToString() : row.GetCell(31).NumericCellValue.ToString();
                data.DAILY_VOLUME_7N = row.GetCell(32).CellType == CellType.String ? row.GetCell(32).StringCellValue.ToString() : row.GetCell(32).NumericCellValue.ToString();
                data.DAILY_VOLUME_8N = row.GetCell(33).CellType == CellType.String ? row.GetCell(33).StringCellValue.ToString() : row.GetCell(33).NumericCellValue.ToString();
                data.DAILY_VOLUME_9N = row.GetCell(34).CellType == CellType.String ? row.GetCell(34).StringCellValue.ToString() : row.GetCell(34).NumericCellValue.ToString();
                data.DAILY_VOLUME_10N = row.GetCell(35).CellType == CellType.String ? row.GetCell(35).StringCellValue.ToString() : row.GetCell(35).NumericCellValue.ToString();
                data.DAILY_VOLUME_11N = row.GetCell(36).CellType == CellType.String ? row.GetCell(36).StringCellValue.ToString() : row.GetCell(36).NumericCellValue.ToString();
                data.DAILY_VOLUME_12N = row.GetCell(37).CellType == CellType.String ? row.GetCell(37).StringCellValue.ToString() : row.GetCell(37).NumericCellValue.ToString();
                data.DAILY_VOLUME_13N = row.GetCell(38).CellType == CellType.String ? row.GetCell(38).StringCellValue.ToString() : row.GetCell(38).NumericCellValue.ToString();
                data.DAILY_VOLUME_14N = row.GetCell(39).CellType == CellType.String ? row.GetCell(39).StringCellValue.ToString() : row.GetCell(39).NumericCellValue.ToString();
                data.DAILY_VOLUME_15N = row.GetCell(40).CellType == CellType.String ? row.GetCell(40).StringCellValue.ToString() : row.GetCell(40).NumericCellValue.ToString();
                data.DAILY_VOLUME_16N = row.GetCell(41).CellType == CellType.String ? row.GetCell(41).StringCellValue.ToString() : row.GetCell(41).NumericCellValue.ToString();
                data.DAILY_VOLUME_17N = row.GetCell(42).CellType == CellType.String ? row.GetCell(42).StringCellValue.ToString() : row.GetCell(42).NumericCellValue.ToString();
                data.DAILY_VOLUME_18N = row.GetCell(43).CellType == CellType.String ? row.GetCell(43).StringCellValue.ToString() : row.GetCell(43).NumericCellValue.ToString();
                data.DAILY_VOLUME_19N = row.GetCell(44).CellType == CellType.String ? row.GetCell(44).StringCellValue.ToString() : row.GetCell(44).NumericCellValue.ToString();
                data.DAILY_VOLUME_20N = row.GetCell(45).CellType == CellType.String ? row.GetCell(45).StringCellValue.ToString() : row.GetCell(45).NumericCellValue.ToString();
                data.DAILY_VOLUME_21N = row.GetCell(46).CellType == CellType.String ? row.GetCell(46).StringCellValue.ToString() : row.GetCell(46).NumericCellValue.ToString();
                data.DAILY_VOLUME_22N = row.GetCell(47).CellType == CellType.String ? row.GetCell(47).StringCellValue.ToString() : row.GetCell(47).NumericCellValue.ToString();
                data.DAILY_VOLUME_23N = row.GetCell(48).CellType == CellType.String ? row.GetCell(48).StringCellValue.ToString() : row.GetCell(48).NumericCellValue.ToString();
                data.DAILY_VOLUME_24N = row.GetCell(49).CellType == CellType.String ? row.GetCell(49).StringCellValue.ToString() : row.GetCell(49).NumericCellValue.ToString();
                data.DAILY_VOLUME_25N = row.GetCell(50).CellType == CellType.String ? row.GetCell(50).StringCellValue.ToString() : row.GetCell(50).NumericCellValue.ToString();
                data.DAILY_VOLUME_26N = row.GetCell(51).CellType == CellType.String ? row.GetCell(51).StringCellValue.ToString() : row.GetCell(51).NumericCellValue.ToString();
                data.DAILY_VOLUME_27N = row.GetCell(52).CellType == CellType.String ? row.GetCell(52).StringCellValue.ToString() : row.GetCell(52).NumericCellValue.ToString();
                data.DAILY_VOLUME_28N = row.GetCell(53).CellType == CellType.String ? row.GetCell(53).StringCellValue.ToString() : row.GetCell(53).NumericCellValue.ToString();
                data.DAILY_VOLUME_29N = row.GetCell(54).CellType == CellType.String ? row.GetCell(54).StringCellValue.ToString() : row.GetCell(54).NumericCellValue.ToString();
                data.DAILY_VOLUME_30N = row.GetCell(55).CellType == CellType.String ? row.GetCell(55).StringCellValue.ToString() : row.GetCell(55).NumericCellValue.ToString();
                data.DAILY_VOLUME_31N = row.GetCell(56).CellType == CellType.String ? row.GetCell(56).StringCellValue.ToString() : row.GetCell(56).NumericCellValue.ToString();
                data.DAILY_VOLUME_1N_PLUS_1 = row.GetCell(57).CellType == CellType.String ? row.GetCell(57).StringCellValue.ToString() : row.GetCell(57).NumericCellValue.ToString();
                data.DAILY_VOLUME_2N_PLUS_1 = row.GetCell(58).CellType == CellType.String ? row.GetCell(58).StringCellValue.ToString() : row.GetCell(58).NumericCellValue.ToString();
                data.DAILY_VOLUME_3N_PLUS_1 = row.GetCell(59).CellType == CellType.String ? row.GetCell(59).StringCellValue.ToString() : row.GetCell(59).NumericCellValue.ToString();
                data.DAILY_VOLUME_4N_PLUS_1 = row.GetCell(60).CellType == CellType.String ? row.GetCell(60).StringCellValue.ToString() : row.GetCell(60).NumericCellValue.ToString();
                data.DAILY_VOLUME_5N_PLUS_1 = row.GetCell(61).CellType == CellType.String ? row.GetCell(61).StringCellValue.ToString() : row.GetCell(61).NumericCellValue.ToString();
                data.DAILY_VOLUME_6N_PLUS_1 = row.GetCell(62).CellType == CellType.String ? row.GetCell(62).StringCellValue.ToString() : row.GetCell(62).NumericCellValue.ToString();
                data.DAILY_VOLUME_7N_PLUS_1 = row.GetCell(63).CellType == CellType.String ? row.GetCell(63).StringCellValue.ToString() : row.GetCell(63).NumericCellValue.ToString();
                data.DAILY_VOLUME_8N_PLUS_1 = row.GetCell(64).CellType == CellType.String ? row.GetCell(64).StringCellValue.ToString() : row.GetCell(64).NumericCellValue.ToString();
                data.DAILY_VOLUME_9N_PLUS_1 = row.GetCell(65).CellType == CellType.String ? row.GetCell(65).StringCellValue.ToString() : row.GetCell(65).NumericCellValue.ToString();
                data.DAILY_VOLUME_10N_PLUS_1 = row.GetCell(66).CellType == CellType.String ? row.GetCell(66).StringCellValue.ToString() : row.GetCell(66).NumericCellValue.ToString();
                data.DAILY_VOLUME_11N_PLUS_1 = row.GetCell(67).CellType == CellType.String ? row.GetCell(67).StringCellValue.ToString() : row.GetCell(67).NumericCellValue.ToString();
                data.DAILY_VOLUME_12N_PLUS_1 = row.GetCell(68).CellType == CellType.String ? row.GetCell(68).StringCellValue.ToString() : row.GetCell(68).NumericCellValue.ToString();
                data.DAILY_VOLUME_13N_PLUS_1 = row.GetCell(69).CellType == CellType.String ? row.GetCell(69).StringCellValue.ToString() : row.GetCell(69).NumericCellValue.ToString();
                data.DAILY_VOLUME_14N_PLUS_1 = row.GetCell(70).CellType == CellType.String ? row.GetCell(70).StringCellValue.ToString() : row.GetCell(70).NumericCellValue.ToString();
                data.DAILY_VOLUME_15N_PLUS_1 = row.GetCell(71).CellType == CellType.String ? row.GetCell(71).StringCellValue.ToString() : row.GetCell(71).NumericCellValue.ToString();
                data.DAILY_VOLUME_16N_PLUS_1 = row.GetCell(72).CellType == CellType.String ? row.GetCell(72).StringCellValue.ToString() : row.GetCell(72).NumericCellValue.ToString();
                data.DAILY_VOLUME_17N_PLUS_1 = row.GetCell(73).CellType == CellType.String ? row.GetCell(73).StringCellValue.ToString() : row.GetCell(73).NumericCellValue.ToString();
                data.DAILY_VOLUME_18N_PLUS_1 = row.GetCell(74).CellType == CellType.String ? row.GetCell(74).StringCellValue.ToString() : row.GetCell(74).NumericCellValue.ToString();
                data.DAILY_VOLUME_19N_PLUS_1 = row.GetCell(75).CellType == CellType.String ? row.GetCell(75).StringCellValue.ToString() : row.GetCell(75).NumericCellValue.ToString();
                data.DAILY_VOLUME_20N_PLUS_1 = row.GetCell(76).CellType == CellType.String ? row.GetCell(76).StringCellValue.ToString() : row.GetCell(76).NumericCellValue.ToString();
                data.DAILY_VOLUME_21N_PLUS_1 = row.GetCell(77).CellType == CellType.String ? row.GetCell(77).StringCellValue.ToString() : row.GetCell(77).NumericCellValue.ToString();
                data.DAILY_VOLUME_22N_PLUS_1 = row.GetCell(78).CellType == CellType.String ? row.GetCell(78).StringCellValue.ToString() : row.GetCell(78).NumericCellValue.ToString();
                data.DAILY_VOLUME_23N_PLUS_1 = row.GetCell(79).CellType == CellType.String ? row.GetCell(79).StringCellValue.ToString() : row.GetCell(79).NumericCellValue.ToString();
                data.DAILY_VOLUME_24N_PLUS_1 = row.GetCell(80).CellType == CellType.String ? row.GetCell(80).StringCellValue.ToString() : row.GetCell(80).NumericCellValue.ToString();
                data.DAILY_VOLUME_25N_PLUS_1 = row.GetCell(81).CellType == CellType.String ? row.GetCell(81).StringCellValue.ToString() : row.GetCell(81).NumericCellValue.ToString();
                data.DAILY_VOLUME_26N_PLUS_1 = row.GetCell(82).CellType == CellType.String ? row.GetCell(82).StringCellValue.ToString() : row.GetCell(82).NumericCellValue.ToString();
                data.DAILY_VOLUME_27N_PLUS_1 = row.GetCell(83).CellType == CellType.String ? row.GetCell(83).StringCellValue.ToString() : row.GetCell(83).NumericCellValue.ToString();
                data.DAILY_VOLUME_28N_PLUS_1 = row.GetCell(84).CellType == CellType.String ? row.GetCell(84).StringCellValue.ToString() : row.GetCell(84).NumericCellValue.ToString();
                data.DAILY_VOLUME_29N_PLUS_1 = row.GetCell(85).CellType == CellType.String ? row.GetCell(85).StringCellValue.ToString() : row.GetCell(85).NumericCellValue.ToString();
                data.DAILY_VOLUME_30N_PLUS_1 = row.GetCell(86).CellType == CellType.String ? row.GetCell(86).StringCellValue.ToString() : row.GetCell(86).NumericCellValue.ToString();
                data.DAILY_VOLUME_31N_PLUS_1 = row.GetCell(87).CellType == CellType.String ? row.GetCell(87).StringCellValue.ToString() : row.GetCell(87).NumericCellValue.ToString();
                data.Transportation_Code = row.GetCell(88).CellType == CellType.String ? row.GetCell(88).StringCellValue.ToString() : row.GetCell(88).NumericCellValue.ToString();
                data.Order_Date = row.GetCell(89).CellType == CellType.String ? row.GetCell(89).StringCellValue.ToString() : row.GetCell(89).NumericCellValue.ToString();
                data.Series = row.GetCell(90).CellType == CellType.String ? row.GetCell(90).StringCellValue.ToString() : row.GetCell(90).NumericCellValue.ToString();
                data.Dummy = row.GetCell(91).CellType == CellType.String ? row.GetCell(91).StringCellValue.ToString() : row.GetCell(91).NumericCellValue.ToString();
                data.Termination_Code = row.GetCell(92).CellType == CellType.String ? row.GetCell(92).StringCellValue.ToString() : row.GetCell(92).NumericCellValue.ToString();


                listData.Add(data);
            }
            return listData;
        }

    }
}