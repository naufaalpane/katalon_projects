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


namespace GPPSU.Models.LineMasterSettingsScreen
{
    public class LineMasterSettingsScreenRepository : GPPSU.Commons.Repositories.BaseRepo
    {

        private static readonly string SHEET_NAME_LINE_MASTER = "Line Master Settings Screen";

        #region singleton
        private static LineMasterSettingsScreenRepository instance = null;
        public static LineMasterSettingsScreenRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LineMasterSettingsScreenRepository();
                }
                return instance;
            }
        }
        #endregion singleton

        #region Search & Count
        public IList<LineMasterSettingsScreen> Search(IDBContext db, LineMasterSettingsScreen data, long currentPage, long rowsPerPage)

        {
            dynamic args = new
            {
                PROCESS_CD = data.PROCESS_CD,
                LINE_CD = data.LINE_CD,
                RowStart = currentPage,
                RowEnd = rowsPerPage
            };

            IList<LineMasterSettingsScreen> result = db.Fetch<LineMasterSettingsScreen>("LineMasterSettingsScreen/LineMasterSettingsScreen_Search", args);
            return result;
        }

        public int SearchCount(IDBContext db, LineMasterSettingsScreen data)
        {
            dynamic args = new
            {
                PROCESS_CD = data.PROCESS_CD,
                LINE_CD = data.LINE_CD
            };

            int result = db.SingleOrDefault<int>("LineMasterSettingsScreen/LineMasterSettingsScreen_SearchCount", args);
            return result;
        }
        #endregion

        #region Add/Edit & GetByKey


        public LineMasterSettingsScreen GetByKey(IDBContext db, LineMasterSettingsScreen data)
        {
            dynamic args = new
            {
                LINE_CD = data.LINE_CD,
                PROCESS_CD = data.PROCESS_CD
            };

            LineMasterSettingsScreen result = db.SingleOrDefault<LineMasterSettingsScreen>("LineMasterSettingsScreen/LineMasterSettingsScreen_GetByKey", args);
            return result;
        }

        public RepoResult SaveAddEdit(LineMasterSettingsScreen data, string userid, string screenMode)
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
                    LINE_CD = data.LINE_CD,
                    TC_FROM = data.TC_FROM,
                    TC_TO = data.TC_TO,
                    LINE_NAME = data.LINE_NAME,
                    PROCESS_CD = data.PROCESS_CD,
                    screenMode = screenMode,
                    UserId = userid
                };

                result = db.Execute("LineMasterSettingsScreen/LineMasterSettingsScreen_InsertUpdate", args);
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

        public GPPSU.Commons.Models.RepoResult Delete(IDBContext db, LineMasterSettingsScreen data)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");
            SqlParameter outputTblOfLineMasterSettingsScreen = CreateSqlParameterTblOfLineMasterSettingsScreen("TableOfLineMasterSettingsScreen",
                data.listData, "dbo.TableOfLineMasterSettingsScreen");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                TableOfLineMasterSettingsScreen = outputTblOfLineMasterSettingsScreen
            };

            int result = db.Execute("LineMasterSettingsScreen/LineMasterSettingsScreen_Delete", args);

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

        private SqlParameter CreateSqlParameterTblOfLineMasterSettingsScreen(string parameterName, IList<LineMasterSettingsScreen> listData, string typeName)
        {
            DataTable table = new DataTable();
            table.Columns.Add("COMPANY_CD", type: typeof(string));
            table.Columns.Add("LINE_CD", type: typeof(string));
            table.Columns.Add("SEQ_NO", type: typeof(string));

            if (listData != null)
            {
                foreach (LineMasterSettingsScreen data in listData)
                {
                    DataRow row = table.NewRow();
                    row["COMPANY_CD"] = data.COMPANY_CD;
                    row["LINE_CD"] = data.LINE_CD;
                    row["SEQ_NO"] = data.SEQ_NO;
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
        public byte[] GenerateDownloadFile(IList<LineMasterSettingsScreen> lineList)
        {
            byte[] result = null;

            try
            {
                result = CreateFile(lineList);
            }
            finally
            {

            }

            return result;
        }

        private byte[] CreateFile(IList<LineMasterSettingsScreen> lineList)
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
                NPOIWriter.EscapeSheetName(SHEET_NAME_LINE_MASTER));
            sheet1.FitToPage = false;

            //Write Header manually
            headers = new Dictionary<string, string>();

            WriteDetail(workBook, sheet1, startRow,
                cellStyleHeader, cellStyleData, lineList);

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
                                        IList<LineMasterSettingsScreen> lineList)
        {
            int rowIdx = startRow;
            int itemCount = 0;

            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 0, cellStyleHeader, "Compant Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 1, cellStyleHeader, "Line Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 2, cellStyleHeader, "Seq No");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 3, cellStyleHeader, "TC From");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 4, cellStyleHeader, "TC To");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 5, cellStyleHeader, "Line Name");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 6, cellStyleHeader, "Process Code");

            rowIdx = 1;
            foreach (LineMasterSettingsScreen st in lineList)
            {
                WriteDetailSingleData(wb, cellStyleData, st, sheet1, ++itemCount, rowIdx++,
                    cellStyleData);
            }
        }

        private void WriteDetailSingleData(HSSFWorkbook wb, ICellStyle cellStyle, LineMasterSettingsScreen data,
                        ISheet sheet1, int rowCount, int rowIndex, ICellStyle cellStyleData)
        {
            IRow row = sheet1.CreateRow(rowIndex);
            int col = 0;

            NPOIWriter.createCellText(row, cellStyle, col++, data.COMPANY_CD);
            NPOIWriter.createCellText(row, cellStyle, col++, data.LINE_CD);
            NPOIWriter.createCellDecimal(row, cellStyle, col++, data.SEQ_NO);
            NPOIWriter.createCellText(row, cellStyle, col++, data.TC_FROM);
            NPOIWriter.createCellText(row, cellStyle, col++, data.TC_TO);
            NPOIWriter.createCellText(row, cellStyle, col++, data.TC_FROM);
            NPOIWriter.createCellText(row, cellStyle, col++, data.TC_TO);

            sheet1.AutoSizeColumn(0);
        }
        #endregion
    }
}