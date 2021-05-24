using NQCEkanbanDataSending.Helper.Base;
using NQCEkanbanDataSending.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;
using System.Data.SqlClient;

namespace NQCEkanbanDataSending.Helper.DBConfig
{
    public class CommonDBHelper : BaseRepository<Common>
    {

        #region Singleton
        private CommonDBHelper() { }
        private static CommonDBHelper instance = null;
        public static CommonDBHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    SetConfig();
                    instance = new CommonDBHelper();
                }

                return instance;
            }
        }
        #endregion

        #region FTP
        public List<FTPCredential> GetFtpCredential(string ID, string sql)
        {
            IDBContext db = dbManager.GetContext();
            var Result = db.Fetch<FTPCredential>(sql, new { ID = ID, PARAM = "" });
            db.Close();
            return Result.ToList();
        }

        public List<FTPCredential> GetFtpCredentialSucc(string param, string ID, string sql)
        {
            IDBContext db = dbManager.GetContext();
            var Result = db.Fetch<FTPCredential>(sql, new { ID = ID, PARAM = param });
            db.Close();
            return Result.ToList();
        }
        #endregion

        #region Create Log
        public Int64 CreateLog(Common model)
        {
            IDBContext db = dbManager.GetContext();

            Int64 result = db.SingleOrDefault<Int64>("CreateLog", model);
            db.Close();
            return result;
        }

        public Int64 CreateLogDetail(Common model)
        {
            IDBContext db = dbManager.GetContext();

            Int64 result = db.SingleOrDefault<Int64>("CreateLogDetail", model);
            db.Close();
            return result;
        }

        public Int64 CreateLogFinish(Common model)
        {
            IDBContext db = dbManager.GetContext();

            Int64 result = db.SingleOrDefault<Int64>("CreateLogFinish", model);
            db.Close();
            return result;
        }
        #endregion

        #region master
        public List<SysvalObj> getSystemVal()
        {
            IDBContext db = dbManager.GetContext();
            var result = db.Fetch<SysvalObj>("GetSystemVal", new { });
            db.Close();
            return result.ToList();
        }

        public string getFilename()
        {
            IDBContext db = dbManager.GetContext();

            var result = db.Fetch<string>("getFilename");
            db.Close();
            return result.FirstOrDefault();
        }

        public string getAPIUrl()
        {
            IDBContext db = dbManager.GetContext();
            string result = db.SingleOrDefault<string>("GetAPIUrl");
            db.Close();
            return result;
        }
        #endregion

        #region transaction

       public List<TPObj> ResultAfterAllValidation()
        {
            IDBContext db = dbManager.GetContext();
            var Result = db.Fetch<TPObj>("GetTPDataAfterAllValidation");
            db.Close();
            return Result.ToList();
        } 

        public List<TPObj> GetListTP(long PID ,string DOCK_SERVICE_PART, string TP_SPLR_CODE, string SYSTEM_SOURCE_GR, string PROD_PURPOSE_CD_GR, string PROD_PURPOSE_GR_SERVICE_PART, string SOURCE_TYPE_TP, string MAT_DOC_DESC, string ORI_UNIT_MEASU_CD_GR, string POST_TIME_FLTR, string POST_LGCL_FLTR, string POST_FLG_FLTR, int TYPE_CODE, string clientId)
        {
            IDBContext db = dbManager.GetContext();
            db.SetCommandTimeout(12000);
            var Result = db.Fetch<TPObj>("GetListTP", new {
                PID = PID,
                dockServicePart = DOCK_SERVICE_PART,
                TP_SUPPLIER_CODE = TP_SPLR_CODE,
                SYS_SOURCE = SYSTEM_SOURCE_GR,
                PROD_PURPOSE_CD = PROD_PURPOSE_CD_GR,
                prodPurposeServicePart = PROD_PURPOSE_GR_SERVICE_PART,
                SOURCE_TYPE_TP = SOURCE_TYPE_TP,
                MAT_DOC_DESC = MAT_DOC_DESC,
                URI_UOM = ORI_UNIT_MEASU_CD_GR,
                FLTR_DATETIME_VALUE = POST_TIME_FLTR,
                FLTR_LOGICAL_VALUE = POST_LGCL_FLTR,
                FLTR_FLAG_VALUE = POST_FLG_FLTR,
                TYPE_CODE = TYPE_CODE,
                clientId = clientId
            });
            db.Close();
            return Result.ToList();
        }

        public List<TPObj> InsertTemp()
        {
            IDBContext db = dbManager.GetContext();
            var Result = db.Fetch<TPObj>("insertTP", new { });
            db.Close();
            return Result.ToList();
        }
        #endregion

        public override List<Common> GetList()
        {
            throw new NotImplementedException();
        }

        public string updateTemp(string val, string key)
        {
            IDBContext db = dbManager.GetContext();
            string result = db.SingleOrDefault<string>("updateTP", new { 
                param = val,
                PROCESS_KEY = key
            });
            db.Close();
            return result;
        }
    }
}
