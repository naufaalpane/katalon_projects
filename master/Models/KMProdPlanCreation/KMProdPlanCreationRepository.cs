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


namespace GPPSU.Models.KMProdPlanCreation
{
    public class KMProdPlanCreationRepository : GPPSU.Commons.Repositories.BaseRepo
    {
        private static readonly string SHEET_NAME_DESTINATION_MASTER = "K/M Prod Plan Creation Screen";

        #region singleton
        private static KMProdPlanCreationRepository instance = null;
        public static KMProdPlanCreationRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new KMProdPlanCreationRepository();
                }
                return instance;
            }
        }
        #endregion singleton

        #region Search & Count
        public IList<KMProdPlanCreation> Search(IDBContext db, KMProdPlanCreation data)

        {
            dynamic args = new
            {
                LINE_CD = data.LINE_CD,
                PROCESS_CD = data.PROCESS_CD,
                LINE_NAME = data.LINE_NAME
            };

            IList<KMProdPlanCreation> result = db.Fetch<KMProdPlanCreation>("KMProdPlanCreation/KMProdPlanCreation_Search", args);
            return result;
        }


        #endregion


        public RepoResult Execute(KMProdPlanCreation param, string userid)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                COMPANY_CD = param.COMPANY_CD,
                PROCESS_CD = param.PROCESS_CD,
                LINE_CD = param.LINE_CD,
                DDMMYYYY = param.DDMMYYYY
               //, userId = userid

            };

            IDBContext db = DatabaseManager.Instance.GetContext();
            int result = db.Execute("KMProdPlanCreation/KMProdPlanCreation_Execute", args);

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