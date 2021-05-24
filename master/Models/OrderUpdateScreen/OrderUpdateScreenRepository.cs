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

namespace GPPSU.Models.OrderUpdateScreen
{
    public class OrderUpdateScreenRepository : GPPSU.Commons.Repositories.BaseRepo
    {
        private static readonly string SHEET_NAME_COEF = "Order Update Screen";

        #region singleton
        private static OrderUpdateScreenRepository instance = null;
        public static OrderUpdateScreenRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderUpdateScreenRepository();
                }
                return instance;
            }
        }
        #endregion singleton

        internal int SearchCount(IDBContext db, OrderUpdateScreen data)
        {
            dynamic args = new
            {
                company_cd = data.COMPANY_CD,
                status_cd = data.STATUS_CD,
                importer_cd = data.IMPORTER_CD,
                exporter_cd = data.EXPORTER_CD,
                order_type = data.ORDER_TYPE,
                pack_month = data.PACK_MONTH,
                cfc = data.CFC,
                re_export_cd = data.RE_EXPORT_CD,
                disable_flag = data.DISABLE_FLAG,

                yymm = data.YYMM
            };

            int result = db.SingleOrDefault<int>("OrderUpdateScreen/OrderUpdateScreen_SearchCount", args);
            return result;
        }

        internal IList<OrderUpdateScreen> Search(IDBContext db, OrderUpdateScreen data, int currentPage, int rowsPerPage)
        {
            dynamic args = new
            {
                company_cd = data.COMPANY_CD,
                status_cd = data.STATUS_CD,
                importer_cd = data.IMPORTER_CD,
                exporter_cd = data.EXPORTER_CD,
                order_type = data.ORDER_TYPE,
                pack_month = data.PACK_MONTH,
                cfc = data.CFC,
                re_export_cd = data.RE_EXPORT_CD,
                disable_flag = data.DISABLE_FLAG,

                yymm = data.YYMM,

                RowStart = currentPage,
                RowEnd = rowsPerPage,

                packmonthplus0 = data.TOTAL_MONTHLY_01,
                packmonthplus1 = data.TOTAL_MONTHLY_02,
                packmonthplus2 = data.TOTAL_MONTHLY_03,
                packmonthplus3 = data.TOTAL_MONTHLY_04

            };

            IList<OrderUpdateScreen> result = db.Fetch<OrderUpdateScreen>("OrderUpdateScreen/OrderUpdateScreen_Search", args);
            return result;
        }

        internal IList<DailyHeaderList> getHeader(IDBContext db, OrderUpdateScreen data)
        {
            dynamic args = new
            {
                yearMonth = data.YYMM
            };

            IList<DailyHeaderList> result = db.Fetch<DailyHeaderList>("OrderUpdateScreen/OrderUpdateScreen_GetDynamicDailyHeader", args);
            return result;
        }

        public RepoResult Create(OrderUpdateScreen data, string userid)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,

                companyCode = data.COMPANY_CD,
                statusCode = data.STATUS_CD,
                importerCode = data.IMPORTER_CD,
                exporterCode = data.EXPORTER_CD,
                orderType = data.ORDER_TYPE,
                packMonth = data.PACK_MONTH,
                cfc = data.CFC,

                UserId = userid
            };

            IDBContext db = DatabaseManager.Instance.GetContext();
            int result = db.Execute("OrderUpdateScreen/OrderUpdateScreen_Create", args);

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

        public RepoResult Prev(OrderUpdateScreen data, string userid)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,

                companyCode = data.COMPANY_CD,
                statusCode = data.STATUS_CD,
                importerCode = data.IMPORTER_CD,
                exporterCode = data.EXPORTER_CD,
                orderType = data.ORDER_TYPE,
                packMonth = data.PACK_MONTH,
                cfc = data.CFC,

                UserId = userid
            };

            IDBContext db = DatabaseManager.Instance.GetContext();
            int result = db.Execute("OrderUpdateScreen/OrderUpdateScreen_Prev", args);

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

        public RepoResult AddSave(OrderUpdateScreen data, string userid)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,

                companyCode = data.COMPANY_CD,
                statusCode = data.STATUS_CD,
                importerCode = data.IMPORTER_CD,
                exporterCode = data.EXPORTER_CD,
                orderType = data.ORDER_TYPE,
                packMonth = data.PACK_MONTH,
                cfc = data.CFC,

                partNo = data.PART_NO,
                reExport = data.RE_EXPORT_CD,
                lotSize = data.LOT_SIZE,
                totalMonthly01 = data.TOTAL_MONTHLY_01,
                totalMonthly02 = data.TOTAL_MONTHLY_02,
                totalMonthly03 = data.TOTAL_MONTHLY_03,
                totalMonthly04 = data.TOTAL_MONTHLY_04,
                dAlocRatio01 = data.DAY_ORD_VOL_01,
                dAlocRatio02 = data.DAY_ORD_VOL_02,
                dAlocRatio03 = data.DAY_ORD_VOL_03,
                dAlocRatio04 = data.DAY_ORD_VOL_04,
                dAlocRatio05 = data.DAY_ORD_VOL_05,
                dAlocRatio06 = data.DAY_ORD_VOL_06,
                dAlocRatio07 = data.DAY_ORD_VOL_07,
                dAlocRatio08 = data.DAY_ORD_VOL_08,
                dAlocRatio09 = data.DAY_ORD_VOL_09,
                dAlocRatio10 = data.DAY_ORD_VOL_10,
                dAlocRatio11 = data.DAY_ORD_VOL_11,
                dAlocRatio12 = data.DAY_ORD_VOL_12,
                dAlocRatio13 = data.DAY_ORD_VOL_13,
                dAlocRatio14 = data.DAY_ORD_VOL_14,
                dAlocRatio15 = data.DAY_ORD_VOL_15,
                dAlocRatio16 = data.DAY_ORD_VOL_16,
                dAlocRatio17 = data.DAY_ORD_VOL_17,
                dAlocRatio18 = data.DAY_ORD_VOL_18,
                dAlocRatio19 = data.DAY_ORD_VOL_19,
                dAlocRatio20 = data.DAY_ORD_VOL_20,
                dAlocRatio21 = data.DAY_ORD_VOL_21,
                dAlocRatio22 = data.DAY_ORD_VOL_22,
                dAlocRatio23 = data.DAY_ORD_VOL_23,
                dAlocRatio24 = data.DAY_ORD_VOL_24,
                dAlocRatio25 = data.DAY_ORD_VOL_25,
                dAlocRatio26 = data.DAY_ORD_VOL_26,
                dAlocRatio27 = data.DAY_ORD_VOL_27,
                dAlocRatio28 = data.DAY_ORD_VOL_28,
                dAlocRatio29 = data.DAY_ORD_VOL_29,
                dAlocRatio30 = data.DAY_ORD_VOL_30,
                dAlocRatio31 = data.DAY_ORD_VOL_31,

                UserId = userid
            };

            IDBContext db = DatabaseManager.Instance.GetContext();
            int result = db.Execute("OrderUpdateScreen/OrderUpdateScreen_Insert", args);

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

        public RepoResult EditSave(OrderUpdateScreen data, string userid)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,

                companyCode = data.COMPANY_CD,
                statusCode = data.STATUS_CD,
                importerCode = data.IMPORTER_CD,
                exporterCode = data.EXPORTER_CD,
                orderType = data.ORDER_TYPE,
                packMonth = data.PACK_MONTH,
                cfc = data.CFC,

                partNo = data.PART_NO,
                reExport = data.RE_EXPORT_CD,
                lotSize = data.LOT_SIZE,
                totalMonthly01 = data.TOTAL_MONTHLY_01,
                totalMonthly02 = data.TOTAL_MONTHLY_02,
                totalMonthly03 = data.TOTAL_MONTHLY_03,
                totalMonthly04 = data.TOTAL_MONTHLY_04,
                dAlocRatio01 = data.DAY_ORD_VOL_01,
                dAlocRatio02 = data.DAY_ORD_VOL_02,
                dAlocRatio03 = data.DAY_ORD_VOL_03,
                dAlocRatio04 = data.DAY_ORD_VOL_04,
                dAlocRatio05 = data.DAY_ORD_VOL_05,
                dAlocRatio06 = data.DAY_ORD_VOL_06,
                dAlocRatio07 = data.DAY_ORD_VOL_07,
                dAlocRatio08 = data.DAY_ORD_VOL_08,
                dAlocRatio09 = data.DAY_ORD_VOL_09,
                dAlocRatio10 = data.DAY_ORD_VOL_10,
                dAlocRatio11 = data.DAY_ORD_VOL_11,
                dAlocRatio12 = data.DAY_ORD_VOL_12,
                dAlocRatio13 = data.DAY_ORD_VOL_13,
                dAlocRatio14 = data.DAY_ORD_VOL_14,
                dAlocRatio15 = data.DAY_ORD_VOL_15,
                dAlocRatio16 = data.DAY_ORD_VOL_16,
                dAlocRatio17 = data.DAY_ORD_VOL_17,
                dAlocRatio18 = data.DAY_ORD_VOL_18,
                dAlocRatio19 = data.DAY_ORD_VOL_19,
                dAlocRatio20 = data.DAY_ORD_VOL_20,
                dAlocRatio21 = data.DAY_ORD_VOL_21,
                dAlocRatio22 = data.DAY_ORD_VOL_22,
                dAlocRatio23 = data.DAY_ORD_VOL_23,
                dAlocRatio24 = data.DAY_ORD_VOL_24,
                dAlocRatio25 = data.DAY_ORD_VOL_25,
                dAlocRatio26 = data.DAY_ORD_VOL_26,
                dAlocRatio27 = data.DAY_ORD_VOL_27,
                dAlocRatio28 = data.DAY_ORD_VOL_28,
                dAlocRatio29 = data.DAY_ORD_VOL_29,
                dAlocRatio30 = data.DAY_ORD_VOL_30,
                dAlocRatio31 = data.DAY_ORD_VOL_31,

                yymm = data.YYMM,
                detailNo = data.DETAIL_NO,
                receiveNo = data.RECEIVE_NO,

                UserId = userid
            };

            IDBContext db = DatabaseManager.Instance.GetContext();
            int result = db.Execute("OrderUpdateScreen/OrderUpdateScreen_Update", args);

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

        public RepoResult UploadSave(OrderUpdateScreen data, string userid)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,

                companyCode = data.COMPANY_CD,
                statusCode = data.STATUS_CD,
                importerCode = data.IMPORTER_CD,
                exporterCode = data.EXPORTER_CD,
                orderType = data.ORDER_TYPE,
                packMonth = data.PACK_MONTH,
                cfc = data.CFC,

                partNo = data.PART_NO,
                reExport = data.RE_EXPORT_CD,
                lotSize = data.LOT_SIZE,
                totalMonthly01 = data.TOTAL_MONTHLY_01,
                totalMonthly02 = data.TOTAL_MONTHLY_02,
                totalMonthly03 = data.TOTAL_MONTHLY_03,
                totalMonthly04 = data.TOTAL_MONTHLY_04,
                dAlocRatio01 = data.DAY_ORD_VOL_01,
                dAlocRatio02 = data.DAY_ORD_VOL_02,
                dAlocRatio03 = data.DAY_ORD_VOL_03,
                dAlocRatio04 = data.DAY_ORD_VOL_04,
                dAlocRatio05 = data.DAY_ORD_VOL_05,
                dAlocRatio06 = data.DAY_ORD_VOL_06,
                dAlocRatio07 = data.DAY_ORD_VOL_07,
                dAlocRatio08 = data.DAY_ORD_VOL_08,
                dAlocRatio09 = data.DAY_ORD_VOL_09,
                dAlocRatio10 = data.DAY_ORD_VOL_10,
                dAlocRatio11 = data.DAY_ORD_VOL_11,
                dAlocRatio12 = data.DAY_ORD_VOL_12,
                dAlocRatio13 = data.DAY_ORD_VOL_13,
                dAlocRatio14 = data.DAY_ORD_VOL_14,
                dAlocRatio15 = data.DAY_ORD_VOL_15,
                dAlocRatio16 = data.DAY_ORD_VOL_16,
                dAlocRatio17 = data.DAY_ORD_VOL_17,
                dAlocRatio18 = data.DAY_ORD_VOL_18,
                dAlocRatio19 = data.DAY_ORD_VOL_19,
                dAlocRatio20 = data.DAY_ORD_VOL_20,
                dAlocRatio21 = data.DAY_ORD_VOL_21,
                dAlocRatio22 = data.DAY_ORD_VOL_22,
                dAlocRatio23 = data.DAY_ORD_VOL_23,
                dAlocRatio24 = data.DAY_ORD_VOL_24,
                dAlocRatio25 = data.DAY_ORD_VOL_25,
                dAlocRatio26 = data.DAY_ORD_VOL_26,
                dAlocRatio27 = data.DAY_ORD_VOL_27,
                dAlocRatio28 = data.DAY_ORD_VOL_28,
                dAlocRatio29 = data.DAY_ORD_VOL_29,
                dAlocRatio30 = data.DAY_ORD_VOL_30,
                dAlocRatio31 = data.DAY_ORD_VOL_31,

                UserId = userid
            };

            IDBContext db = DatabaseManager.Instance.GetContext();
            int result = db.Execute("OrderUpdateScreen/OrderUpdateScreen_Insert", args);

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

        #region Delete
        public GPPSU.Commons.Models.RepoResult Delete(IDBContext db, OrderUpdateScreen data)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");
            SqlParameter outputTblOfOrderUpdateScreen = CreateSqlParameterTblOfOrderUpdateScreen("TableOfOrderUpdateScreen",
                data.listData, "dbo.TableOfOrderUpdateScreen");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                TblOfOrderUpdateScreen = outputTblOfOrderUpdateScreen
            };

            int result = db.Execute("OrderUpdateScreen/OrderUpdateScreen_Delete", args);

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

        private SqlParameter CreateSqlParameterTblOfOrderUpdateScreen(string parameterName, IList<OrderUpdateScreen> listData, string typeName)
        {
            DataTable table = new DataTable();
            table.Columns.Add("COMPANY_CD", type: typeof(string));
            table.Columns.Add("RECEIVE_NO", type: typeof(string));
            table.Columns.Add("DETAIL_NO", type: typeof(string));
            table.Columns.Add("YYMM", type: typeof(string));

            if (listData != null)
            {
                foreach (OrderUpdateScreen data in listData)
                {
                    DataRow row = table.NewRow();
                    row["COMPANY_CD"] = data.COMPANY_CD;
                    row["RECEIVE_NO"] = data.RECEIVE_NO;
                    row["DETAIL_NO"] = data.DETAIL_NO;
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
        public byte[] GenerateDownloadFile(IList<OrderUpdateScreen> orderList)
        {
            byte[] result = null;

            try
            {
                result = CreateFile(orderList);
            }
            finally
            {

            }

            return result;
        }

        private byte[] CreateFile(IList<OrderUpdateScreen> orderList)
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
                cellStyleHeader, cellStyleData, orderList);

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
                                        IList<OrderUpdateScreen> orderList)
        {
            int rowIdx = startRow;
            int itemCount = 0;

            NPOIWriter.CreateMergedColHeader(wb, sheet1, 0, 1, 0, 0, cellStyleHeader, "Parts No");
            NPOIWriter.CreateMergedColHeader(wb, sheet1, 0, 1, 1, 1, cellStyleHeader, "Re Exp");
            NPOIWriter.CreateMergedColHeader(wb, sheet1, 0, 1, 2, 2, cellStyleHeader, "Order Lot Size");
            NPOIWriter.CreateMergedColHeader(wb, sheet1, 0, 0, 3, 6, cellStyleHeader, "Total");
            NPOIWriter.CreateMergedColHeader(wb, sheet1, 0, 0, 7, 37, cellStyleHeader, "Month");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 3, cellStyleHeader, "01");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 4, cellStyleHeader, "02");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 5, cellStyleHeader, "03");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 6, cellStyleHeader, "04");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 7, cellStyleHeader, "Day 1");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 8, cellStyleHeader, "Day 2");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 9, cellStyleHeader, "Day 3");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 10, cellStyleHeader, "Day 4");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 11, cellStyleHeader, "Day 5");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 12, cellStyleHeader, "Day 6");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 13, cellStyleHeader, "Day 7");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 14, cellStyleHeader, "Day 8");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 15, cellStyleHeader, "Day 9");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 16, cellStyleHeader, "Day 10");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 17, cellStyleHeader, "Day 11");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 18, cellStyleHeader, "Day 12");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 19, cellStyleHeader, "Day 13");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 20, cellStyleHeader, "Day 14");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 21, cellStyleHeader, "Day 15");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 22, cellStyleHeader, "Day 16");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 23, cellStyleHeader, "Day 17");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 24, cellStyleHeader, "Day 18");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 25, cellStyleHeader, "Day 19");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 26, cellStyleHeader, "Day 20");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 27, cellStyleHeader, "Day 21");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 28, cellStyleHeader, "Day 22");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 29, cellStyleHeader, "Day 23");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 30, cellStyleHeader, "Day 24");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 31, cellStyleHeader, "Day 25");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 32, cellStyleHeader, "Day 26");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 33, cellStyleHeader, "Day 27");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 34, cellStyleHeader, "Day 28");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 35, cellStyleHeader, "Day 29");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 36, cellStyleHeader, "Day 30");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 1, 37, cellStyleHeader, "Day 31");

            rowIdx = 2;
            foreach (OrderUpdateScreen st in orderList)
            {
                WriteDetailSingleData(wb, cellStyleData, st, sheet1, ++itemCount, rowIdx++,
                    cellStyleData);
            }
        }

        private void WriteDetailSingleData(HSSFWorkbook wb, ICellStyle cellStyle, OrderUpdateScreen data,
                        ISheet sheet1, int rowCount, int rowIndex, ICellStyle cellStyleData)
        {
            IRow row = sheet1.CreateRow(rowIndex);
            int col = 0;

            NPOIWriter.createCellText(row, cellStyle, col++, data.PART_NO);
            NPOIWriter.createCellText(row, cellStyle, col++, data.RE_EXPORT_CD);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.LOT_SIZE);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.TOTAL_MONTHLY_01);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.TOTAL_MONTHLY_02);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.TOTAL_MONTHLY_03);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.TOTAL_MONTHLY_04);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_01);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_02);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_03);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_04);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_05);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_06);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_07);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_08);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_09);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_10);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_11);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_12);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_13);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_14);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_15);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_16);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_17);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_18);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_19);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_20);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_21);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_22);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_23);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_24);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_25);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_26);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_27);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_28);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_29);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_30);
            NPOIWriter.createCellDouble(row, cellStyle, col++, data.DAY_ORD_VOL_31);

            sheet1.AutoSizeColumn(0);
        }
        #endregion
    }
}