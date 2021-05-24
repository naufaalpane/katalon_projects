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

namespace GPPSU.Models.UnitProductionPlanHEIJUNKAMaster
{
    public class UnitProductionPlanHEIJUNKAMasterRepository : GPPSU.Commons.Repositories.BaseRepo
    {
        private static readonly string SHEET_NAME_STOCK_MASTER = "Unit Production Plan HEIJUNKA Master Screen";

        #region singleton
        private static UnitProductionPlanHEIJUNKAMasterRepository instance = null;
        public static UnitProductionPlanHEIJUNKAMasterRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UnitProductionPlanHEIJUNKAMasterRepository();
                }
                return instance;
            }
        }
        #endregion singleton

        #region Search & Count
        public IList<UnitProductionPlanHEIJUNKAMaster> Search(IDBContext db, UnitProductionPlanHEIJUNKAMaster data, long currentPage, long rowsPerPage)
        {
            dynamic args = new
            {
                HeijunkaCd = data.HEIJUNKA_CD,
                LineCd = data.LINE_CD,
                Cfc = data.CFC,
                RowStart = currentPage,
                RowEnd = rowsPerPage
            };

            IList<UnitProductionPlanHEIJUNKAMaster> result = db.Fetch<UnitProductionPlanHEIJUNKAMaster>("UnitProductionPlanHEIJUNKAMaster/UnitProductionPlanHEIJUNKAMaster_Search", args);
            return result;
        }

        public int SearchCount(IDBContext db, UnitProductionPlanHEIJUNKAMaster data)
        {
            dynamic args = new
            {
                HeijunkaCd = data.HEIJUNKA_CD,
                LineCd = data.LINE_CD,
                Cfc = data.CFC
            };

            int result = db.SingleOrDefault<int>("UnitProductionPlanHEIJUNKAMaster/UnitProductionPlanHEIJUNKAMaster_Count", args);
            return result;
        }
        #endregion

        #region Add & Edit Data
        public RepoResult SaveAddEdit(IDBContext db, UnitProductionPlanHEIJUNKAMaster data, string userid, string screenMode, string companyCode)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                companyCode,
                lineCd = data.LINE_CD,
                cfc = data.CFC,
                heijunkaCd = data.HEIJUNKA_CD,
                sumSign = data.SUM_SIGN,
                partNo = screenMode.Equals("Upload") ? data.PART_NO : data.PART_NO1 + data.PART_NO2 + data.PART_NO3,
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                userId = userid
            };

            if (screenMode.Equals("ADD") || screenMode.Equals("Upload"))
            {
                int result = db.Execute("UnitProductionPlanHEIJUNKAMaster/UnitProductionPlanHEIJUNKAMaster_SaveAdd", args);
            }
            else if (screenMode.Equals("EDIT")) {
                int result = db.Execute("UnitProductionPlanHEIJUNKAMaster/UnitProductionPlanHEIJUNKAMaster_SaveEdit", args);
            }
               
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

        #region delete
        public GPPSU.Commons.Models.RepoResult Delete(IDBContext db, UnitProductionPlanHEIJUNKAMaster data)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");
            SqlParameter outputTableOfUnitProductionPlanHEIJUNKAMaster = CreateSqlParameterTableOfUnitProductionPlanHEIJUNKAMaster("TableOfUnitProductionPlanHEIJUNKAMaster", data.listData, "dbo.TableOfUnitProductionPlanHEIJUNKAMaster");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                TableOfUnitProductionPlanHEIJUNKAMaster = outputTableOfUnitProductionPlanHEIJUNKAMaster
            };

            int result = db.Execute("UnitProductionPlanHEIJUNKAMaster/UnitProductionPlanHEIJUNKAMaster_Delete", args);

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

        private SqlParameter CreateSqlParameterTableOfUnitProductionPlanHEIJUNKAMaster(string parameterName, IList<UnitProductionPlanHEIJUNKAMaster> listData, string typeName)
        {
            DataTable table = new DataTable();
            table.Columns.Add("COMPANY_CD", type: typeof(string));
            table.Columns.Add("LINE_CD", type: typeof(string));
            table.Columns.Add("PART_NO", type: typeof(string));
            table.Columns.Add("CFC", type: typeof(string));
            table.Columns.Add("HEIJUNKA_CD", type: typeof(string));

            if (listData != null)
            {
                foreach (UnitProductionPlanHEIJUNKAMaster data in listData)
                {
                    DataRow row = table.NewRow();
                    row["COMPANY_CD"] = data.COMPANY_CD;
                    row["LINE_CD"] = data.LINE_CD;
                    row["PART_NO"] = data.PART_NO;
                    row["CFC"] = data.CFC;
                    row["HEIJUNKA_CD"] = data.HEIJUNKA_CD;
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
        public byte[] GenerateDownloadFile(IList<UnitProductionPlanHEIJUNKAMaster> unitProdHeiStock)
        {
            byte[] result = null;

            try
            {
                result = CreateFile(unitProdHeiStock);
            }
            finally
            {

            }

            return result;
        }

        private byte[] CreateFile(IList<UnitProductionPlanHEIJUNKAMaster> unitProdHeiStock)
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
                NPOIWriter.EscapeSheetName(SHEET_NAME_STOCK_MASTER));
            sheet1.FitToPage = false;

            //Write Header manually
            headers = new Dictionary<string, string>();

            WriteDetail(workBook, sheet1, startRow, cellStyleHeader, cellStyleData, unitProdHeiStock);

            using (MemoryStream buffer = new MemoryStream())
            {
                workBook.Write(buffer);
                result = buffer.GetBuffer();
            }

            workBook = null;
            return result;
        }

        public void WriteDetail(HSSFWorkbook wb, ISheet sheet1, int startRow, ICellStyle cellStyleHeader, ICellStyle cellStyleData, IList<UnitProductionPlanHEIJUNKAMaster> unitProdHeiStock)
        {
            int rowIdx = startRow;
            int itemCount = 0;

            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 0, cellStyleHeader, "Line Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 1, cellStyleHeader, "Car Family Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 2, cellStyleHeader, "Heijunka Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 3, cellStyleHeader, "Summary Sign");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 4, cellStyleHeader, "Part No");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 5, cellStyleHeader, "Created By");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 6, cellStyleHeader, "Created Date");

            rowIdx = 1;
            foreach (UnitProductionPlanHEIJUNKAMaster st in unitProdHeiStock)
            {
                WriteDetailSingleData(wb, cellStyleData, st, sheet1, ++itemCount, rowIdx++,
                    cellStyleData);
            }
        }

        private void WriteDetailSingleData(HSSFWorkbook wb, ICellStyle cellStyle, UnitProductionPlanHEIJUNKAMaster data,
                        ISheet sheet1, int rowCount, int rowIndex, ICellStyle cellStyleData)
        {
            IRow row = sheet1.CreateRow(rowIndex);
            int col = 0;

            NPOIWriter.createCellText(row, cellStyle, col++, data.LINE_CD);
            NPOIWriter.createCellText(row, cellStyle, col++, data.CFC);
            NPOIWriter.createCellText(row, cellStyle, col++, data.HEIJUNKA_CD);
            NPOIWriter.createCellText(row, cellStyle, col++, data.SUM_SIGN);
            NPOIWriter.createCellText(row, cellStyle, col++, data.PART_NO);
            NPOIWriter.createCellText(row, cellStyle, col++, data.CREATED_BY);
            NPOIWriter.createCellText(row, cellStyle, col++, data.CREATED_DT);

            sheet1.AutoSizeColumn(0);
        }
        #endregion

        //LAST LINE
    }
}