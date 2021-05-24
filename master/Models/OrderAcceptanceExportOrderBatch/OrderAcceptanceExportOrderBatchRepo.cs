using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Toyota.Common.Database;
using GPPSU.Commons.Repositories;

namespace GPPSU.Models.OrderAcceptanceExportOrderBatch
{
    public class OrderAcceptanceExportOrderBatchRepo : BaseRepo
    {

        private static OrderAcceptanceExportOrderBatchRepo instance = null;
        public static OrderAcceptanceExportOrderBatchRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderAcceptanceExportOrderBatchRepo();
                }
                return instance;
            }
        }

        

            public DataErrorReport GetOCNAndDate()
        {
            dynamic args = new
            {
            };
            DataErrorReport result = Db.SingleOrDefault<DataErrorReport>("OrderAcceptanceExportOrderBatch/GetOCNAndDate", args);

            return result;
        }




        public string SetTheExecutionResult()
        {
            dynamic args = new
            {
            };
            string result = Db.SingleOrDefault<string>("OrderAcceptanceExportOrderBatch/8_SetTheExecutionResult", args);

            return result;
        }

             public List<DataErrorReport> GetDataError()
        {
            dynamic args = new
            {
            };

            List<DataErrorReport> result = Db.Fetch<DataErrorReport>("OrderAcceptanceExportOrderBatch/GetDataError", args);

            return result;
        }

        public string CreateNewOrderControlTable(string Username, string Company_CD , IDBContext db)
        {
            dynamic args = new
            {
                Param_CompanyCD = Company_CD
                ,username = Username
            };
            string result = db.SingleOrDefault<string>("OrderAcceptanceExportOrderBatch/7_CreateNewOrderControlTable", args);

            db.Close();
            return result;
        }

        public string CreateNewOrderControlTableNoError(string Username, string Company_CD, IDBContext db)
        {
            dynamic args = new
            {
                Param_CompanyCD = Company_CD
                ,
                username = Username
            };
            string result = db.SingleOrDefault<string>("OrderAcceptanceExportOrderBatch/7_CreateNewOrderControlTableNoError", args);

            db.Close();
            return result;
        }

        public string OutputOrder(string Username , string Company_CD)
        {
            dynamic args = new
            {
               Param_CompanyCD  =  Company_CD
               ,username         =  Username
            };
            string result = Db.SingleOrDefault<string>("OrderAcceptanceExportOrderBatch/6_OutputOrder", args);
            Db.Close();
            return result;
        }

        public string ExecutionResult(string CompanyCd , string OkOrErr , string username)
        {
            dynamic args = new
            {
                CompanyCd  = CompanyCd  ,
                OkOrErr    = OkOrErr ,
                username = username
            };
            int result = Db.Execute("OrderAcceptanceExportOrderBatch/8_ExecutionResult", args);
            Db.Close();
            return result.ToString();
        }

        public string AssignNewReceive()
        {
            dynamic args = new
            {
            };
            string result = Db.SingleOrDefault<string>("OrderAcceptanceExportOrderBatch/5_AssignNewReceive", args);
            Db.Close();
            return result;
        }
        public string CheckHeaderInformationAndFormatCheck()
        {
            dynamic args = new
            {
            };
            string result = Db.SingleOrDefault<string>("OrderAcceptanceExportOrderBatch/3and4_CheckHeaderInformationAndFormatCheck", args);
            Db.Close();
            return result;
        }

        public void DeleteTemp()
        {
            dynamic args = new
            {      
            };
           Db.Execute("OrderAcceptanceExportOrderBatch/DeleteTemp", args);
            Db.Close();
        }

        public void uploadFile(DataToTempModel data, int NOMOR)
        {
            dynamic args = new
            {
              NOMOR                                        =   NOMOR                                               ,
              Param_DATAID                                 =   data.DATAID                                       ,
              Param_VERSION                                =   data.VERSION                                      ,
              Param_REVISIONNO                             =   data.REVISIONNO                                   ,
              Param_IMPORTERDCD                            =   data.IMPORTERDCD                                  ,
              Param_EXPORTERCD                             =   data.EXPORTERCD                                   ,
              Param_ODRTYPE                                =   data.ODRTYPE                                      ,
              Param_PCMN                                   =   data.PCMN                                         ,
              Param_CFC                                    =   data.CFC                                          ,
              Param_REEXPCD                                =   data.REEXPCD                                      ,
              Param_AICO_CEPT                              =   data.AICO_CEPT                                    ,
              Param_PXPLAG                                 =   data.PXPLAG                                       ,
              Param_IMPORTERNAME                           =   data.IMPORTERNAME                                 ,
              Param_EXPORTERNAME                           =   data.EXPORTERNAME                                 ,
              Param_SSNO                                   =   data.SSNO                                         ,
              Param_LINECD                                 =   data.LINECD                                       ,
              Param_PARTNO                                 =   data.PARTNO                                       ,
              Param_Lot_Code                               =   data.Lot_Code                                     ,
              Param_Exterior_Color                         =   data.Exterior_Color                               ,
              Param_Interior_Color                         =   data.Interior_Color                               ,
              Param_Control_Mode_Code                      =   data.Control_Mode_Code                            ,
              Param_Display_Mode_Code                      =   data.Display_Mode_Code                            ,
              Param_ODRLOT                                 =   data.ODRLOT                                       ,
              Param_MONTH_ORDER_VOLUME_Nmonth              =   data.MONTH_ORDER_VOLUME_Nmonth                    ,
              Param_MONTH_ORDER_VOLUME_Nmonth_PLUS_1       =   data.MONTH_ORDER_VOLUME_Nmonth_PLUS_1             ,
              Param_MONTH_ORDER_VOLUME_Nmonth_PLUS_2       =   data.MONTH_ORDER_VOLUME_Nmonth_PLUS_2             ,
              Param_MONTH_ORDER_VOLUME_Nmonth_PLUS_3       =   data.MONTH_ORDER_VOLUME_Nmonth_PLUS_3             ,
              Param_DAILY_VOLUME_1N                        =   data.DAILY_VOLUME_1N                              ,
              Param_DAILY_VOLUME_2N                        =   data.DAILY_VOLUME_2N                              ,
              Param_DAILY_VOLUME_3N                        =   data.DAILY_VOLUME_3N                              ,
              Param_DAILY_VOLUME_4N                        =   data.DAILY_VOLUME_4N                              ,
              Param_DAILY_VOLUME_5N                        =   data.DAILY_VOLUME_5N                              ,
              Param_DAILY_VOLUME_6N                        =   data.DAILY_VOLUME_6N                              ,
              Param_DAILY_VOLUME_7N                        =   data.DAILY_VOLUME_7N                              ,
              Param_DAILY_VOLUME_8N                        =   data.DAILY_VOLUME_8N                              ,
              Param_DAILY_VOLUME_9N                        =   data.DAILY_VOLUME_9N                              ,
              Param_DAILY_VOLUME_10N                       =   data.DAILY_VOLUME_10N                             ,
              Param_DAILY_VOLUME_11N                       =   data.DAILY_VOLUME_11N                             ,
              Param_DAILY_VOLUME_12N                       =   data.DAILY_VOLUME_12N                             ,
              Param_DAILY_VOLUME_13N                       =   data.DAILY_VOLUME_13N                             ,
              Param_DAILY_VOLUME_14N                       =   data.DAILY_VOLUME_14N                             ,
              Param_DAILY_VOLUME_15N                       =   data.DAILY_VOLUME_15N                             ,
              Param_DAILY_VOLUME_16N                       =   data.DAILY_VOLUME_16N                             ,
              Param_DAILY_VOLUME_17N                       =   data.DAILY_VOLUME_17N                             ,
              Param_DAILY_VOLUME_18N                       =   data.DAILY_VOLUME_18N                             ,
              Param_DAILY_VOLUME_19N                       =   data.DAILY_VOLUME_19N                             ,
              Param_DAILY_VOLUME_20N                       =   data.DAILY_VOLUME_20N                             ,
              Param_DAILY_VOLUME_21N                       =   data.DAILY_VOLUME_21N                             ,
              Param_DAILY_VOLUME_22N                       =   data.DAILY_VOLUME_22N                             ,
              Param_DAILY_VOLUME_23N                       =   data.DAILY_VOLUME_23N                             ,
              Param_DAILY_VOLUME_24N                       =   data.DAILY_VOLUME_24N                             ,
              Param_DAILY_VOLUME_25N                       =   data.DAILY_VOLUME_25N                             ,
              Param_DAILY_VOLUME_26N                       =   data.DAILY_VOLUME_26N                             ,
              Param_DAILY_VOLUME_27N                       =   data.DAILY_VOLUME_27N                             ,
              Param_DAILY_VOLUME_28N                       =   data.DAILY_VOLUME_28N                             ,
              Param_DAILY_VOLUME_29N                       =   data.DAILY_VOLUME_29N                             ,
              Param_DAILY_VOLUME_30N                       =   data.DAILY_VOLUME_30N                             ,
              Param_DAILY_VOLUME_31N                       =   data.DAILY_VOLUME_31N                             ,
              Param_DAILY_VOLUME_1N_PLUS_1                 =   data.DAILY_VOLUME_1N_PLUS_1                       ,
              Param_DAILY_VOLUME_2N_PLUS_1                 =   data.DAILY_VOLUME_2N_PLUS_1                       ,
              Param_DAILY_VOLUME_3N_PLUS_1                 =   data.DAILY_VOLUME_3N_PLUS_1                       ,
              Param_DAILY_VOLUME_4N_PLUS_1                 =   data.DAILY_VOLUME_4N_PLUS_1                       ,
              Param_DAILY_VOLUME_5N_PLUS_1                 =   data.DAILY_VOLUME_5N_PLUS_1                       ,
              Param_DAILY_VOLUME_6N_PLUS_1                 =   data.DAILY_VOLUME_6N_PLUS_1                       ,
              Param_DAILY_VOLUME_7N_PLUS_1                 =   data.DAILY_VOLUME_7N_PLUS_1                       ,
              Param_DAILY_VOLUME_8N_PLUS_1                 =   data.DAILY_VOLUME_8N_PLUS_1                       ,
              Param_DAILY_VOLUME_9N_PLUS_1                 =   data.DAILY_VOLUME_9N_PLUS_1                       ,
              Param_DAILY_VOLUME_10N_PLUS_1                =   data.DAILY_VOLUME_10N_PLUS_1                      ,
              Param_DAILY_VOLUME_11N_PLUS_1                =   data.DAILY_VOLUME_11N_PLUS_1                      ,
              Param_DAILY_VOLUME_12N_PLUS_1                =   data.DAILY_VOLUME_12N_PLUS_1                      ,
              Param_DAILY_VOLUME_13N_PLUS_1                =   data.DAILY_VOLUME_13N_PLUS_1                      ,
              Param_DAILY_VOLUME_14N_PLUS_1                =   data.DAILY_VOLUME_14N_PLUS_1                      ,
              Param_DAILY_VOLUME_15N_PLUS_1                =   data.DAILY_VOLUME_15N_PLUS_1                      ,
              Param_DAILY_VOLUME_16N_PLUS_1                =   data.DAILY_VOLUME_16N_PLUS_1                      ,
              Param_DAILY_VOLUME_17N_PLUS_1                =   data.DAILY_VOLUME_17N_PLUS_1                      ,
              Param_DAILY_VOLUME_18N_PLUS_1                =   data.DAILY_VOLUME_18N_PLUS_1                      ,
              Param_DAILY_VOLUME_19N_PLUS_1                =   data.DAILY_VOLUME_19N_PLUS_1                      ,
              Param_DAILY_VOLUME_20N_PLUS_1                =   data.DAILY_VOLUME_20N_PLUS_1                      ,
              Param_DAILY_VOLUME_21N_PLUS_1                =   data.DAILY_VOLUME_21N_PLUS_1                      ,
              Param_DAILY_VOLUME_22N_PLUS_1                =   data.DAILY_VOLUME_22N_PLUS_1                      ,
              Param_DAILY_VOLUME_23N_PLUS_1                =   data.DAILY_VOLUME_23N_PLUS_1                      ,
              Param_DAILY_VOLUME_24N_PLUS_1                =   data.DAILY_VOLUME_24N_PLUS_1                      ,
              Param_DAILY_VOLUME_25N_PLUS_1                =   data.DAILY_VOLUME_25N_PLUS_1                      ,
              Param_DAILY_VOLUME_26N_PLUS_1                =   data.DAILY_VOLUME_26N_PLUS_1                      ,
              Param_DAILY_VOLUME_27N_PLUS_1                =   data.DAILY_VOLUME_27N_PLUS_1                      ,
              Param_DAILY_VOLUME_28N_PLUS_1                =   data.DAILY_VOLUME_28N_PLUS_1                      ,
              Param_DAILY_VOLUME_29N_PLUS_1                =   data.DAILY_VOLUME_29N_PLUS_1                      ,
              Param_DAILY_VOLUME_30N_PLUS_1                =   data.DAILY_VOLUME_30N_PLUS_1                      ,
              Param_DAILY_VOLUME_31N_PLUS_1                =   data.DAILY_VOLUME_31N_PLUS_1                      ,
              Param_Transportation_Code                    =   data.Transportation_Code                          ,
              Param_Order_Date                             =   data.Order_Date                                   ,
              Param_Series                                 =   data.Series                                       ,
              Param_Dummy                                  =   data.Dummy                                        ,
              Param_Termination_Code                       =   data.Termination_Code
                                                        
            };
            int result = Db.Execute("OrderAcceptanceExportOrderBatch/UploadToTemp", args);
            Db.Close();
        }

    }
}