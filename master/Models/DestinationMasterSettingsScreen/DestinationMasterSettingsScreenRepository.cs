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


namespace GPPSU.Models.DestinationMasterSettingsScreen
{
    public class DestinationMasterSettingsScreenRepository : GPPSU.Commons.Repositories.BaseRepo
    {
        private static readonly string SHEET_NAME_DESTINATION_MASTER = "Destination Master Settings Screen";

        #region singleton
        private static DestinationMasterSettingsScreenRepository instance = null;
        public static DestinationMasterSettingsScreenRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DestinationMasterSettingsScreenRepository();
                }
                return instance;
            }
        }
        #endregion singleton

        #region Search & Count
        public IList<DestinationMasterSettingsScreen> Search(IDBContext db, DestinationMasterSettingsScreen data, long currentPage, long rowsPerPage)

        {
            dynamic args = new
            {
                DestCd = data.DESTINATION_CODE,
                RowStart = currentPage,
                RowEnd = rowsPerPage
            };

            IList<DestinationMasterSettingsScreen> result = db.Fetch<DestinationMasterSettingsScreen>("DestinationMasterSettingsScreen/DestinationMasterSettingsScreen_Search", args);
            return result;
        }

        public int SearchCount(IDBContext db, DestinationMasterSettingsScreen data)
        {
            dynamic args = new
            {
                DestCd = data.DESTINATION_CODE
            };

            int result = db.SingleOrDefault<int>("DestinationMasterSettingsScreen/DestinationMasterSettingsScreen_SearchCount", args);
            return result;
        }
        #endregion

        #region Add Edit
        public RepoResult AddSave(DestinationMasterSettingsScreen data, string userid, string screenMode)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                companyCode = data.COMPANY_CD,
                seqNo = data.SEQ_NO,
                destinationCode = data.DESTINATION_CODE,
                destinationName = data.DESTINATION_NAME,
                exportCode = data.EXPORT_CODE,
                leadTime = data.LEAD_TIME,
                eKanban = data.E_KANBAN,
                tcFrom = data.TC_FROM,
                tcTo = data.TC_TO,
                screenMode = screenMode,
                UserId = userid
            };

            IDBContext db = DatabaseManager.Instance.GetContext();
            int result = db.Execute("DestinationMasterSettingsScreen/DestinationMasterSettingsScreen_InsertUpdate", args);

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

        public RepoResult EditSave(DestinationMasterSettingsScreen data, string userid, string screenMode)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                companyCode = data.COMPANY_CD,
                seqNo = data.SEQ_NO,
                destinationCode = data.DESTINATION_CODE,
                destinationName = data.DESTINATION_NAME,
                exportCode = data.EXPORT_CODE,
                leadTime = data.LEAD_TIME,
                eKanban = data.E_KANBAN,
                tcFrom = data.TC_FROM,
                tcTo = data.TC_TO,
                screenMode = screenMode,
                UserId = userid
            };

            IDBContext db = DatabaseManager.Instance.GetContext();
            int result = db.Execute("DestinationMasterSettingsScreen/DestinationMasterSettingsScreen_InsertUpdate", args);

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
        public GPPSU.Commons.Models.RepoResult Delete(IDBContext db, DestinationMasterSettingsScreen data)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");
            SqlParameter outputTblOfDestinationGroupMasterSettingsScreen = CreateSqlParameterTblOfDestinationMasterSettingsScreen("TableOfDestinationGroupMasterSettingsScreen", 
                data.listData, "dbo.TableOfDestinationGroupMasterSettingsScreen");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                TblOfDestinationGroupMasterSettingsScreen = outputTblOfDestinationGroupMasterSettingsScreen
            };

            int result = db.Execute("DestinationMasterSettingsScreen/DestinationMasterSettingsScreen_Delete", args);

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

        private SqlParameter CreateSqlParameterTblOfDestinationMasterSettingsScreen(string parameterName, IList<DestinationMasterSettingsScreen> listData, string typeName)
        {
            DataTable table = new DataTable();
            table.Columns.Add("COMPANY_CD", type: typeof(string));
            table.Columns.Add("DEST_CD", type: typeof(string));
            table.Columns.Add("SEQ_NO", type: typeof(string));

            if (listData != null)
            {
                foreach (DestinationMasterSettingsScreen data in listData)
                {
                    DataRow row = table.NewRow();
                    row["COMPANY_CD"] = data.COMPANY_CD;
                    row["DEST_CD"] = data.DESTINATION_CODE;
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
        public byte[] GenerateDownloadFile(IList<DestinationMasterSettingsScreen> destList)
        {
            byte[] result = null;

            try
            {
                result = CreateFile(destList);
            }
            finally
            {

            }

            return result;
        }

        private byte[] CreateFile(IList<DestinationMasterSettingsScreen> destList)
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
                NPOIWriter.EscapeSheetName(SHEET_NAME_DESTINATION_MASTER));
            sheet1.FitToPage = false;

            //Write Header manually
            headers = new Dictionary<string, string>();

            WriteDetail(workBook, sheet1, startRow,
                cellStyleHeader, cellStyleData, destList);

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
                                        IList<DestinationMasterSettingsScreen> destList)
        {
            int rowIdx = startRow;
            int itemCount = 0;

            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 0, cellStyleHeader, "Destination Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 1, cellStyleHeader, "Destination Name");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 2, cellStyleHeader, "Export Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 3, cellStyleHeader, "Lead Time");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 4, cellStyleHeader, "E-Kanban");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 5, cellStyleHeader, "Tc From");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 6, cellStyleHeader, "Tc To");

            rowIdx = 1;
            foreach (DestinationMasterSettingsScreen st in destList)
            {
                WriteDetailSingleData(wb, cellStyleData, st, sheet1, ++itemCount, rowIdx++,
                    cellStyleData);
            }
        }

        private void WriteDetailSingleData(HSSFWorkbook wb, ICellStyle cellStyle, DestinationMasterSettingsScreen data,
                        ISheet sheet1, int rowCount, int rowIndex, ICellStyle cellStyleData)
        {
            IRow row = sheet1.CreateRow(rowIndex);
            int col = 0;

            NPOIWriter.createCellText(row, cellStyle, col++, data.DESTINATION_CODE);
            NPOIWriter.createCellText(row, cellStyle, col++, data.DESTINATION_NAME);
            NPOIWriter.createCellText(row, cellStyle, col++, data.EXPORT_CODE);
            NPOIWriter.createCellText(row, cellStyle, col++, data.LEAD_TIME);
            NPOIWriter.createCellText(row, cellStyle, col++, data.E_KANBAN);
            NPOIWriter.createCellText(row, cellStyle, col++, data.TC_FROM);
            NPOIWriter.createCellText(row, cellStyle, col++, data.TC_TO);

            sheet1.AutoSizeColumn(0);
        }
        #endregion


    }
}