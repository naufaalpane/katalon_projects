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

namespace GPPSU.Models.HEIJUNKACodeNameMasterSettings
{
    public class HEIJUNKACodeNameMasterSettingsRepo : BaseRepo
    {
       
        #region singleton
        private static HEIJUNKACodeNameMasterSettingsRepo instance = null;
        public static HEIJUNKACodeNameMasterSettingsRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HEIJUNKACodeNameMasterSettingsRepo();
                }
                return instance;
            }
        }
        #endregion singleton


       public IList<HEIJUNKACodeNameMasterSettings> Search(IDBContext db, HEIJUNKACodeNameMasterSettings listParam, long currentPage, long rowsPerPage)

        {

            dynamic args = new
                {
                COMPANY_CD = listParam.COMPANY_CD,
                HEIJUNKA_CD = listParam.HEIJUNKA_CD,
                HEIJUNKA_NAME = listParam.HEIJUNKA_NAME,

                PageNumber = currentPage,
                PageSize = rowsPerPage
                // CREATED_BY = listParam.CREATED_BY
                //CREATED_DT = listParam.CREATED_DT != null? Convert.ToDateTime(listParam.CREATED_DT).ToString("yyyy-MM-dd") : null
            };
      
            IList<HEIJUNKACodeNameMasterSettings> result = db.Fetch<HEIJUNKACodeNameMasterSettings>("HEIJUNKACodeNameMaster/HEIJUNKACodeNameMaster_SearchData", args);
            return result;

        }

        public int SearchCount(IDBContext db, HEIJUNKACodeNameMasterSettings listParam)

        {
                dynamic args = new
                {
                    COMPANY_CD = listParam.COMPANY_CD,
                    HEIJUNKA_CD = listParam.HEIJUNKA_CD,
                    HEIJUNKA_NAME = listParam.HEIJUNKA_NAME
                };
                int result = db.SingleOrDefault<int>("HEIJUNKACodeNameMaster/HEIJUNKACodeNameMaster_SearchCount", args);
                return result;

            }

        #region SaveAddEdit
        public RepoResult SaveAddEdit(HEIJUNKACodeNameMasterSettings data,string userId, string companyCode, string screenMode)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");


            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesgs = outputErrMesg,
                companyCode = companyCode,
                HEIJUNKA_CD = data.HEIJUNKA_CD,
                HEIJUNKA_NAME = data.HEIJUNKA_NAME,
                userId = userId,
                screenMode = screenMode

            };
            IDBContext db = DatabaseManager.Instance.GetContext();
            int result = db.Execute("HEIJUNKACodeNameMaster/HEIJUNKACodeNameMasterSettings_SaveAddEdit", args);

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

        public HEIJUNKACodeNameMasterSettings GetByKey(IDBContext db, HEIJUNKACodeNameMasterSettings param)
        {
            dynamic args = new
            {
                param.COMPANY_CD,
          
                param.HEIJUNKA_CD
            };

            HEIJUNKACodeNameMasterSettings result = Db.SingleOrDefault<HEIJUNKACodeNameMasterSettings>("HEIJUNKACodeNameMasterSettings/HEIJUNKACodeNameMasterSettings_GetByKey", args);

            return result;
        }

        #endregion
        #region delete
        public GPPSU.Commons.Models.RepoResult Delete(IDBContext db, HEIJUNKACodeNameMasterSettings data)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");
            SqlParameter outputTblOfHEIJUNKA = CreateSqlParameterTblOfHEIJUNKAMaster("TableOfHEIJUNKAMaster", data.listData, "dbo.TableOfHEIJUNKAMasterSettings");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                TblOfHEIJUNKAData = outputTblOfHEIJUNKA
            };

            int result = db.Execute("HEIJUNKACodeNameMaster/HEIJUNKACodeNameMaster_Delete", args);

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

        private SqlParameter CreateSqlParameterTblOfHEIJUNKAMaster(string parameterName, IList<HEIJUNKACodeNameMasterSettings> listData, string typeName)
        {
            DataTable table = new DataTable();
            table.Columns.Add("COMPANY_CD", type: typeof(string));
            table.Columns.Add("HEIJUNKA_CD", type: typeof(string));
         

            if (listData != null)
            {
                foreach (HEIJUNKACodeNameMasterSettings data in listData)
                {
                    DataRow row = table.NewRow();
                    row["COMPANY_CD"] = data.COMPANY_CD;
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

    }

}