using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using GPPSU.Commons.Repositories;
using GPPSU.Models.MCapacityMasterSetUp;
using GPPSU.Commons.Models;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using GPPSU.Commons.Controllers;
using System.IO;

namespace GPPSU.Models.MCapacityMasterSetUp
{
    public class MCapacityMasterSetUpRepository : BaseRepo
    {
        private static readonly string SHEET_NAME_CAPACITY_MASTER = "M Capacity Master";

        #region Singleton
        private static MCapacityMasterSetUpRepository instance = null;
        public static MCapacityMasterSetUpRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MCapacityMasterSetUpRepository();
                }
                return instance;
            }
        }

        #endregion

        #region Search
        public IList<MCapacityMasterSetUp> Search(IDBContext db, MCapacityMasterSetUp param, long currentPage, long rowsPerPage)
        {
            dynamic args = new
            {
                proccd = param.PROCESS_CD,
                linecd = param.LINE_CD,
                hjkcd = param.HEIJUNKA_CD,
                RowStart = currentPage,
                RowEnd = rowsPerPage
            };

            IList<MCapacityMasterSetUp> result = db.Fetch<MCapacityMasterSetUp>("MCapacityMasterSetUp/MCapacityMasterSetUp_Search", args);
            return result;
        }
        #endregion

        #region Count
        public int Count(IDBContext db, MCapacityMasterSetUp param)
        {
            dynamic args = new
            {
                proccd = param.PROCESS_CD,
                linecd = param.LINE_CD,
                hjkcd = param.HEIJUNKA_CD

            };

            int result = db.SingleOrDefault<int>("MCapacityMasterSetUp/MCapacityMasterSetUp_SearchCount", args);
            return result;
        }
        #endregion

        #region SaveAddEdit
        public RepoResult SaveAddEdit(MCapacityMasterSetUp data, string userid, string companyCode, string screenMode)
        {
            SqlParameter RetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter ErrMesgs = CreateSqlParameterOutputErrMesg("ErrMesgs");
            
            IDBContext db = DatabaseManager.Instance.GetContext();
            dynamic args = new
            {
                data.PROCESS_CD,
                data.LINE_CD,
                data.SEQ_NO,
                data.HEIJUNKA_CD,
                data.CAPACITY,
                data.TC_FROM,
                data.TC_TO,
                screenMode,
                userid,
                companyCode,
                RetVal,
                ErrMesgs,
            };

            int result = db.Execute("MCapacityMasterSetUp/MCapacityMasterSetUp_SaveAddEdit", args);

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

        public MCapacityMasterSetUp GetByKey(IDBContext db, MCapacityMasterSetUp param)
        {
            dynamic args = new
            {
                param.PROCESS_CD,
                param.LINE_CD,
                param.HEIJUNKA_CD
            };

            MCapacityMasterSetUp result = Db.SingleOrDefault<MCapacityMasterSetUp>("MCapacityMasterSetUp/MCapacityMasterSetUp_GetByKey", args);

            return result;
        }

        #endregion


        #region delete
        public GPPSU.Commons.Models.RepoResult Delete(IDBContext db, MCapacityMasterSetUp data)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");
            SqlParameter outputTblOfMCapacityData = CreateSqlParameterTblOfMCapacityMaster("TableOfMCapacityMaster", data.listData, "dbo.TableOfMCapacityMaster");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                TblOfMCapacityData = outputTblOfMCapacityData
            };

            int result = db.Execute("MCapacityMasterSetUp/MCapacityMasterSetUp_Delete", args);

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

        private SqlParameter CreateSqlParameterTblOfMCapacityMaster(string parameterName, IList<MCapacityMasterSetUp> listData, string typeName)
        {
            DataTable table = new DataTable();
            table.Columns.Add("COMPANY_CD", type: typeof(string));
            table.Columns.Add("LINE_CD", type: typeof(string));
            table.Columns.Add("SEQ_NO", type: typeof(string));

            if (listData != null)
            {
                foreach (MCapacityMasterSetUp data in listData)
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

        #region download
        public byte[] GenerateDownloadFile(IList<MCapacityMasterSetUp> datas)
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

        private byte[] CreateFile(IList<MCapacityMasterSetUp> dataList)
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
                NPOIWriter.EscapeSheetName(SHEET_NAME_CAPACITY_MASTER));
            sheet1.FitToPage = false;

            //Write Header manually
            headers = new Dictionary<string, string>();

            WriteDetail(workBook, sheet1, startRow,
                cellStyleHeader, cellStyleData, dataList);

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
                                IList<MCapacityMasterSetUp> dataList)
        {
            int rowIdx = startRow;
            int itemCount = 0;

            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 0, cellStyleHeader, "Proccess Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 1, cellStyleHeader, "Line Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 2, cellStyleHeader, "Heijunka Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 3, cellStyleHeader, "Capacity");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 4, cellStyleHeader, "Tc From");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 5, cellStyleHeader, "Tc To");

            rowIdx = 1;
            foreach (MCapacityMasterSetUp st in dataList)
            {
                WriteDetailSingleData(wb, cellStyleData, st, sheet1, ++itemCount, rowIdx++,
                    cellStyleData);
            }
        }

        private void WriteDetailSingleData(HSSFWorkbook wb, ICellStyle cellStyle, MCapacityMasterSetUp data,
                        ISheet sheet1, int rowCount, int rowIndex, ICellStyle cellStyleData)
        {
            IRow row = sheet1.CreateRow(rowIndex);
            int col = 0;

            NPOIWriter.createCellText(row, cellStyle, col++, data.PROCESS_CD);
            NPOIWriter.createCellText(row, cellStyle, col++, data.LINE_CD);
            NPOIWriter.createCellText(row, cellStyle, col++, data.HEIJUNKA_CD);
            NPOIWriter.createCellText(row, cellStyle, col++, data.CAPACITY);
            NPOIWriter.createCellText(row, cellStyle, col++, data.TC_FROM);
            NPOIWriter.createCellText(row, cellStyle, col++, data.TC_TO);

            sheet1.AutoSizeColumn(0);
        }
        #endregion


    }
}