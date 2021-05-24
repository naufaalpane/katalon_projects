using GPPSU.Commons.Controllers;
using GPPSU.Commons.Models;
using GPPSU.Commons.Repositories;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;

namespace GPPSU.Models.DeliveryPackingPartsNoM_PxP
{
    public class DPPNoM_PxPRepo : BaseRepo
    {
       
        private static readonly string SHEET_NAME_DlV_PART_MASTER = "Delivert part master sccreen";
        #region singleton
        private static DPPNoM_PxPRepo instance = null;
        public static DPPNoM_PxPRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DPPNoM_PxPRepo();
                }
                return instance;
            }
        }
        #endregion singleton


       public IList<DeliveryPackingPartsNoM_PxP> Search(IDBContext db, DeliveryPackingPartsNoM_PxP listParam, long currentPage, long rowsPerPage)

        {

            dynamic args = new
                {
                DEST_CD = listParam.DEST_CD,
                CFC = listParam.CFC,
                PART_NO = listParam.PART_NO,
                PART_NO_ONE = listParam.PART_NO_ONE,
                PART_NO_TWO = listParam.PART_NO_TWO,
                PART_NO_THREE = listParam.PART_NO_THREE,
                STATUS_CD = listParam.STATUS_CD,
                    PROCESS_CD = listParam.PROCESS_CD,
                    PART_NAME = listParam.PART_NAME,
                    LOT_SIZE = listParam.LOT_SIZE,
                    SEL_MATCH_RATIO = listParam.SEL_MATCH_RATIO,
                    TC_FROM = listParam.TC_FROM != null ? Convert.ToDateTime(listParam.TC_FROM).ToString("yyyy-MM-dd") : null,
                    TC_TO = listParam.TC_TO != null ? Convert.ToDateTime(listParam.TC_TO).ToString("yyyy-MM-dd") : null,

                    PageNumber = currentPage,
                    PageSize = rowsPerPage
                // CREATED_BY = listParam.CREATED_BY
                //CREATED_DT = listParam.CREATED_DT != null? Convert.ToDateTime(listParam.CREATED_DT).ToString("yyyy-MM-dd") : null
            };
      
            IList<DeliveryPackingPartsNoM_PxP> result = db.Fetch<DeliveryPackingPartsNoM_PxP>("DeliveryPackingPartsNoM_PxP/SearchData", args);
            return result;

        }

        public int SearchCount(IDBContext db, DeliveryPackingPartsNoM_PxP listParam)

        {
                dynamic args = new
                {
                    DEST_CD = listParam.DEST_CD,
                    CFC = listParam.CFC,
                
                    PART_NO = listParam.PART_NO,
                    PART_NO_ONE = listParam.PART_NO_ONE,
                    PART_NO_TWO = listParam.PART_NO_TWO,
                    PART_NO_THREE = listParam.PART_NO_THREE,
                    listParam.STATUS_CD,
                    listParam.PROCESS_CD,
                    listParam.PART_NAME,
                    listParam.LOT_SIZE,
                    listParam.SEL_MATCH_RATIO,
                     TC_FROM = listParam.TC_FROM != null ? Convert.ToDateTime(listParam.TC_FROM).ToString("yyyy-MM-dd") : null,
                    TC_TO = listParam.TC_TO != null ? Convert.ToDateTime(listParam.TC_TO).ToString("yyyy-MM-dd") : null,
                    listParam.CREATED_BY,
                    CREATED_DT = listParam.CREATED_DT != null ? Convert.ToDateTime(listParam.CREATED_DT).ToString("yyyy-MM-dd") : null,
             
                };
                int result = db.SingleOrDefault<int>("DeliveryPackingPartsNoM_PxP/DPPNOM_getCount", args);
                return result;

            }
        #region getComboboxData
        public IList<DeliveryPackingPartsNoM_PxP> GetDestCode(string destCd)
        {
            IList<DeliveryPackingPartsNoM_PxP> result = Db.Fetch<DeliveryPackingPartsNoM_PxP>(destCd);

            return result;
        }
        #endregion

    
        #region Add Edit
        public RepoResult AddSave(DeliveryPackingPartsNoM_PxP param, string userid, string screenMode)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                DEST_CD = param.DEST_CD,
                CFC = param.CFC,
                PART_NO = param.PART_NO,
                PART_NO_ONE = param.PART_NO_ONE,
                PART_NO_TWO = param.PART_NO_TWO,
                PART_NO_THREE = param.PART_NO_THREE,
                STATUS_CD = param.STATUS_CD,
                PROCESS_CD = param.PROCESS_CD,
                PART_NAME = param.PART_NAME,
                LOT_SIZE = param.LOT_SIZE,
                SEL_MATCH_RATIO = param.SEL_MATCH_RATIO,
                TC_FROM = param.TC_FROM,
                TC_TO = param.TC_TO,
                userId = userid,
                screenMode = screenMode
            };

            IDBContext db = DatabaseManager.Instance.GetContext();
            int result = db.Execute("DeliveryPackingPartsNoM_PxP/DPPNOM_InsertUpdate", args);

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

        public RepoResult EditSave(DeliveryPackingPartsNoM_PxP param, string userid, string screenMode)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                DEST_CD = param.DEST_CD,
                CFC = param.CFC,
                PART_NO = param.PART_NO,
                PART_NO_ONE = param.PART_NO_ONE,
                PART_NO_TWO = param.PART_NO_TWO,
                PART_NO_THREE = param.PART_NO_THREE,
                STATUS_CD = param.STATUS_CD,
                PROCESS_CD = param.PROCESS_CD,
                PART_NAME = param.PART_NAME,
                LOT_SIZE = param.LOT_SIZE,
                SEL_MATCH_RATIO = param.SEL_MATCH_RATIO,
                TC_FROM = param.TC_FROM,
                TC_TO = param.TC_TO,
                userId = userid,
                screenMode = screenMode
            };

            IDBContext db = DatabaseManager.Instance.GetContext();
            int result = db.Execute("DeliveryPackingPartsNoM_PxP/DPPNOM_InsertUpdate", args);

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
        public GPPSU.Commons.Models.RepoResult Delete(IDBContext db, DeliveryPackingPartsNoM_PxP data)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");
            SqlParameter listDlvPart = CreateSqlParameterTblOfDeliveryPackingPartsNoM_PxP("ListDlvPart",
             data.DPPartsNoM_PxPCollection , "dbo.TableOfDeliveryPackingPartsNoM_PxP");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                ListDlvPart = listDlvPart
            };

            int result = db.Execute("DeliveryPackingPartsNoM_PxP/DPPNOM_Delete", args);

            RepoResult repoResult = new RepoResult();
            repoResult.Result = RepoResult.VALUE_SUCCESS;

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

        private SqlParameter CreateSqlParameterTblOfDeliveryPackingPartsNoM_PxP(string parameterName, IList<DeliveryPackingPartsNoM_PxP> DPPartsNoM_PxPCollection, string typeName)
        {
            DataTable table = new DataTable();
            table.Columns.Add("COMPANY_CD", type: typeof(string));
            table.Columns.Add("DEST_CD", type: typeof(string));
            table.Columns.Add("PART_NO", type: typeof(string));

            if (DPPartsNoM_PxPCollection != null)
            {
                foreach (DeliveryPackingPartsNoM_PxP data in DPPartsNoM_PxPCollection)
                {
                    DataRow row = table.NewRow();
                    row["COMPANY_CD"] = data.COMPANY_CD;
                    row["DEST_CD"] = data.DEST_CD;
                    row["PART_NO"] = data.PART_NO;
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
        public byte[] GenerateDownloadFile(IList<DeliveryPackingPartsNoM_PxP> delParttList)
        {
            byte[] result = null;

            try
            {
                result = CreateFile(delParttList);
            }
            finally
            {

            }

            return result;
        }

        private byte[] CreateFile(IList<DeliveryPackingPartsNoM_PxP> destList)
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
                NPOIWriter.EscapeSheetName(SHEET_NAME_DlV_PART_MASTER));
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
                                        IList<DeliveryPackingPartsNoM_PxP> destList)
        {
            int rowIdx = startRow;
            int itemCount = 0;

            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 0, cellStyleHeader, "Destination Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 1, cellStyleHeader, "Car Family Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 2, cellStyleHeader, "Part No");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 3, cellStyleHeader, "Status Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 4, cellStyleHeader, "Process Code");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 5, cellStyleHeader, "Part Name");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 6, cellStyleHeader, "Order Lot Size");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 7, cellStyleHeader, "Selective Matching Ratio");
         
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 8, cellStyleHeader, "Tc From");
            NPOIWriter.CreateSingleColHeader(wb, sheet1, 0, 9, cellStyleHeader, "Tc To");

            rowIdx = 1;
            foreach (DeliveryPackingPartsNoM_PxP st in destList)
            {
                WriteDetailSingleData(wb, cellStyleData, st, sheet1, ++itemCount, rowIdx++,
                    cellStyleData);
            }
        }

        private void WriteDetailSingleData(HSSFWorkbook wb, ICellStyle cellStyle, DeliveryPackingPartsNoM_PxP data,
                        ISheet sheet1, int rowCount, int rowIndex, ICellStyle cellStyleData)
        {
            IRow row = sheet1.CreateRow(rowIndex);
            int col = 0;

            NPOIWriter.createCellText(row, cellStyle, col++, data.DEST_CD);
            NPOIWriter.createCellText(row, cellStyle, col++, data.CFC);
            NPOIWriter.createCellText(row, cellStyle, col++, data.PART_NO);
            NPOIWriter.createCellText(row, cellStyle, col++, data.STATUS_CD);
            NPOIWriter.createCellText(row, cellStyle, col++, data.PROCESS_CD);
            NPOIWriter.createCellText(row, cellStyle, col++, data.PART_NAME);
            NPOIWriter.createCellText(row, cellStyle, col++, data.LOT_SIZE);
            NPOIWriter.createCellText(row, cellStyle, col++, data.SEL_MATCH_RATIO);
            NPOIWriter.createCellText(row, cellStyle, col++, data.TC_FROM);
            NPOIWriter.createCellText(row, cellStyle, col++, data.TC_TO);

            sheet1.AutoSizeColumn(0);
        }
        #endregion

    }

}