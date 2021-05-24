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



namespace GPPSU.Models.DayByDayDeliveryPackingCoeficient
{
    public class DayByDayDeliveryPackingCoeficientRepository : GPPSU.Commons.Repositories.BaseRepo
    {

        private static readonly string SHEET_NAME = "Day by Day Delivery Packing Coeficient";

        #region singleton
        private static DayByDayDeliveryPackingCoeficientRepository instance = null;
        public static DayByDayDeliveryPackingCoeficientRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DayByDayDeliveryPackingCoeficientRepository();
                }
                return instance;
            }
        }
        #endregion singleton


        #region Search & Count
        internal IList<DayByDayDeliveryPackingCoeficient> Search(IDBContext db, DayByDayDeliveryPackingCoeficient data, long currentPage, long rowsPerPage)

        {
            dynamic args = new
            {
                YYMM = data.YYMM,
                RowStart = currentPage,
                RowEnd = rowsPerPage
            };

            IList<DayByDayDeliveryPackingCoeficient> result = db.Fetch<DayByDayDeliveryPackingCoeficient>("DayByDayDeliveryPackingCoeficient/DayByDayDeliveryPackingCoeficient_Search", args);

            foreach (DayByDayDeliveryPackingCoeficient r in result)
            {
                dynamic argRatio = new
                {
                    company_cd = r.COMPANY_CD,
                    process_cd = r.PROCESS_CD,
                    yearmonth = r.YYMM,
                    dest_cd = r.DEST_CD
                };
                r.DAILYRATIOLIST = db.Fetch<DailyAllocationList>("DayByDayDeliveryPackingCoeficient/DailyAllocationRatioList", argRatio);
            }
            return result;
        }

        internal int SearchCount(IDBContext db, DayByDayDeliveryPackingCoeficient data)
        {
            dynamic args = new
            {
                YYMM = data.YYMM
            };

            int result = db.SingleOrDefault<int>("DayByDayDeliveryPackingCoeficient/DayByDayDeliveryPackingCoeficient_SearchCount", args);
            return result;
        }


        internal IList<DailyHeaderList> getHeader(IDBContext db, DayByDayDeliveryPackingCoeficient data)
        {
            dynamic args = new
            {
                YYMM = data.YYMM
            };

            IList<DailyHeaderList> result = db.Fetch<DailyHeaderList>("DayByDayDeliveryPackingCoeficient/GetDynamicDailyHeader", args);
            return result;
        }

        #endregion

        #region Add / Edit Save

        public RepoResult AddEditSave(DayByDayDeliveryPackingCoeficient data, string userid, string screenMode)
        {

            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");
            GPPSU.Commons.Models.RepoResult repoResult = new GPPSU.Commons.Models.RepoResult();

            int result;
            IDBContext db = DatabaseManager.Instance.GetContext();
            try
            {
                dynamic args = new
                {
                    RetVal = outputRetVal,
                    ErrMesg = outputErrMesg,
                    DEST_CD = data.DEST_CD,
                    PROCESS_CD = data.PROCESS_CD,
                    YYMM = data.YYMM,
                    screenMode = screenMode,



                    DAY_ALOC_RATIO_01 = data.DAY_ALOC_RATIO_01,
                    DAY_ALOC_RATIO_02 = data.DAY_ALOC_RATIO_02,
                    DAY_ALOC_RATIO_03 = data.DAY_ALOC_RATIO_03,
                    DAY_ALOC_RATIO_04 = data.DAY_ALOC_RATIO_04,
                    DAY_ALOC_RATIO_05 = data.DAY_ALOC_RATIO_05,
                    DAY_ALOC_RATIO_06 = data.DAY_ALOC_RATIO_06,
                    DAY_ALOC_RATIO_07 = data.DAY_ALOC_RATIO_07,
                    DAY_ALOC_RATIO_08 = data.DAY_ALOC_RATIO_08,
                    DAY_ALOC_RATIO_09 = data.DAY_ALOC_RATIO_09,
                    DAY_ALOC_RATIO_10 = data.DAY_ALOC_RATIO_10,
                    DAY_ALOC_RATIO_11 = data.DAY_ALOC_RATIO_11,
                    DAY_ALOC_RATIO_12 = data.DAY_ALOC_RATIO_12,
                    DAY_ALOC_RATIO_13 = data.DAY_ALOC_RATIO_13,
                    DAY_ALOC_RATIO_14 = data.DAY_ALOC_RATIO_14,
                    DAY_ALOC_RATIO_15 = data.DAY_ALOC_RATIO_15,
                    DAY_ALOC_RATIO_16 = data.DAY_ALOC_RATIO_16,
                    DAY_ALOC_RATIO_17 = data.DAY_ALOC_RATIO_17,
                    DAY_ALOC_RATIO_18 = data.DAY_ALOC_RATIO_18,
                    DAY_ALOC_RATIO_19 = data.DAY_ALOC_RATIO_19,
                    DAY_ALOC_RATIO_20 = data.DAY_ALOC_RATIO_20,
                    DAY_ALOC_RATIO_21 = data.DAY_ALOC_RATIO_21,
                    DAY_ALOC_RATIO_22 = data.DAY_ALOC_RATIO_22,
                    DAY_ALOC_RATIO_23 = data.DAY_ALOC_RATIO_23,
                    DAY_ALOC_RATIO_24 = data.DAY_ALOC_RATIO_24,
                    DAY_ALOC_RATIO_25 = data.DAY_ALOC_RATIO_25,
                    DAY_ALOC_RATIO_26 = data.DAY_ALOC_RATIO_26,
                    DAY_ALOC_RATIO_27 = data.DAY_ALOC_RATIO_27,
                    DAY_ALOC_RATIO_28 = data.DAY_ALOC_RATIO_28,
                    DAY_ALOC_RATIO_29 = data.DAY_ALOC_RATIO_29,
                    DAY_ALOC_RATIO_30 = data.DAY_ALOC_RATIO_30,
                    DAY_ALOC_RATIO_31 = data.DAY_ALOC_RATIO_31,

                    UserId = userid
                };

                    result = db.Execute("DayByDayDeliveryPackingCoeficient/DayByDayDeliveryPackingCoeficient_InsertUpdate", args);
            }
            finally
            {
                db.Close();
            }


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

        #endregion

        #region Delete
        public GPPSU.Commons.Models.RepoResult Delete(IDBContext db, DayByDayDeliveryPackingCoeficient data)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");
            SqlParameter outputTblOfDayByDayDeliveryPackingCoeficient = CreateSqlParameterTblOfDayByDayDeliveryPackingCoeficient("TableOfDayByDayDeliveryPackingCoeficient",
                data.listData, "dbo.TableOfDayByDayDeliveryPackingCoeficient");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                TableOfDayByDayDeliveryPackingCoeficient = outputTblOfDayByDayDeliveryPackingCoeficient
            };

            int result = db.Execute("DayByDayDeliveryPackingCoeficient/DayByDayDeliveryPackingCoeficient_Delete", args);

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

        private SqlParameter CreateSqlParameterTblOfDayByDayDeliveryPackingCoeficient(string parameterName, IList<DayByDayDeliveryPackingCoeficient> listData, string typeName)
        {
            DataTable table = new DataTable();
            table.Columns.Add("COMPANY_CD", type: typeof(string));
            table.Columns.Add("PROCESS_CD", type: typeof(string));
            table.Columns.Add("DEST_CD", type: typeof(string));
            table.Columns.Add("YYMM", type: typeof(string));

            if (listData != null)
            {
                foreach (DayByDayDeliveryPackingCoeficient data in listData)
                {
                    DataRow row = table.NewRow();
                    row["COMPANY_CD"] = data.COMPANY_CD;
                    row["PROCESS_CD"] = data.PROCESS_CD;
                    row["DEST_CD"] = data.DEST_CD;
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
        public byte[] GenerateDownloadFile(IList<DayByDayDeliveryPackingCoeficient> dayList, DayByDayDeliveryPackingCoeficient yymm)
        {
            byte[] result = null;

            try
            {
                result = CreateFile(dayList,yymm);
            }
            finally
            {

            }

            return result;
        }

        private byte[] CreateFile(IList<DayByDayDeliveryPackingCoeficient> dayList, DayByDayDeliveryPackingCoeficient yymm)
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
                NPOIWriter.EscapeSheetName(SHEET_NAME));
            sheet1.FitToPage = false;

            //Write Header manually
            headers = new Dictionary<string, string>();

            WriteDetail(workBook, sheet1, startRow,
                cellStyleHeader, cellStyleData, dayList,yymm);

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
                                        IList<DayByDayDeliveryPackingCoeficient> dayList, DayByDayDeliveryPackingCoeficient yymm)
        {
            int rowIdx = startRow;
            int itemCount = 0;

            string YearMonth = "" + yymm.YYMM;

            int col = 1;

            int year = Int32.Parse(YearMonth.Substring(0, 4));
            int month = Int32.Parse(YearMonth.Substring(4, 2));

            int days = System.DateTime.DaysInMonth(year, month);


            NPOIWriter.CreateMergedColHeader(wb, sheet1, 0, 1, 0, 0, cellStyleHeader, "Destionation Code");
            NPOIWriter.CreateMergedColHeader(wb, sheet1, 0, 0, 1, days, cellStyleHeader, "Month");
            
            for(int i = 1; i <= days; i++)
            {
                NPOIWriter.CreateSingleColHeader(wb, sheet1, 1,col, cellStyleHeader, ""+i);
                col++;
            }
                



            rowIdx = 2;


            foreach (DayByDayDeliveryPackingCoeficient st in dayList)
            {
                WriteDetailSingleData(wb, cellStyleData, st, sheet1, ++itemCount, rowIdx++,
                    cellStyleData,days);
            }
        }

        private void WriteDetailSingleData(HSSFWorkbook wb, ICellStyle cellStyle, DayByDayDeliveryPackingCoeficient data,
                        ISheet sheet1, int rowCount, int rowIndex, ICellStyle cellStyleData,int days)
        {
            IRow row = sheet1.CreateRow(rowIndex);
            int col = 0;
            NPOIWriter.createCellText(row, cellStyle, col++, data.DEST_CD);
            foreach (DailyAllocationList i in data.DAILYRATIOLIST)
            {
                NPOIWriter.createCellDecimal(row, cellStyle, col++, i.VALUE);
            }
            sheet1.AutoSizeColumn(0);
        }
        #endregion



    }
}
