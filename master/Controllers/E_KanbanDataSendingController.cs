using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using GPPSU.Commons.Controllers;
using GPPSU.Models.E_KanbanDataSending;
using GPPSU.Commons.Models;
using Toyota.Common.Credential;


namespace GPPSU.Controllers
{
    public class E_KanbanDataSendingController : BaseController
    {

        public DatabaseManager dManage = DatabaseManager.Instance;
        public E_KanbanDataSendingRepository eKanbanRepo = E_KanbanDataSendingRepository.Instance;
        protected override void Startup()
        {
            base.Startup();
            Settings.Title = "E-Kanban Data Sending";
        }

        public void ExecuteBatch(string comcd)
        {
            AjaxResult ajaxResult = new AjaxResult();
            RepoResult repoResult = null;
            IDBContext db = dManage.GetContext();

            string noreg = Lookup.Get<User>().RegistrationNumber;

            try
            {
                db.BeginTransaction();
                IList<NQCEkanban> resultNQCEkanban = eKanbanRepo.GetNQCEKanban(db, comcd);
                CreateNQCKanban(resultNQCEkanban);
            }
            catch (Exception e)
            {
                db.AbortTransaction();
                ajaxResult.Result = AjaxResult.VALUE_ERROR;
                ajaxResult.ErrMesgs = new string[] {
                    string.Format("{0} = {1}", e.GetType().FullName, e.Message)
                };
            }
            finally
            {
                db.Close();
            }
        }
        public void CreateNQCKanban(IList<NQCEkanban> ListResult)
        {
            try
            {
                #region create csv file
                StringBuilder NQC = new StringBuilder();
                
                NQC.Append(Environment.NewLine);
                
                foreach (NQCEkanban d in ListResult)
                {
                    TxtAddLine(NQC, new string[] {
                            (d.PROD_PLAN_ID_CD),
                            (d.USER_CD),
                            (d.CFC),
                            (d.SS_NO),
                            (d.BASIC),
                            (d.PART_NO),
                            (d.COLOR_SUFFIX),
                            (d.SOURCE_CD),
                            (d.PART_MATCH_KEY),
                            (d.F_DUMMY),
                            (d.ID_LINE),
                            (d.F_DUMMY),
                            (d.DEST_CD),
                            (d.F_DUMMY),
                            (d.REP_PART_FLAG),
                            (d.OFF_OPT_CD),
                            (d.PROD_PLANT_TYPE),
                            (d.FREE_AREA),
                            (d.START_DT),
                            (d.EFF_RANGE_DAY_QTY),
                            (d.DAY_QTY_01),
                            (d.DAY_QTY_02),
                            (d.DAY_QTY_03),
                            (d.DAY_QTY_04),
                            (d.DAY_QTY_05),
                            (d.DAY_QTY_06),
                            (d.DAY_QTY_07),
                            (d.DAY_QTY_08),
                            (d.DAY_QTY_09),
                            (d.DAY_QTY_10),
                            (d.DAY_QTY_11),
                            (d.DAY_QTY_12),
                            (d.DAY_QTY_13),
                            (d.DAY_QTY_14),
                            (d.DAY_QTY_15),
                            (d.DAY_QTY_16),
                            (d.DAY_QTY_17),
                            (d.DAY_QTY_18),
                            (d.DAY_QTY_19),
                            (d.DAY_QTY_20),
                            (d.DAY_QTY_21),
                            (d.DAY_QTY_22),
                            (d.DAY_QTY_23),
                            (d.DAY_QTY_24),
                            (d.DAY_QTY_25),
                            (d.DAY_QTY_26),
                            (d.DAY_QTY_27),
                            (d.DAY_QTY_28),
                            (d.DAY_QTY_29),
                            (d.DAY_QTY_30),
                            (d.DAY_QTY_31),
                            (d.DAY_QTY_SIGN_01),
                            (d.DAY_QTY_SIGN_02),
                            (d.DAY_QTY_SIGN_03),
                            (d.DAY_QTY_SIGN_04),
                            (d.DAY_QTY_SIGN_05),
                            (d.DAY_QTY_SIGN_06),
                            (d.DAY_QTY_SIGN_07),
                            (d.DAY_QTY_SIGN_08),
                            (d.DAY_QTY_SIGN_09),
                            (d.DAY_QTY_SIGN_10),
                            (d.DAY_QTY_SIGN_11),
                            (d.DAY_QTY_SIGN_12),
                            (d.DAY_QTY_SIGN_13),
                            (d.DAY_QTY_SIGN_14),
                            (d.DAY_QTY_SIGN_15),
                            (d.DAY_QTY_SIGN_16),
                            (d.DAY_QTY_SIGN_17),
                            (d.DAY_QTY_SIGN_18),
                            (d.DAY_QTY_SIGN_19),
                            (d.DAY_QTY_SIGN_20),
                            (d.DAY_QTY_SIGN_21),
                            (d.DAY_QTY_SIGN_22),
                            (d.DAY_QTY_SIGN_23),
                            (d.DAY_QTY_SIGN_24),
                            (d.DAY_QTY_SIGN_25),
                            (d.DAY_QTY_SIGN_26),
                            (d.DAY_QTY_SIGN_27),
                            (d.DAY_QTY_SIGN_28),
                            (d.DAY_QTY_SIGN_29),
                            (d.DAY_QTY_SIGN_30),
                            (d.DAY_QTY_SIGN_31),
                            (d.CYCLE_FLAG),
                            (d.START_DT_TOTAL_QTY),
                            (d.EF_RANGE_TOTAL_QTY),
                            (d.TOTAL_QTY_01),
                            (d.TOTAL_QTY_SIGN_01),
                            }
                    );
                }

                string fileFormatNm = "NQC E-Kanban";

                Byte[] bin = UTF8Encoding.UTF8.GetBytes(NQC.ToString());
                string DownloadDirectory = "Temp/NQC/" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "/";
                string downdir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DownloadDirectory);

                Console.WriteLine("Create Directory : " + downdir);
                System.IO.Directory.CreateDirectory(downdir);

                var pathfile = Path.Combine(downdir, fileFormatNm + ".txt");

                System.IO.File.WriteAllBytes(pathfile, bin);
                #endregion

            }
            catch
            {

            }
        }

        //private string _text_sep;
        //private string TEXT_SEP
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(_text_sep))
        //        {

        //            //_csv_sep = ConfigurationManager.AppSettings["CSV_SEPARATOR"];
        //            _text_sep = " ";//System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
        //            if (string.IsNullOrEmpty(_text_sep)) _text_sep = " ";
        //        }
        //        return _text_sep;
        //    }
        //}
        public void TxtAddLine(StringBuilder b, string[] values)
        {
            //string SEP = TEXT_SEP;
            for (int i = 0; i < values.Length - 1; i++)
            {
                b.Append(values[i]); //b.Append(SEP);
            }
            b.Append(values[values.Length - 1]);
            b.Append(Environment.NewLine);
        }

    }
}