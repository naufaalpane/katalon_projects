using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Data.SqlClient;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;
using NQCEkanbanDataSending.Models;
using NQCEkanbanDataSending.Helper.DBConfig;
using NQCEkanbanDataSending.Helper.FTP;
using NQCEkanbanDataSending.Helper.Base;

namespace NQCEkanbanDataSending.AppCode
{
    class E_KanbanDataSending : BaseBatch
    {
        public E_KanbanDataSendingRepository eKanbanRepo = E_KanbanDataSendingRepository.Instance;
        public override List<Common> GetList()
        {
            throw new NotImplementedException();
        }
        public override void ExecuteBatch()
        {
            Console.WriteLine("Function is started");
           
            try
            {
                IDBContext db = dbManager.GetContext();
                string comcd = "a";
                db.BeginTransaction();
                IList<NQCEkanban> resultNQCEkanban = eKanbanRepo.GetNQCEKanban(db, comcd);
                CreateNQCKanban(resultNQCEkanban);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Function error : " + ex.Message);
               
            }
        }

        public void CreateNQCKanban(IList<NQCEkanban> ListResult)
        {
            try
            {
                #region create text file
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
