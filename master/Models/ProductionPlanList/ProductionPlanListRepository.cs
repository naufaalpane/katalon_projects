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


namespace GPPSU.Models.ProductionPlanList
{
    public class ProductionPlanListRepository : GPPSU.Commons.Repositories.BaseRepo
    {
        private static readonly string SHEET_NAME_DESTINATION_MASTER = "K/M Prod Plan Creation Screen";

        #region singleton
        private static ProductionPlanListRepository instance = null;
        public static ProductionPlanListRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductionPlanListRepository();
                }
                return instance;
            }
        }
        #endregion singleton

        #region Search & Count
        public IList<ProductionPlanList> Search(IDBContext db, ProductionPlanList data)

        {
            dynamic args = new
            {
                processCd = data.PROCESS_CD,
                companyCd = data.COMPANY_CD
            };

            IList<ProductionPlanList> result = db.Fetch<ProductionPlanList>("ProductionPlanList/ProductionPlanList_Search", args);
            return result;
        }


        #endregion

        public RepoResult Print(ProductionPlanList param, string userid)
        {
            SqlParameter outputRetVal = CreateSqlParameterOutputReturnValue("RetVal");
            SqlParameter outputErrMesg = CreateSqlParameterOutputErrMesg("ErrorMessage");

            dynamic args = new
            {
                RetVal = outputRetVal,
                ErrMesg = outputErrMesg,
                companyCd = param.COMPANY_CD,
                proceesCd = param.PROCESS_CD,
                lineCd = param.LINE_CD,
                startYyyymm = param.START_YYYYMM,
                endYyyymm = param.END_YYYYMM
                , userId = userid

            };

            IDBContext db = DatabaseManager.Instance.GetContext();
            int result = db.Execute("ProductionPlanList/ProductionPlanList_Print", args);

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