using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using GPPSU.Commons.Repositories;
using GPPSU.Commons.Models;
using GPPSU.Commons.Controllers;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace GPPSU.Models.UnitProductionControlMasterSettings
{
    public class UnitProductionControlMasterSettingsRepository : BaseRepo
    {
        private static readonly string SHEET_NAME_UNIT_PRODUCTION_CONTROL_MASTER = "Unit Production Control Master";

        #region Singleton
        private static UnitProductionControlMasterSettingsRepository instance = null;
        public static UnitProductionControlMasterSettingsRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UnitProductionControlMasterSettingsRepository();
                }
                return instance;
            }
        }
        #endregion singleton

        #region Search
        public IList<UnitProductionControlMasterSettings> Search(IDBContext db, UnitProductionControlMasterSettings data, long currentPage, long rowsPerPage)
        {
            dynamic args = new
            {
                cfc = data.CFC,
                partsno = data.PART_NO,
                partsno2 = data.PART_NO2,
                partsno3 = data.PART_NO3,
                linecd = data.LINE_CD,
                RowStart = currentPage,
                RowEnd = rowsPerPage
            };

            IList<UnitProductionControlMasterSettings> result = db.Fetch<UnitProductionControlMasterSettings>("UnitProductionControlMasterSettings/UnitProductionControlMasterSettings_Search", args);
            return result;
        }

        public int SearchCount(IDBContext db, UnitProductionControlMasterSettings data)
        {
            dynamic args = new
            {
                cfc = data.CFC,
                partsno = data.PART_NO,
                partsno2 = data.PART_NO2,
                partsno3 = data.PART_NO3,
                linecd = data.LINE_CD
            };

            int result = db.SingleOrDefault<int>("UnitProductionControlMasterSettings/UnitProductionControlMasterSettings_SearchCount", args);
            return result;
        }

        #endregion

        #region SaveAddEdit
        public RepoResult SaveAddEdit(UnitProductionControlMasterSettings data, string userid, string screenMode/*, string companyCode*/)
        {
            SqlParameter RetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter ErrMesgs = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                data.CFC,
                data.PART_NO,
                data.PART_NO2,
                data.PART_NO3,
                data.LINE_CD,
                data.STATUS_CD,
                data.PART_NAME,
                data.UNIT_SIGN,
                data.TC_FROM,
                data.TC_TO,
                data.SEQ_NO,
                //companyCode,
                screenMode,
                userid,
                RetVal,
                ErrMesgs

            };

            IDBContext db = DatabaseManager.Instance.GetContext();
            int result = db.Execute("UnitProductionControlMasterSettings/UnitProductionControlMasterSettings_SaveAddEdit", args);

            RepoResult repoResult = new RepoResult();
            repoResult.Result = RepoResult.VALUE_SUCCESS;

            if ((int)RetVal.Value != 0)
            {
                repoResult.Result = RepoResult.VALUE_ERROR;
                string errMesg = string.Empty;

                if (ErrMesgs != null && ErrMesgs.Value != null)
                {
                    errMesg = ErrMesgs.Value.ToString();
                }

                repoResult.ErrMesgs = new string[1];
                repoResult.ErrMesgs[0] = errMesg;
            }

            return repoResult;
        }
        #endregion

        #region Delete
        public GPPSU.Commons.Models.RepoResult Delete(IDBContext db, UnitProductionControlMasterSettings data)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");
            SqlParameter outputTblOfOfUnitProdControlMaster = CreateSqlParameterTblOfUnitProdControlMaster("TableOfUnitProdControlMaster",
                                                                            data.listData, "dbo.TableOfUnitProdControlMaster");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                TableOfUnitProdControlMaster = outputTblOfOfUnitProdControlMaster
            };

            int result = db.Execute("UnitProductionControlMasterSettings/UnitProductionControlMasterSettings_Delete", args);

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

        private SqlParameter CreateSqlParameterTblOfUnitProdControlMaster(string parameterName, IList<UnitProductionControlMasterSettings> listData, string typeName)
        {
            DataTable table = new DataTable();
            table.Columns.Add("COMPANY_CD", type: typeof(string));
            table.Columns.Add("CFC", type: typeof(string));
            table.Columns.Add("PART_NO", type: typeof(string));
            table.Columns.Add("LINE_CD", type: typeof(string));
            table.Columns.Add("SEQ_NO", type: typeof(string));

            if (listData != null)
            {
                foreach (UnitProductionControlMasterSettings data in listData)
                {
                    DataRow row = table.NewRow();
                    row["COMPANY_CD"] = data.COMPANY_CD;
                    row["CFC"]        = data.CFC;
                    row["PART_NO"]    = data.PART_NO;
                    row["LINE_CD"]    = data.LINE_CD;
                    row["SEQ_NO"]     = data.SEQ_NO;
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

        #region Download Data to Excell
        public byte[] GenerateDownloadFile(IList<UnitProductionControlMasterSettings> datas)
        {
            byte[] result = null;

            try
            {
                result = CreateFile(datas);
            }
            finally
            {

            }

            return result;
        }

        private byte[] CreateFile(IList<UnitProductionControlMasterSettings> dataList)
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
            ICellStyle cellStyleHeader = NPOIWriter.createCellStyleColumnHeader(workBook);
            ICellStyle cellStyleDateTime = NPOIWriter.createCellStyleDataDate(workBook, dateTimeFormat);

            sheet1 = workBook.CreateSheet(NPOIWriter.EscapeSheetName(SHEET_NAME_UNIT_PRODUCTION_CONTROL_MASTER));
            sheet1.FitToPage = false;

            //Write Header manually
            headers = new Dictionary<string, string>();

            WriteDetail(workBook, sheet1, startRow, cellStyleHeader, cellStyleData, dataList);

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
                                IList<UnitProductionControlMasterSettings> dataList)
        {
            int rowIdx = startRow;
            int itemCount = 0;

            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 0, cellStyleHeader, "Car Family Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 1, cellStyleHeader, "Parts No.");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 2, cellStyleHeader, "Line Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 3, cellStyleHeader, "Status Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 4, cellStyleHeader, "Parts Name");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 5, cellStyleHeader, "Unit Sign");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 6, cellStyleHeader, "TC From");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 7, cellStyleHeader, "TC To");


            rowIdx = 1;
            foreach (UnitProductionControlMasterSettings st in dataList)
            {
                WriteDetailSingleData(wb, cellStyleData, st, sheet1, ++itemCount, rowIdx++,
                    cellStyleData);
            }
        }

        private void WriteDetailSingleData(HSSFWorkbook wb, ICellStyle cellStyle, UnitProductionControlMasterSettings data,
                        ISheet sheet1, int rowCount, int rowIndex, ICellStyle cellStyleData)
        {
            IRow row = sheet1.CreateRow(rowIndex);
            int col = 0;

            NPOIWriter.createCellText(row, cellStyle, col++, data.CFC);
            NPOIWriter.createCellText(row, cellStyle, col++, data.PART_NO);
            NPOIWriter.createCellText(row, cellStyle, col++, data.LINE_CD);
            NPOIWriter.createCellText(row, cellStyle, col++, data.STATUS_CD);
            NPOIWriter.createCellText(row, cellStyle, col++, data.PART_NAME);
            NPOIWriter.createCellText(row, cellStyle, col++, data.UNIT_SIGN);
            NPOIWriter.createCellText(row, cellStyle, col++, data.TC_FROM);
            NPOIWriter.createCellText(row, cellStyle, col++, data.TC_TO);

            sheet1.AutoSizeColumn(0);
        }

        #endregion

    }
}