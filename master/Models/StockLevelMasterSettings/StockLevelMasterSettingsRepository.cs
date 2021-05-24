using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using GPPSU.Models.Common;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;
using GPPSU.Commons.Models;

using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using GPPSU.Commons.Controllers;
using System.IO;

namespace GPPSU.Models.StockLevelMasterSettings
{
    public class StockLevelMasterSettingsRepository : GPPSU.Commons.Repositories.BaseRepo
    {
        private static readonly string SHEET_NAME_STOCK_MASTER = "Stock Level Master Settings Screen";

        #region singleton
        private static StockLevelMasterSettingsRepository instance = null;
        public static StockLevelMasterSettingsRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StockLevelMasterSettingsRepository();
                }
                return instance;
            }
        }
        #endregion singleton

        #region Search & Count
        public IList<StockLevelMasterSettings> Search(IDBContext db, StockLevelMasterSettings data, long currentPage, long rowsPerPage)
        {
            dynamic args = new
            {
                processCd = data.PROCESS_CD,
                LineCd = data.LINE_CD,
                Cfc = data.CFC,
                PartNo1 = data.PART_NO1,
                PartNo2 = data.PART_NO2,
                PartNo3 = data.PART_NO3,
                StatusCd = data.STATUS_CD,
                RowStart = currentPage,
                RowEnd = rowsPerPage
            };

            IList<StockLevelMasterSettings> result = db.Fetch<StockLevelMasterSettings>("StockLevelMasterSettings/StockLevelMasterSettings_Search", args);
            return result;
        }

        public int SearchCount(IDBContext db, StockLevelMasterSettings data)
        {
            dynamic args = new
            {
                processCd = data.PROCESS_CD,
                LineCd = data.LINE_CD,
                Cfc = data.CFC,
                PartNo1 = data.PART_NO1,
                PartNo2 = data.PART_NO2,
                PartNo3 = data.PART_NO3,
                StatusCd = data.STATUS_CD
            };

            int result = db.SingleOrDefault<int>("StockLevelMasterSettings/StockLevelMasterSettings_Count", args);
            return result;
        }
        #endregion

        #region Add/Edit & GetByKey
        //public LineMasterSettingsScreen GetByKey(IDBContext db, LineMasterSettingsScreen data)
        //{
        //    dynamic args = new
        //    {
        //        LINE_CD = data.LINE_CD,
        //        PROCESS_CD = data.PROCESS_CD
        //    };

        //    LineMasterSettingsScreen result = db.SingleOrDefault<LineMasterSettingsScreen>("LineMasterSettingsScreen/LineMasterSettingsScreen_GetByKey", args);
        //    return result;
        //}
        public RepoResult SaveAdd(IDBContext db, StockLevelMasterSettings data, string userid, string screenMode)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                companyCode = data.COMPANY_CD,
                lineCd = data.LINE_CD,
                cfc = data.CFC,
                partNo = screenMode.Equals("Upload") ? data.PART_NO : data.PART_NO1 + data.PART_NO2 + data.PART_NO3,
                statusCd = data.STATUS_CD,
                exportCd = data.EXPORT_CD,
                partName = data.PART_NAME,
                unitSign = data.UNIT_SIGN,
                minStock = data.MIN_STOCK,
                maxStock = data.MAX_STOCK,
                tcFrom = data.TC_FROM,
                tcTo = data.TC_TO,
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                userId = userid
            };

            int result = db.Execute("StockLevelMasterSettings/StockLevelMasterSettings_SaveAdd", args);

            RepoResult repoResult = new RepoResult();
            repoResult.Result = RepoResult.VALUE_SUCCESS;

            if ((int)outputRetVal.Value != 0)
            {
                repoResult.Result = RepoResult.VALUE_ERROR;
                string errMesg = string.Empty;

                if (outputErrMesg != null && outputErrMesg.Value != null)
                {
                    errMesg = outputErrMesg.Value.ToString();
                }

                repoResult.ErrMesgs = new string[1];
                repoResult.ErrMesgs[0] = errMesg;
            }

            return repoResult;
        }

        public RepoResult SaveEdit(IDBContext db, StockLevelMasterSettings data, string userid, string screenMode)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                companyCode = data.COMPANY_CD,
                lineCd = data.LINE_CD,
                cfc = data.CFC,
                partNo = data.PART_NO1 + data.PART_NO2 + data.PART_NO3,
                statusCd = data.STATUS_CD,
                exportCd = data.EXPORT_CD,
                partName = data.PART_NAME,
                unitSign = data.UNIT_SIGN,
                minStock = data.MIN_STOCK,
                maxStock = data.MAX_STOCK,
                tcFrom = data.TC_FROM,
                tcTo = data.TC_TO,
                seqNo = data.SEQ_NO,
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                userId = userid
            };

            int result = db.Execute("StockLevelMasterSettings/StockLevelMasterSettings_SaveEdit", args);

            RepoResult repoResult = new RepoResult();
            repoResult.Result = RepoResult.VALUE_SUCCESS;

            if ((int)outputRetVal.Value != 0)
            {
                repoResult.Result = RepoResult.VALUE_ERROR;
                string errMesg = string.Empty;

                if (outputErrMesg != null && outputErrMesg.Value != null)
                {
                    errMesg = outputErrMesg.Value.ToString();
                }

                repoResult.ErrMesgs = new string[1];
                repoResult.ErrMesgs[0] = errMesg;
            }

            return repoResult;
        }
        #endregion

        #region Delete
        public RepoResult Delete(IDBContext db, StockLevelMasterSettings data)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                seqNo = data.SEQ_NO
            };

            int result = db.Execute("StockLevelMasterSettings/StockLevelMasterSettings_Delete", args);

            RepoResult repoResult = new RepoResult();
            repoResult.Result = RepoResult.VALUE_SUCCESS;

            if ((int)outputRetVal.Value != 0)
            {
                repoResult.Result = RepoResult.VALUE_ERROR;
                string errMesg = string.Empty;

                if (outputErrMesg != null && outputErrMesg.Value != null)
                {
                    errMesg = outputErrMesg.Value.ToString();
                }

                repoResult.ErrMesgs = new string[1];
                repoResult.ErrMesgs[0] = errMesg;
            }

            return repoResult;
        }
        #endregion

        #region downloadExcel
        public byte[] GenerateDownloadFile(IList<StockLevelMasterSettings> destStock)
        {
            byte[] result = null;

            try
            {
                result = CreateFile(destStock);
            }
            finally
            {

            }

            return result;
        }

        private byte[] CreateFile(IList<StockLevelMasterSettings> destStock)
        {
            Dictionary<string, string> headers = null;
            ISheet sheet1 = null;

            HSSFWorkbook workBook = null;
            byte[] result;
            int startRow = 0;

            workBook = new HSSFWorkbook();
            IDataFormat dataFormat = workBook.CreateDataFormat();
            short dateTimeFormat = dataFormat.GetFormat("dd/MM/yyy HH:mm:ss");

            //ICellStyle cellStyleData = NPOIWriter.createCellStyleData(workBook,true);
            ICellStyle cellStyleData = NPOIWriter.createCellStyleData(workBook, true);
            ICellStyle cellStyleHeader =
                NPOIWriter.createCellStyleColumnHeader(workBook);
            ICellStyle cellStyleDateTime =
                NPOIWriter.createCellStyleDataDate(workBook, dateTimeFormat);

            sheet1 = workBook.CreateSheet(
                NPOIWriter.EscapeSheetName(SHEET_NAME_STOCK_MASTER));
            sheet1.FitToPage = false;

            //Write Header manually
            headers = new Dictionary<string, string>();

            WriteDetail(workBook, sheet1, startRow, cellStyleHeader, cellStyleData, destStock);

            using (MemoryStream buffer = new MemoryStream())
            {
                workBook.Write(buffer);
                result = buffer.GetBuffer();
            }

            workBook = null;
            return result;
        }

        public void WriteDetail(HSSFWorkbook wb, ISheet sheet1, int startRow,ICellStyle cellStyleHeader, ICellStyle cellStyleData,IList<StockLevelMasterSettings> destStock)
        {
            int rowIdx = startRow;
            int itemCount = 0;

            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 0, cellStyleHeader, "Line Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 1, cellStyleHeader, "Car Family Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 2, cellStyleHeader, "Part No");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 3, cellStyleHeader, "Status Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 4, cellStyleHeader, "Export Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 5, cellStyleHeader, "Part Name");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 6, cellStyleHeader, "Unit Sign");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 7, cellStyleHeader, "Min Stock");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 8, cellStyleHeader, "Max Stock");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 9, cellStyleHeader, "Tc From");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 10, cellStyleHeader, "Tc To");

            rowIdx = 1;
            foreach (StockLevelMasterSettings st in destStock)
            {
                WriteDetailSingleData(wb, cellStyleData, st, sheet1, ++itemCount, rowIdx++,
                    cellStyleData);
            }
        }

        private void WriteDetailSingleData(HSSFWorkbook wb, ICellStyle cellStyle, StockLevelMasterSettings data,
                        ISheet sheet1, int rowCount, int rowIndex, ICellStyle cellStyleData)
        {
            IRow row = sheet1.CreateRow(rowIndex);
            int col = 0;

            NPOIWriter.createCellText(row, cellStyle, col++, data.LINE_CD);
            NPOIWriter.createCellText(row, cellStyle, col++, data.CFC);
            NPOIWriter.createCellText(row, cellStyle, col++, data.PART_NO);
            NPOIWriter.createCellText(row, cellStyle, col++, data.STATUS_CD);
            NPOIWriter.createCellText(row, cellStyle, col++, data.EXPORT_CD);
            NPOIWriter.createCellText(row, cellStyle, col++, data.PART_NAME);
            NPOIWriter.createCellText(row, cellStyle, col++, data.UNIT_SIGN);
            NPOIWriter.createCellText(row, cellStyle, col++, data.MIN_STOCK);
            NPOIWriter.createCellText(row, cellStyle, col++, data.MAX_STOCK);
            NPOIWriter.createCellText(row, cellStyle, col++, data.TC_FROM);
            NPOIWriter.createCellText(row, cellStyle, col++, data.TC_TO);

            sheet1.AutoSizeColumn(0);
        }
        #endregion

        // LAST LINE
    }
}