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


namespace GPPSU.Models.DayByDayExplosionInstructions
{
    public class DayByDayExplosionInstructionsRepo : BaseRepo
    {
       
        #region singleton
        private static DayByDayExplosionInstructionsRepo instance = null;
        public static DayByDayExplosionInstructionsRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DayByDayExplosionInstructionsRepo();
                }
                return instance;
            }
        }
        #endregion singleton


       public IList<DayByDayExplosionInstructions> Search(IDBContext db, DayByDayExplosionInstructions listParam)

        {

            dynamic args = new
                {
                IMPORTER_CD = listParam.IMPORTER_CD,
                EXPORTER_CD = listParam.EXPORTER_CD,
                ORD_TYPE = listParam.ORD_TYPE,
                PACK_MONTH = listParam.PACK_MONTH,
             
                CFC = listParam.CFC,
                RECEIVE_NO = listParam.RECEIVE_NO,
                COMPANY_CD = listParam.COMPANY_CD,
                K_PACK_CREATE_TIME = listParam.START_DATE
              
                // CREATED_BY = listParam.CREATED_BY
                //CREATED_DT = listParam.CREATED_DT != null? Convert.ToDateTime(listParam.CREATED_DT).ToString("yyyy-MM-dd") : null
            };
      
            IList<DayByDayExplosionInstructions> result = db.Fetch<DayByDayExplosionInstructions>("DayByDayExplosionInstructions/DayByDayExplosionInstructions_SearchData", args);
            return result;

        }
        public IList<DayByDayExplosionInstructions> SearchKeihen(IDBContext db, DayByDayExplosionInstructions listParam)

        {

            dynamic args = new
            {
                IMPORTER_CD = listParam.IMPORTER_CD,
                EXPORTER_CD = listParam.EXPORTER_CD,
                ORD_TYPE = listParam.ORD_TYPE,
                PACK_MONTH = listParam.PACK_MONTH,

                CFC = listParam.CFC,
                RECEIVE_NO = listParam.RECEIVE_NO,
                COMPANY_CD = listParam.COMPANY_CD,
                K_PACK_CREATE_TIME = listParam.START_DATE

                // CREATED_BY = listParam.CREATED_BY
                //CREATED_DT = listParam.CREATED_DT != null? Convert.ToDateTime(listParam.CREATED_DT).ToString("yyyy-MM-dd") : null
            };

            IList<DayByDayExplosionInstructions> result = db.Fetch<DayByDayExplosionInstructions>("DayByDayExplosionInstructions/DayByDayExplosionInstructions_SearchDataKeihen", args);
            return result;

        }

        public int SearchCount(IDBContext db, DayByDayExplosionInstructions listParam)

        {
                dynamic args = new
                {
                    IMPORTER_CD = listParam.IMPORTER_CD,
                    EXPORTER_CD = listParam.EXPORTER_CD,
                    ORD_TYPE = listParam.ORD_TYPE,
                    PACK_MONTH = listParam.PACK_MONTH,
                   
                    CFC = listParam.CFC,
                    RECEIVE_NO = listParam.RECEIVE_NO,
                    COMPANY_CD = listParam.COMPANY_CD
                };
                int result = db.SingleOrDefault<int>("DayByDayExplosionInstructions/DayByDayExplosionInstructions_SearchCount", args);
                return result;

            }

        public RepoResult Execute(DayByDayExplosionInstructions param, string userid)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                COMPANY_CD = param.COMPANY_CD,
                PROCESS_CD = param.PROCESS_CD,
                RECEIVE_NO = param.RECEIVE_NO,
                START_DATE = param.START_DATE,
                userId = userid,
        
            };

            IDBContext db = DatabaseManager.Instance.GetContext();
            int result = db.Execute("DayByDayExplosionInstructions/DayByDayExplosionInstructions_Execute", args);

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


    }

}