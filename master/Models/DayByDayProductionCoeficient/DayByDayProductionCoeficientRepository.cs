using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Toyota.Common.Database;
using GPPSU.Commons.Models;
using System.Data;
using System.Data.SqlClient;
using Toyota.Common.Web.Platform;

using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using GPPSU.Commons.Controllers;
using System.IO;

namespace GPPSU.Models.DayByDayProductionCoeficient
{
    public class DayByDayProductionCoeficientRepository : GPPSU.Commons.Repositories.BaseRepo
    {
        private static readonly string SHEET_NAME_COEF = "Day By Day Production Coeficient";

        #region singleton
        private static DayByDayProductionCoeficientRepository instance = null;
        public static DayByDayProductionCoeficientRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DayByDayProductionCoeficientRepository();
                }
                return instance;
            }
        }
        #endregion singleton

        internal int SearchCount(IDBContext db, DayByDayProductionCoeficient data)
        {
            dynamic args = new
            {
                company_cd = data.COMPANY_CD,
                process_cd = data.PROCESS_CD,
                yearmonth  = data.YYMM
            };

            int result = db.SingleOrDefault<int>("DayByDayProductionCoeficient/DayByDayProductionCoeficient_SearchCount", args);
            return result;
        }

        internal IList<DayByDayProductionCoeficient> Search(IDBContext db, DayByDayProductionCoeficient data, int currentPage, int rowsPerPage)
        {
            dynamic args = new
            {
                company_cd = data.COMPANY_CD,
                process_cd = data.PROCESS_CD,
                yearmonth  = data.YYMM,
                RowStart   = currentPage,
                RowEnd     = rowsPerPage
            };

            IList<DayByDayProductionCoeficient> result = db.Fetch<DayByDayProductionCoeficient>("DayByDayProductionCoeficient/DayByDayProductionCoeficient_Search", args);
            //foreach(DayByDayProductionCoeficient r in result)
            //{
            //    dynamic argRatio = new
            //    {
            //        company_cd = r.COMPANY_CD,
            //        process_cd = r.PROCESS_CD,
            //        yearmonth  = r.YYMM
            //    };
            //    r.DAILYRATIOLIST = db.Fetch<DailyAllocationList>("DayByDayProductionCoeficient/DailyAllocationRatioList", argRatio);
            //}
            return result;
        }

        internal IList<DailyHeaderList> getHeader(IDBContext db, DayByDayProductionCoeficient data)
        {
            dynamic args = new
            {
                yearMonth = data.YYMM
            };

            IList<DailyHeaderList> result = db.Fetch<DailyHeaderList>("DayByDayProductionCoeficient/GetDynamicDailyHeader", args);
            return result;
        }

        #region Add Edit
        public RepoResult AddSave(DayByDayProductionCoeficient data, string userid)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                companyCode = data.COMPANY_CD,
                lineCd = data.LINE_CD,
                workDaySystem = data.WORK_DAY_SYSTEM,
                workDayHand = data.WORK_DAY_HAND,
                processCd = data.PROCESS_CD,
                yymm = data.YYMM,

                dAlocRatio01 = data.DAY_ALOC_RATIO_01,
                dAlocRatio02 = data.DAY_ALOC_RATIO_02,
                dAlocRatio03 = data.DAY_ALOC_RATIO_03,
                dAlocRatio04 = data.DAY_ALOC_RATIO_04,
                dAlocRatio05 = data.DAY_ALOC_RATIO_05,
                dAlocRatio06 = data.DAY_ALOC_RATIO_06,
                dAlocRatio07 = data.DAY_ALOC_RATIO_07,
                dAlocRatio08 = data.DAY_ALOC_RATIO_08,
                dAlocRatio09 = data.DAY_ALOC_RATIO_09,
                dAlocRatio10 = data.DAY_ALOC_RATIO_10,
                dAlocRatio11 = data.DAY_ALOC_RATIO_11,
                dAlocRatio12 = data.DAY_ALOC_RATIO_12,
                dAlocRatio13 = data.DAY_ALOC_RATIO_13,
                dAlocRatio14 = data.DAY_ALOC_RATIO_14,
                dAlocRatio15 = data.DAY_ALOC_RATIO_15,
                dAlocRatio16 = data.DAY_ALOC_RATIO_16,
                dAlocRatio17 = data.DAY_ALOC_RATIO_17,
                dAlocRatio18 = data.DAY_ALOC_RATIO_18,
                dAlocRatio19 = data.DAY_ALOC_RATIO_19,
                dAlocRatio20 = data.DAY_ALOC_RATIO_20,
                dAlocRatio21 = data.DAY_ALOC_RATIO_21,
                dAlocRatio22 = data.DAY_ALOC_RATIO_22,
                dAlocRatio23 = data.DAY_ALOC_RATIO_23,
                dAlocRatio24 = data.DAY_ALOC_RATIO_24,
                dAlocRatio25 = data.DAY_ALOC_RATIO_25,
                dAlocRatio26 = data.DAY_ALOC_RATIO_26,
                dAlocRatio27 = data.DAY_ALOC_RATIO_27,
                dAlocRatio28 = data.DAY_ALOC_RATIO_28,
                dAlocRatio29 = data.DAY_ALOC_RATIO_29,
                dAlocRatio30 = data.DAY_ALOC_RATIO_30,
                dAlocRatio31 = data.DAY_ALOC_RATIO_31,

                UserId = userid
            };

            IDBContext db = DatabaseManager.Instance.GetContext();
            int result = db.Execute("DayByDayProductionCoeficient/DayByDayProductionCoeficient_Insert", args);

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

        public RepoResult EditSave(DayByDayProductionCoeficient data, string userid)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                companyCode = data.COMPANY_CD,
                lineCd = data.LINE_CD,
                workDaySystem = data.WORK_DAY_SYSTEM,
                workDayHand = data.WORK_DAY_HAND,
                processCd = data.PROCESS_CD,
                yymm = data.YYMM,

                dAlocRatio01 = data.DAY_ALOC_RATIO_01,
                dAlocRatio02 = data.DAY_ALOC_RATIO_02,
                dAlocRatio03 = data.DAY_ALOC_RATIO_03,
                dAlocRatio04 = data.DAY_ALOC_RATIO_04,
                dAlocRatio05 = data.DAY_ALOC_RATIO_05,
                dAlocRatio06 = data.DAY_ALOC_RATIO_06,
                dAlocRatio07 = data.DAY_ALOC_RATIO_07,
                dAlocRatio08 = data.DAY_ALOC_RATIO_08,
                dAlocRatio09 = data.DAY_ALOC_RATIO_09,
                dAlocRatio10 = data.DAY_ALOC_RATIO_10,
                dAlocRatio11 = data.DAY_ALOC_RATIO_11,
                dAlocRatio12 = data.DAY_ALOC_RATIO_12,
                dAlocRatio13 = data.DAY_ALOC_RATIO_13,
                dAlocRatio14 = data.DAY_ALOC_RATIO_14,
                dAlocRatio15 = data.DAY_ALOC_RATIO_15,
                dAlocRatio16 = data.DAY_ALOC_RATIO_16,
                dAlocRatio17 = data.DAY_ALOC_RATIO_17,
                dAlocRatio18 = data.DAY_ALOC_RATIO_18,
                dAlocRatio19 = data.DAY_ALOC_RATIO_19,
                dAlocRatio20 = data.DAY_ALOC_RATIO_20,
                dAlocRatio21 = data.DAY_ALOC_RATIO_21,
                dAlocRatio22 = data.DAY_ALOC_RATIO_22,
                dAlocRatio23 = data.DAY_ALOC_RATIO_23,
                dAlocRatio24 = data.DAY_ALOC_RATIO_24,
                dAlocRatio25 = data.DAY_ALOC_RATIO_25,
                dAlocRatio26 = data.DAY_ALOC_RATIO_26,
                dAlocRatio27 = data.DAY_ALOC_RATIO_27,
                dAlocRatio28 = data.DAY_ALOC_RATIO_28,
                dAlocRatio29 = data.DAY_ALOC_RATIO_29,
                dAlocRatio30 = data.DAY_ALOC_RATIO_30,
                dAlocRatio31 = data.DAY_ALOC_RATIO_31,

                UserId = userid
            };

            IDBContext db = DatabaseManager.Instance.GetContext();
            int result = db.Execute("DayByDayProductionCoeficient/DayByDayProductionCoeficient_Update", args);

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

        public RepoResult UploadSave(DayByDayProductionCoeficient data, string userid)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                companyCode = data.COMPANY_CD,
                lineCd = data.LINE_CD,
                workDaySystem = data.D_WORK_DAY_SYSTEM,
                workDayHand = data.D_WORK_DAY_HAND,
                processCd = data.PROCESS_CD,
                yymm = data.YYMM,

                dAlocRatio01 = data.D_DAY_ALOC_RATIO_01,
                dAlocRatio02 = data.D_DAY_ALOC_RATIO_02,
                dAlocRatio03 = data.D_DAY_ALOC_RATIO_03,
                dAlocRatio04 = data.D_DAY_ALOC_RATIO_04,
                dAlocRatio05 = data.D_DAY_ALOC_RATIO_05,
                dAlocRatio06 = data.D_DAY_ALOC_RATIO_06,
                dAlocRatio07 = data.D_DAY_ALOC_RATIO_07,
                dAlocRatio08 = data.D_DAY_ALOC_RATIO_08,
                dAlocRatio09 = data.D_DAY_ALOC_RATIO_09,
                dAlocRatio10 = data.D_DAY_ALOC_RATIO_10,
                dAlocRatio11 = data.D_DAY_ALOC_RATIO_11,
                dAlocRatio12 = data.D_DAY_ALOC_RATIO_12,
                dAlocRatio13 = data.D_DAY_ALOC_RATIO_13,
                dAlocRatio14 = data.D_DAY_ALOC_RATIO_14,
                dAlocRatio15 = data.D_DAY_ALOC_RATIO_15,
                dAlocRatio16 = data.D_DAY_ALOC_RATIO_16,
                dAlocRatio17 = data.D_DAY_ALOC_RATIO_17,
                dAlocRatio18 = data.D_DAY_ALOC_RATIO_18,
                dAlocRatio19 = data.D_DAY_ALOC_RATIO_19,
                dAlocRatio20 = data.D_DAY_ALOC_RATIO_20,
                dAlocRatio21 = data.D_DAY_ALOC_RATIO_21,
                dAlocRatio22 = data.D_DAY_ALOC_RATIO_22,
                dAlocRatio23 = data.D_DAY_ALOC_RATIO_23,
                dAlocRatio24 = data.D_DAY_ALOC_RATIO_24,
                dAlocRatio25 = data.D_DAY_ALOC_RATIO_25,
                dAlocRatio26 = data.D_DAY_ALOC_RATIO_26,
                dAlocRatio27 = data.D_DAY_ALOC_RATIO_27,
                dAlocRatio28 = data.D_DAY_ALOC_RATIO_28,
                dAlocRatio29 = data.D_DAY_ALOC_RATIO_29,
                dAlocRatio30 = data.D_DAY_ALOC_RATIO_30,
                dAlocRatio31 = data.D_DAY_ALOC_RATIO_31,

                UserId = userid
            };

            IDBContext db = DatabaseManager.Instance.GetContext();
            int result = db.Execute("DayByDayProductionCoeficient/DayByDayProductionCoeficient_Insert", args);

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
        public GPPSU.Commons.Models.RepoResult Delete(IDBContext db, DayByDayProductionCoeficient data)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");
            SqlParameter outputTblOfDayByDayProductionCoeficientScreen = CreateSqlParameterTblOfDayByDayProductionCoeficientScreen("TableOfDayByDayProductionCoeficientScreen",
                data.listData, "dbo.TableOfDayByDayProductionCoeficientScreen");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                TblOfDayByDayProductionCoeficientScreen = outputTblOfDayByDayProductionCoeficientScreen
            };

            int result = db.Execute("DayByDayProductionCoeficient/DayByDayProductionCoeficient_Delete", args);

            GPPSU.Commons.Models.RepoResult repoResult = new GPPSU.Commons.Models.RepoResult();
            repoResult.Result = GPPSU.Commons.Models.RepoResult.VALUE_SUCCESS;

            if ((int)outputRetVal.Value != 0)
            {
                repoResult.Result = GPPSU.Commons.Models.RepoResult.VALUE_ERROR;
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

        private SqlParameter CreateSqlParameterTblOfDayByDayProductionCoeficientScreen(string parameterName, IList<DayByDayProductionCoeficient> listData, string typeName)
        {
            DataTable table = new DataTable();
            table.Columns.Add("COMPANY_CD", type: typeof(string));
            table.Columns.Add("LINE_CD", type: typeof(string));
            table.Columns.Add("PROCESS_CD", type: typeof(string));
            table.Columns.Add("YYMM", type: typeof(string));

            if (listData != null)
            {
                foreach (DayByDayProductionCoeficient data in listData)
                {
                    DataRow row = table.NewRow();
                    row["COMPANY_CD"] = data.COMPANY_CD;
                    row["LINE_CD"] = data.LINE_CD;
                    row["PROCESS_CD"] = data.PROCESS_CD;
                    row["YYMM"] = data.YYMM;
                    table.Rows.Add(row);
                }
            }

            var paramStruct = new System.Data.SqlClient.SqlParameter(parameterName, System.Data.SqlDbType.Structured);
            paramStruct.SqlDbType = SqlDbType.Structured;
            paramStruct.SqlValue = table;
            paramStruct.TypeName = typeName;
            return paramStruct;
        }

        #endregion

        #region downloadExcel
        public byte[] GenerateDownloadFile(IList<DayByDayProductionCoeficient> coefList)
        {
            byte[] result = null;

            try
            {
                result = CreateFile(coefList);
            }
            finally
            {

            }

            return result;
        }

        private byte[] CreateFile(IList<DayByDayProductionCoeficient> coefList)
        {
            Dictionary<string, string> headers = null;
            ISheet sheet1 = null;

            HSSFWorkbook workBook = null;
            byte[] result;
            int startRow = 0;

            workBook = new HSSFWorkbook();
            IDataFormat dataFormat = workBook.CreateDataFormat();
            short dateTimeFormat = dataFormat.GetFormat("dd/MM/yyy HH:mm:ss");

            ICellStyle cellStyleData = NPOIWriter.createCellStyleData(workBook, true);
            ICellStyle cellStyleHeader =
                NPOIWriter.createCellStyleColumnHeader(workBook);
            ICellStyle cellStyleDateTime =
                NPOIWriter.createCellStyleDataDate(workBook, dateTimeFormat);

            sheet1 = workBook.CreateSheet(
                NPOIWriter.EscapeSheetName(SHEET_NAME_COEF));
            sheet1.FitToPage = false;

            //Write Header manually
            headers = new Dictionary<string, string>();

            WriteDetail(workBook, sheet1, startRow,
                cellStyleHeader, cellStyleData, coefList);

            using (MemoryStream buffer = new MemoryStream())
            {
                workBook.Write(buffer);
                result = buffer.GetBuffer();
            }

            workBook = null;
            return result;
        }

        public void WriteDetail(HSSFWorkbook wb, ISheet sheet1, int startRow,
                                    ICellStyle cellStyleHeader, ICellStyle cellStyleData,
                                        IList<DayByDayProductionCoeficient> coefList)
        {
            int rowIdx = startRow;
            int itemCount = 0;

            NPOIWriter.CreateMergedColHeader(wb, sheet1, 0, 1, 0, 0, cellStyleHeader, "Line Code");
            NPOIWriter.CreateMergedColHeader(wb, sheet1, 0, 1, 1, 1, cellStyleHeader, "No. Of Working Days");
            NPOIWriter.CreateMergedColHeader(wb, sheet1, 0, 1, 2, 2, cellStyleHeader, "Change No. Of Working Days");
            NPOIWriter.CreateMergedColHeader(wb, sheet1, 0, 0, 3, 33, cellStyleHeader, "Month");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 3, cellStyleHeader, "1");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 4, cellStyleHeader, "2");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 5, cellStyleHeader, "3");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 6, cellStyleHeader, "4");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 7, cellStyleHeader, "5");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 8, cellStyleHeader, "6");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 9, cellStyleHeader, "7");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 10, cellStyleHeader, "8");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 11, cellStyleHeader, "9");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 12, cellStyleHeader, "10");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 13, cellStyleHeader, "11");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 14, cellStyleHeader, "12");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 15, cellStyleHeader, "13");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 16, cellStyleHeader, "14");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 17, cellStyleHeader, "15");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 18, cellStyleHeader, "16");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 19, cellStyleHeader, "17");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 20, cellStyleHeader, "18");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 21, cellStyleHeader, "19");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 22, cellStyleHeader, "20");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 23, cellStyleHeader, "21");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 24, cellStyleHeader, "22");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 25, cellStyleHeader, "23");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 26, cellStyleHeader, "24");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 27, cellStyleHeader, "25");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 28, cellStyleHeader, "26");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 29, cellStyleHeader, "27");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 30, cellStyleHeader, "28");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 31, cellStyleHeader, "29");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 32, cellStyleHeader, "30");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 33, cellStyleHeader, "31");

            rowIdx = 2;
            foreach (DayByDayProductionCoeficient st in coefList)
            {
                WriteDetailSingleData(wb, cellStyleData, st, sheet1, ++itemCount, rowIdx++,
                    cellStyleData);
            }
        }

        private void WriteDetailSingleData(HSSFWorkbook wb, ICellStyle cellStyle, DayByDayProductionCoeficient data,
                        ISheet sheet1, int rowCount, int rowIndex, ICellStyle cellStyleData)
        {
            IRow row = sheet1.CreateRow(rowIndex);
            int col = 0;

            NPOIWriter.createCellText(row, cellStyle, col++, data.LINE_CD);
            NPOIWriter.createCellText(row, cellStyle, col++, data.WORK_DAY_SYSTEM);
            NPOIWriter.createCellText(row, cellStyle, col++, data.WORK_DAY_HAND);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_01);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_02);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_03);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_04);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_05);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_06);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_07);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_08);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_09);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_10);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_11);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_12);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_13);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_14);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_15);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_16);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_17);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_18);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_19);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_20);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_21);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_22);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_23);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_24);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_25);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_26);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_27);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_28);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_29);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_30);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DAY_ALOC_RATIO_31);

            sheet1.AutoSizeColumn(0);
        }
        #endregion
    }
}