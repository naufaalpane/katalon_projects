
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NQCEkanbanDataSending.Models;
using NQCEkanbanDataSending.Helper.DBConfig;
using NQCEkanbanDataSending.Helper.FTP;
using NQCEkanbanDataSending.Helper.Base;
using System.IO;
using System.Reflection;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;
using System.IO.Compression;
using NQCEkanbanDataSending.Helper.Util;

namespace NQCEkanbanDataSending.AppCode
{
    public class TPIPPCStoICS : BaseBatch
    {
        public override void ExecuteBatch()
        {
            string loc = "TRANSFER POSTING IPPCS TO ICS";
            string module = "5";
            string function = "51005";
            int errorfound = 0;
            int succesfound = 0;

            #region var
            string DOCK_SERVICE_PART = null;
            string TP_SPLR_CODE = null;
            string SYSTEM_SOURCE_GR = null;
            string PROD_PURPOSE_CD_GR = null;
            string PROD_PURPOSE_GR_SERVICE_PART = null;
            string SOURCE_TYPE_TP = null;
            string MAT_DOC_DESC = null;
            string ORI_UNIT_MEASU_CD_GR = null;
            string POST_TIME_FLTR = null;
            string POST_LGCL_FLTR = null;
            string POST_FLG_FLTR = null;
            int TYPE_CODE = 0;
            string clientId = null;
            #endregion

            Common getProc = new Common();
            getProc.MSG_TXT = "Start Transfer Posting IPPCS To ICS";
            getProc.LOCATION = loc;
            getProc.PID = 0;
            getProc.MSG_ID = "TPINV030302001";
            getProc.MSG_TYPE = "I";
            getProc.MODULE_ID = module;
            getProc.FUNCTION_ID = function;
            getProc.USER_ID = "SYSTEM";
            getProc.PROCESS_STS = 0;
            Int64 PID = CommonDBHelper.Instance.CreateLog(getProc);

            CommonDBHelper Repo = CommonDBHelper.Instance;

            Console.WriteLine("Function is started");
            try
            {
                IDBContext db = dbManager.GetContext();

                Console.WriteLine("Fetching IPPCS TP data");

                getProc.MSG_TXT = "Fetching IPPCS TP data";
                getProc.LOCATION = loc;
                getProc.PID = PID;
                getProc.MSG_ID = "TPINV030302001";
                getProc.MSG_TYPE = "I";
                getProc.MODULE_ID = module;
                getProc.FUNCTION_ID = function;
                getProc.PROCESS_STS = 0;
                getProc.USER_ID = "SYSTEM";
                PID = CommonDBHelper.Instance.CreateLogDetail(getProc);

                #region params
                IEnumerable<SysvalObj> SysVal = Repo.getSystemVal().ToList();

                if (SysVal.Count() > 0)
                {
                    getProc.MSG_TXT = "Fetching IPPCS data";
                    getProc.LOCATION = loc;
                    getProc.PID = PID;
                    getProc.MSG_ID = "TPINV0303020";
                    getProc.MSG_TYPE = "E";
                    getProc.MODULE_ID = module;
                    getProc.FUNCTION_ID = function;
                    getProc.PROCESS_STS = 0;
                    getProc.USER_ID = "SYSTEM";

                    foreach (SysvalObj a in SysVal)
                    {
                        if (a.DOCK_SERVICE_PART == "" || a.DOCK_SERVICE_PART == null)
                        {
                            getProc.MSG_TXT = "TPINV030302001 : Please define parameter DOCK_SERVICE_PART in System Master";
                            PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                            errorfound++;
                        }

                        if (a.TP_SPLR_CODE == "" || a.TP_SPLR_CODE == null)
                        {
                            getProc.MSG_TXT = "TPINV030302001 : Please define parameter TP_SUPPLIER_CODE in System Master";
                            PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                            errorfound++;
                        }

                        if (a.SYSTEM_SOURCE_GR == "" || a.SYSTEM_SOURCE_GR == null)
                        {
                            getProc.MSG_TXT = "TPINV030302001 : Please define parameter SYSTEM_SOURCE in System Master";
                            PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                            errorfound++;
                        }

                        if (a.PROD_PURPOSE_CD_GR == "" || a.PROD_PURPOSE_CD_GR == null)
                        {
                            getProc.MSG_TXT = "TPINV030302001 : Please define parameter PROD_PURPOSE_CD in System Master";
                            PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                            errorfound++;
                        }

                        if (a.PROD_PURPOSE_GR_SERVICE_PART == "" || a.PROD_PURPOSE_GR_SERVICE_PART == null)
                        {
                            getProc.MSG_TXT = "TPINV030302001 : Please define parameter PROD_PURPOSE_SERVICE_PART in System Master";
                            PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                            errorfound++;
                        }

                        if (a.SOURCE_TYPE_TP == "" || a.SOURCE_TYPE_TP == null)
                        {
                            getProc.MSG_TXT = "TPINV030302001 : Please define parameter SOURCE_TYPE in System Master";
                            PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                            errorfound++;
                        }

                        if (a.MAT_DOC_DESC == "" || a.MAT_DOC_DESC == null)
                        {
                            getProc.MSG_TXT = "TPINV030302001 : Please define parameter MAT_DOC_DESC in System Master";
                            PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                            errorfound++;
                        }

                        if (a.ORI_UNIT_MEASU_CD_GR == "" || a.ORI_UNIT_MEASU_CD_GR == null)
                        {
                            getProc.MSG_TXT = "TPINV030302001 : Please define parameter ORI_UOM in System Master";
                            PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                            errorfound++;
                        }

                        if (a.POST_TIME_FLTR == "" || a.POST_TIME_FLTR == null)
                        {
                            getProc.MSG_TXT = "TPINV030302001 : Please define parameter POST_TIME_FLTR in System Master";
                            PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                            errorfound++;
                        }

                        if (a.POST_LGCL_FLTR == "" || a.POST_LGCL_FLTR == null)
                        {
                            getProc.MSG_TXT = "TPINV030302001 : Please define parameter POST_LGCL_FLTR in System Master";
                            PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                            errorfound++;
                        }

                        if (a.POST_FLG_FLTR == "" || a.POST_FLG_FLTR == null)
                        {
                            getProc.MSG_TXT = "TPINV030302001 : Please define parameter POST_FLG_FLTR in System Master";
                            PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                            errorfound++;
                        }

                        if (a.clientId == "" || a.clientId == null)
                        {
                            getProc.MSG_TXT = "TPINV030302001 : Please define parameter TP_CLIENT_ID in System Master";
                            PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                            errorfound++;
                        }

                        DOCK_SERVICE_PART = a.DOCK_SERVICE_PART;
                        TP_SPLR_CODE = a.TP_SPLR_CODE;
                        SYSTEM_SOURCE_GR = a.SYSTEM_SOURCE_GR;
                        PROD_PURPOSE_CD_GR = a.PROD_PURPOSE_CD_GR;
                        PROD_PURPOSE_GR_SERVICE_PART = a.PROD_PURPOSE_GR_SERVICE_PART;
                        SOURCE_TYPE_TP = a.SOURCE_TYPE_TP;
                        MAT_DOC_DESC = a.MAT_DOC_DESC;
                        ORI_UNIT_MEASU_CD_GR = a.ORI_UNIT_MEASU_CD_GR;
                        POST_TIME_FLTR = a.POST_TIME_FLTR;
                        POST_LGCL_FLTR = a.POST_LGCL_FLTR;
                        POST_FLG_FLTR = a.POST_FLG_FLTR;
                        clientId = a.clientId;
                    }
                }
                #endregion

                if (errorfound > 0)
                {
                    Console.WriteLine("Fetching Error Found : " + errorfound.ToString());
                    Console.WriteLine("Send TP Process is finished with error");
                    getProc.MSG_TXT = "Send TP Process is finished with error";
                    getProc.LOCATION = loc;
                    getProc.PID = PID;
                    getProc.MSG_ID = "TPINV030302001";
                    getProc.MSG_TYPE = "I";
                    getProc.MODULE_ID = module;
                    getProc.FUNCTION_ID = function;
                    getProc.USER_ID = "SYSTEM";
                    getProc.PROCESS_STS = 1;
                    PID = CommonDBHelper.Instance.CreateLogFinish(getProc);
                    return;
                }
                else
                {
                    Console.WriteLine("Get List Data TP");

                    IEnumerable<TPObj> DownloadList = Repo.GetListTP(PID, DOCK_SERVICE_PART, TP_SPLR_CODE, SYSTEM_SOURCE_GR, PROD_PURPOSE_CD_GR, PROD_PURPOSE_GR_SERVICE_PART, SOURCE_TYPE_TP, MAT_DOC_DESC, ORI_UNIT_MEASU_CD_GR, POST_TIME_FLTR, POST_LGCL_FLTR, POST_FLG_FLTR, TYPE_CODE, clientId).ToList();
                    Console.WriteLine(string.Format("Receive List Data : {0} data(s)", DownloadList.Count().ToString()));

                    StringBuilder tp = new StringBuilder("");
                    getProc.MSG_TXT = "Checking Mandatory TP IPPCS data";
                    getProc.LOCATION = loc;
                    getProc.PID = PID;
                    getProc.MSG_ID = "TPINV030302001";
                    getProc.MSG_TYPE = "E";
                    getProc.MODULE_ID = module;
                    getProc.FUNCTION_ID = function;
                    getProc.PROCESS_STS = 0;
                    getProc.USER_ID = "SYSTEM";

                    if (DownloadList.Count() > 0)
                    {
                        Console.WriteLine("Checking Mandatory TP IPPCS data");
                        #region validate mandatory

                        foreach (TPObj b in DownloadList)
                        {
                            string manifest_no = b.REF_NO;
                            string part_no = b.SND_PART_NO;
                            if (b.PROCESS_KEY == "" || b.PROCESS_KEY == null)
                            {
                                getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field PROCESS_KEY for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                                PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                                errorfound++;
                            }

                            if (b.SYSTEM_SOURCE == "" || b.SYSTEM_SOURCE == null)
                            {
                                getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field SYSTEM_SOURCE for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                                PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                                errorfound++;
                            }

                            if (b.CLIENT_ID == "" || b.CLIENT_ID == null)
                            {
                                getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field CLIENT_ID for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                                PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                                errorfound++;
                            }

                            if (b.MOVEMENT_TYPE == "" || b.MOVEMENT_TYPE == null)
                            {
                                getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field MOV_TYPE for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                                PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                                errorfound++;
                            }

                            if (b.DOC_DT == "" || b.DOC_DT == null)
                            {
                                getProc.MSG_TXT = "TPINV0303020: Error TP mandatory field DOC_DT for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                                PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                                errorfound++;
                            }

                            if (b.POSTING_DT == "" || b.POSTING_DT == null)
                            {
                                getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field POSTING_DT for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                                PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                                errorfound++;
                            }

                            if (b.REF_NO == "" || b.REF_NO == null)
                            {
                                getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field REF_NO for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                                PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                                errorfound++;
                            }

                            if (b.MAT_DOC_DESC == "" || b.REF_NO == null)
                            {
                                getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field MAT_DOC_DESC for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                                PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                                errorfound++;
                            }

                            if (b.SND_PART_NO == "" || b.SND_PART_NO == null)
                            {
                                getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field SND_PART_NO for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                                PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                                errorfound++;
                            }

                            if (b.SND_PROD_PURPOSE_CD == "" || b.SND_PROD_PURPOSE_CD == null)
                            {
                                getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field SND_PROD_PURPOSE_CD for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                                PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                                errorfound++;
                            }

                            if (b.SND_SOURCE_TYPE == "" || b.SND_SOURCE_TYPE == null)
                            {
                                getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field SND_SOURCE_TYPE for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                                PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                                errorfound++;
                            }

                            //if (b.SND_PLANT_CD == "" || b.SND_PLANT_CD == null)
                            //{
                            //    getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field SND_PLANT_CD for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                            //    PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                            //    errorfound++;
                            //}

                            //if (b.SND_SLOC_CD == "" || b.SND_SLOC_CD == null)
                            //{
                            //    getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field SND_SLOC_CD for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                            //    PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                            //    errorfound++;
                            //}

                            if (b.RCV_PART_NO == "" || b.RCV_PART_NO == null)
                            {
                                getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field RCV_PART_NO for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                                PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                                errorfound++;
                            }

                            if (b.RCV_PROD_PURPOSE_CD == "" || b.RCV_PROD_PURPOSE_CD == null)
                            {
                                getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field RCV_PROD_PURPOSE_CD for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                                PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                                errorfound++;
                            }

                            if (b.RCV_SOURCE_TYPE == "" || b.RCV_SOURCE_TYPE == null)
                            {
                                getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field RCV_SOURCE_TYPE for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                                PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                                errorfound++;
                            }

                            //if (b.RCV_PLANT_CD == "" || b.RCV_PLANT_CD == null)
                            //{
                            //    getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field RCV_PLANT_CD for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                            //    PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                            //    errorfound++;
                            //}

                            if (b.QUANTITY == "" || b.QUANTITY == null)
                            {
                                getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field QUANTITY for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                                PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                                errorfound++;
                            }

                            if (b.UOM == "" || b.UOM == null)
                            {
                                getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field UNIT_OF_MEASURE_CD for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                                PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                                errorfound++;
                            }

                            if (b.DN_COMPLETE_FLAG == "" || b.DN_COMPLETE_FLAG == null)
                            {
                                getProc.MSG_TXT = "TPINV030302001: Error TP mandatory field UNIT_OF_MEASURE_CD for Manifest No : " + manifest_no + " and Part No : " + part_no + "";
                                PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                                errorfound++;
                            }
                        }

                        #endregion
                        if (errorfound > 0)
                        {
                            Console.WriteLine("Checking Mandatory error found : " + errorfound.ToString());
                            Console.WriteLine("Send TP Process is finished with error");
                            getProc.MSG_TXT = "Send TP Process is finished with error";
                            getProc.LOCATION = loc;
                            getProc.PID = PID;
                            getProc.MSG_ID = "TPINV030302001";
                            getProc.MSG_TYPE = "I";
                            getProc.MODULE_ID = module;
                            getProc.FUNCTION_ID = function;
                            getProc.USER_ID = "SYSTEM";
                            getProc.PROCESS_STS = 1;
                            PID = CommonDBHelper.Instance.CreateLogFinish(getProc);
                            return;
                        }

                        Console.WriteLine("Executing Insert data into Temporary table");
                        getProc.MSG_TXT = "MINV030303002I : Executing Insert data into Temporary table";
                        getProc.LOCATION = loc;
                        getProc.PID = PID;
                        getProc.MSG_ID = "TPINV030302001";
                        getProc.MSG_TYPE = "I";
                        getProc.MODULE_ID = module;
                        getProc.FUNCTION_ID = function;
                        getProc.PROCESS_STS = 0;
                        getProc.USER_ID = "SYSTEM";
                        PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                        IEnumerable<TPObj> InsertTemp = Repo.InsertTemp().ToList();

                        string val = null;
                        string key = null;
                        #region check plant sloc
                        foreach (TPObj f in InsertTemp)
                        {
                            if (f.SND_PLANT_CD == null || f.SND_SLOC_CD == null || f.RCV_PLANT_CD == null || f.RCV_SLOC_CD == null)
                            {
                                val = "invalid";
                                key = f.PROCESS_KEY;
                                db.Execute("updateTP", new
                                {
                                    param = val,
                                    PROCESS_KEY = key
                                });
                                errorfound++;
                            }
                            else
                            {
                                val = "valid";
                                key = f.PROCESS_KEY;
                                db.Execute("updateTP", new
                                {
                                    param = val,
                                    PROCESS_KEY = key
                                });
                                succesfound++;
                            }
                        }


                        getProc.MSG_TXT = "TPINV0303020 : Total Data : " + DownloadList.Count() + ", Success  : " + succesfound + ", Error : " + errorfound + "";
                        getProc.LOCATION = loc;
                        getProc.PID = PID;
                        getProc.MSG_ID = "TPINV0303020";
                        getProc.MSG_TYPE = "I";
                        getProc.MODULE_ID = module;
                        getProc.FUNCTION_ID = function;
                        getProc.PROCESS_STS = 0;
                        getProc.USER_ID = "SYSTEM";
                        PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                        #endregion


                        Console.WriteLine("Executing Generate CSV file");
                        getProc.MSG_TXT = "MINV030303002I : Executing Generate CSV file";
                        getProc.LOCATION = loc;
                        getProc.PID = PID;
                        getProc.MSG_ID = "TPINV030302001";
                        getProc.MSG_TYPE = "I";
                        getProc.MODULE_ID = module;
                        getProc.FUNCTION_ID = function;
                        getProc.PROCESS_STS = 0;
                        getProc.USER_ID = "SYSTEM";
                        PID = CommonDBHelper.Instance.CreateLogDetail(getProc);

                        IEnumerable<TPObj> ResultAfterAllValidation = Repo.ResultAfterAllValidation().ToList();

                        #region create csv file
                        for (int i = 0; i < CsvCol.Count; i++)
                        {
                            if (i < CsvCol.Count - 1)
                            {
                                tp.Append(Quote(CsvCol[i].Text)); tp.Append(CSV_SEP);
                            }
                            if (i == CsvCol.Count - 1)
                            { tp.Append(Quote(CsvCol[i].Text)); }
                        }

                        tp.Append(Environment.NewLine);

                        foreach (TPObj d in ResultAfterAllValidation)
                        {
                            csvAddLine(tp, new string[] {
                            Equs(d.PROCESS_ID),
                            Equs(d.PROCESS_KEY),
                            Equs(d.SYSTEM_SOURCE),
                            Equs(d.CLIENT_ID),
                            Equs(d.MOVEMENT_TYPE),
                            Equs(d.DOC_DT),
                            Equs(d.POSTING_DT),
                            Equs(d.REF_NO),
                            Equs(d.MAT_DOC_DESC),
                            Equs(d.SND_PART_NO),
                            Equs(d.SND_PROD_PURPOSE_CD),
                            Equs(d.SND_SOURCE_TYPE),
                            Equs(d.SND_PLANT_CD),
                            Equs(d.SND_SLOC_CD),
                            Equs(d.SND_BATCH_NO),
                            Equs(d.RCV_PART_NO),
                            Equs(d.RCV_PROD_PURPOSE_CD),
                            Equs(d.RCV_SOURCE_TYPE),
                            Equs(d.RCV_PLANT_CD),
                            Equs(d.RCV_SLOC_CD),
                            Equs(d.RCV_BATCH_NO),
                            Equs(d.QUANTITY),
                            Equs(d.UOM),
                            Equs(d.CREATED_BY),
                            Equs(d.CREATED_DT)
                            }
                            );
                        }

                        //string zipPath = CSTDDateUtil.GetCurrentDBDate().ToString("yyyyMMddHHmmss");

                        string fileFormatNm = CommonDBHelper.Instance.getFilename();

                        Byte[] bin = UTF8Encoding.UTF8.GetBytes(tp.ToString());
                        string DownloadDirectory = "Temp/TP/" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "/";
                        string downdir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DownloadDirectory);

                        Console.WriteLine("Create Directory : " + downdir);
                        System.IO.Directory.CreateDirectory(downdir);

                        var pathfile = Path.Combine(downdir, fileFormatNm + ".csv");

                        System.IO.File.WriteAllBytes(pathfile, bin);
                        #endregion

                        #region clear data on temp table
                        db.Execute("clearTempTP", new { });
                        #endregion

                        #region create zip file
                        Console.WriteLine("Executing Generate ZIP file");
                        getProc.MSG_TXT = "MINV030303002I : Executing Generate ZIP file";
                        getProc.LOCATION = loc;
                        getProc.PID = PID;
                        getProc.MSG_ID = "TPINV030302001";
                        getProc.MSG_TYPE = "I";
                        getProc.MODULE_ID = module;
                        getProc.FUNCTION_ID = function;
                        getProc.PROCESS_STS = 0;
                        getProc.USER_ID = "SYSTEM";
                        PID = CommonDBHelper.Instance.CreateLogDetail(getProc);

                        string ZipFilename = downdir + fileFormatNm + ".zip";
                        using (ZipArchive newFile = ZipFile.Open(ZipFilename, ZipArchiveMode.Create))
                        {
                            newFile.CreateEntryFromFile(pathfile, fileFormatNm + ".csv", CompressionLevel.Fastest);
                        }
                        #endregion

                        #region Call API
                        Console.WriteLine("Executing call API");
                        getProc.MSG_TXT = "MINV030303002I : Executing call API";
                        getProc.LOCATION = loc;
                        getProc.PID = PID;
                        getProc.MSG_ID = "TPINV030302001";
                        getProc.MSG_TYPE = "I";
                        getProc.MODULE_ID = module;
                        getProc.FUNCTION_ID = function;
                        getProc.PROCESS_STS = 0;
                        getProc.USER_ID = "SYSTEM";
                        PID = CommonDBHelper.Instance.CreateLogDetail(getProc);

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data["Username"] = "abi";
                        data["SystemSource"] = "IPPCS";
                        data["File"] = new FormFile() { Name = Path.GetFileName(ZipFilename), ContentType = MimeUtil.GetMimeType(Path.GetExtension(ZipFilename)), FilePath = ZipFilename }; //ZipFilename;

                        response isResult = WebAPIUtil.RequestFile<SendICSRes>(data, "GRInterface", null, "POST");

                        Console.WriteLine("API Response ==> " + "PROCESS_ID : " + isResult.PROCESS_ID.ToString() + ", MESSAGE : " + isResult.MESSAGE_TEXT + "");
                        getProc.MSG_TXT = "PROCESS_ID : " + isResult.PROCESS_ID + ", MESSAGE : " + isResult.MESSAGE_TEXT + "";
                        getProc.LOCATION = loc;
                        getProc.PID = PID;
                        getProc.MSG_ID = "TPINV030302001";
                        getProc.MSG_TYPE = "I";
                        getProc.MODULE_ID = module;
                        getProc.FUNCTION_ID = function;
                        getProc.PROCESS_STS = 0;
                        getProc.USER_ID = "SYSTEM";
                        PID = CommonDBHelper.Instance.CreateLogDetail(getProc);
                        #endregion

                        // isResult.MESSAGE_TEXT = "Success"; 
                        if (isResult.MESSAGE_TEXT == "Success")
                        {
                            db.Execute("SETYFORSUCCESS");
                        }

                        #region Update data
                        getProc.MSG_TXT = "Update send flag";
                        getProc.LOCATION = loc;
                        getProc.PID = PID;
                        getProc.MSG_ID = "TPINV0303020";
                        getProc.MSG_TYPE = "I";
                        getProc.MODULE_ID = module;
                        getProc.FUNCTION_ID = function;
                        getProc.PROCESS_STS = 0;
                        getProc.USER_ID = "SYSTEM";
                        PID = CommonDBHelper.Instance.CreateLogDetail(getProc);

                        db.BeginTransaction();
                        dynamic args = new
                        {

                        };
                        db.Execute("UpdateTPSeq", args);
                        db.CommitTransaction();
                        #endregion

                        #region Delete file
                        Console.WriteLine("Delete : " + downdir);
                        Directory.Delete(downdir, true);

                        //System.IO.DirectoryInfo di = new DirectoryInfo(downdir);

                        //foreach (FileInfo file in di.GetFiles())
                        //{
                        //    file.Delete();
                        //}
                        //foreach (DirectoryInfo dir in di.GetDirectories())
                        //{
                        //    dir.Delete(true);
                        //}
                        #endregion

                        Console.WriteLine("Finish Sending Status TP from IPPCS to ICS");
                        getProc.MSG_TXT = "MINV030303002I : Finish Sending Status TP from IPPCS to ICS";
                        getProc.LOCATION = loc;
                        getProc.PID = PID;
                        getProc.MSG_ID = "TPINV030302001";
                        getProc.MSG_TYPE = "I";
                        getProc.MODULE_ID = module;
                        getProc.FUNCTION_ID = function;
                        getProc.PROCESS_STS = 0;
                        getProc.USER_ID = "SYSTEM";
                        PID = CommonDBHelper.Instance.CreateLogFinish(getProc);
                    }
                    else
                    {
                        Console.WriteLine("No data found for TP IPPCS to ICS");
                        getProc.MSG_TXT = "TPINV030302001: No data found for TP IPPCS to ICS";
                        getProc.LOCATION = loc;
                        getProc.PID = PID;
                        getProc.MSG_ID = "TPINV030302001";
                        getProc.MSG_TYPE = "I";
                        getProc.MODULE_ID = module;
                        getProc.FUNCTION_ID = function;
                        getProc.PROCESS_STS = 1;
                        getProc.USER_ID = "SYSTEM";
                        PID = CommonDBHelper.Instance.CreateLogFinish(getProc);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Function error : " + ex.Message);
                Console.WriteLine("Send TP Process is finished with error");
                getProc.MSG_TXT = ex.Message.ToString(); //"Send TP Process is finished with error";
                getProc.LOCATION = loc;
                getProc.PID = PID;
                getProc.MSG_ID = "TPINV030302001";
                getProc.MSG_TYPE = "I";
                getProc.MODULE_ID = module;
                getProc.FUNCTION_ID = function;
                getProc.PROCESS_STS = 1;
                getProc.USER_ID = "SYSTEM";
                PID = CommonDBHelper.Instance.CreateLogFinish(getProc);
            }
        }

        private string _csv_sep;
        private string CSV_SEP
        {
            get
            {
                if (string.IsNullOrEmpty(_csv_sep))
                {

                    //_csv_sep = ConfigurationManager.AppSettings["CSV_SEPARATOR"];
                    _csv_sep = ",";//System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
                    if (string.IsNullOrEmpty(_csv_sep)) _csv_sep = ",";
                }
                return _csv_sep;
            }
        }

        public void csvAddLine(StringBuilder b, string[] values)
        {
            string SEP = CSV_SEP;
            for (int i = 0; i < values.Length - 1; i++)
            {
                b.Append(values[i]); b.Append(SEP);
            }
            b.Append(values[values.Length - 1]);
            b.Append(Environment.NewLine);
        }

        private List<CsvColumn> CsvCol = CsvColumn.Parse(
             "PROCESS_ID|C|20|0|PROCESS_ID;" +
             "PROCESS_KEY|C|20|0|PROCESS_KEY;" +
             "SYSTEM_SOURCE|C|10|0|SYSTEM_SOURCE;" +
             "CLIENT_ID|C|20|1|CLIENT_ID;" +
             "MOVEMENT_TYPE|C|20|1|MOV_TYPE;" +
             "DOC_DT|C|30|1|DOC_DT;" +
             "POSTING_DT|C|30|1|POSTING_DT;" +
             "REF_NO|C|16|1|REF_NO;" +
             "MAT_DOC_DESC|C|50|1|MAT_DOC_DESC;" +
             "SND_PART_NO|C|20|1|SND_PART_NO;" +
             "SND_PROD_PURPOSE_CD|C|20|1|SND_PROD_PURPOSE_CD;" +
             "SND_SOURCE_TYPE|C|20|1|SND_SOURCE_TYPE;" +
             "SND_PLANT_CD|C|20|1|SND_PLANT_CD;" +
             "SND_SLOC_CD|C|20|1|SND_SLOC_CD;" +
             "SND_BATCH_NO|C|20|1|SND_BATCH_NO;" +
             "RCV_PART_NO|C|20|1|RCV_PART_NO;" +
             "RCV_PROD_PURPOSE_CD|C|20|1|RCV_PROD_PURPOSE_CD;" +
             "RCV_SOURCE_TYPE|C|20|1|RCV_SOURCE_TYPE;" +
             "RCV_PLANT_CD|C|20|1|RCV_PLANT_CD;" +
             "RCV_SLOC_CD|C|20|1|RCV_SLOC_CD;" +
             "RCV_BATCH_NO|C|20|1|RCV_BATCH_NO;" +
             "QUANTITY|C|20|1|QUANTITY;" +
             "UOM|C|20|1|UOM;" +
             "CREATED_BY|C|20|1|CREATED_BY;" +
             "CREATED_DT|C|30|1|CREATED_DT;")
             ;

        private string Quote(string s)
        {
            return s != null ? "\"" + s + "\"" : s;
        }

        /// <summary>
        /// put Equal Sign in front and quote char to prevent 'numerization' by Excel
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string Equs(string s)
        {
            string separator = ",";// System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
            if (s != null)
            {
                if (s.Contains(separator))
                {
                    s = "\"" + s + "\"";
                }
                else
                {
                    s = "\"" + s + "\"";//s = "=\"" + s + "\"";
                }
            }
            return s;
        }

        public class CsvColumn
        {
            public string Text { get; set; }
            public string Column { get; set; }
            public string DataType { get; set; }
            public int Len { get; set; }
            public int Mandatory { get; set; }

            public static List<CsvColumn> Parse(string v)
            {
                List<CsvColumn> l = new List<CsvColumn>();

                string[] x = v.Split(';');


                for (int i = 0; i < x.Length; i++)
                {
                    string[] y = x[i].Split('|');
                    CsvColumn me = null;
                    if (y.Length >= 4)
                    {
                        int len = 0;
                        int mandat = 0;
                        int.TryParse(y[2], out len);
                        int.TryParse(y[3], out mandat);
                        me = new CsvColumn()
                        {
                            Text = y[0],
                            DataType = y[1],
                            Len = len,
                            Mandatory = mandat
                        };
                        l.Add(me);
                    }

                    if (y.Length > 4 && me != null)
                    {
                        me.Column = y[4];
                    }
                }
                return l;
            }


        }


        public override List<Common> GetList()
        {
            throw new NotImplementedException();
        }
    }
}
