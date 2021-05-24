using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;
using GPPSU.Commons.Models;
using GPPSU.Models.Common;
using GPPSU.Commons.Repositories;

namespace GPPSU.Models.E_KanbanDataSending
{
    public class E_KanbanDataSendingRepository : BaseRepo
    {
        #region singleton
        private static E_KanbanDataSendingRepository instance = null;
        public static E_KanbanDataSendingRepository Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new E_KanbanDataSendingRepository();
                }
                return instance;
            }
        }
        #endregion

        public IList<NQCEkanban> GetNQCEKanban(IDBContext db,string comcd)
        {
            dynamic args = new
            {
                COMPANY_CD = comcd
            };
            IList<NQCEkanban> result = db.Fetch<NQCEkanban>("E-KanbanSendingData/NQCEKanban",args);
            return result;
            
        }
        #region execBatch
        public RepoResult ExecBacth()
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
                    ErrMessage = outputErrMesg
                };
                result = db.Execute("",args);
            }

            finally
            {
                db.Close();
            }

            repoResult.Result = RepoResult.VALUE_SUCCESS;

            if((int)outputRetVal.Value != 0)
            {
                repoResult.Result = RepoResult.VALUE_ERROR;
                string errMesg = string.Empty;

                if(outputErrMesg != null && outputErrMesg.Value != null)
                {
                    errMesg = outputErrMesg.Value.ToString();
                }
                repoResult.ErrMesgs = new string[1];
                repoResult.ErrMesgs[0] = errMesg;
            }

            return repoResult;
        }
        #endregion
    }
}