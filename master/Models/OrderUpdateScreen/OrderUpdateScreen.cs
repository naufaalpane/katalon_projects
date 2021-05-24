using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPPSU.Models.OrderUpdateScreen
{
    public class OrderUpdateScreen
    {        
        public IList<DailyAllocationList> DAILYRATIOLIST { get; set; }
        public IList<DailyHeaderList> DailyList { get; set; }

        public string YYMM { get; set; }
        public string RECEIVE_NO { get; set; }
        public string DETAIL_NO { get; set; }

        public string COMPANY_CD { get; set; }
        public int STATUS_CD { get; set; }
        public string IMPORTER_CD { get; set; }
        public string EXPORTER_CD { get; set; }
        public string ORDER_TYPE { get; set; }
        public string PACK_MONTH { get; set; }
        public string CFC { get; set; }
        public string DISABLE_FLAG { get; set; }
        public string RE_EXPORT_CD { get; set; }

        public string PART_NO { get; set; }
        public int LOT_SIZE { get; set; }
        public int TOTAL_MONTHLY_01 { get; set; }
        public int TOTAL_MONTHLY_02 { get; set; }
        public int TOTAL_MONTHLY_03 { get; set; }
        public int TOTAL_MONTHLY_04 { get; set; }
        public int DAY_ORD_VOL_01 { get; set; }
        public int DAY_ORD_VOL_02 { get; set; }
        public int DAY_ORD_VOL_03 { get; set; }
        public int DAY_ORD_VOL_04 { get; set; }
        public int DAY_ORD_VOL_05 { get; set; }
        public int DAY_ORD_VOL_06 { get; set; }
        public int DAY_ORD_VOL_07 { get; set; }
        public int DAY_ORD_VOL_08 { get; set; }
        public int DAY_ORD_VOL_09 { get; set; }
        public int DAY_ORD_VOL_10 { get; set; }
        public int DAY_ORD_VOL_11 { get; set; }
        public int DAY_ORD_VOL_12 { get; set; }
        public int DAY_ORD_VOL_13 { get; set; }
        public int DAY_ORD_VOL_14 { get; set; }
        public int DAY_ORD_VOL_15 { get; set; }
        public int DAY_ORD_VOL_16 { get; set; }
        public int DAY_ORD_VOL_17 { get; set; }
        public int DAY_ORD_VOL_18 { get; set; }
        public int DAY_ORD_VOL_19 { get; set; }
        public int DAY_ORD_VOL_20 { get; set; }
        public int DAY_ORD_VOL_21 { get; set; }
        public int DAY_ORD_VOL_22 { get; set; }
        public int DAY_ORD_VOL_23 { get; set; }
        public int DAY_ORD_VOL_24 { get; set; }
        public int DAY_ORD_VOL_25 { get; set; }
        public int DAY_ORD_VOL_26 { get; set; }
        public int DAY_ORD_VOL_27 { get; set; }
        public int DAY_ORD_VOL_28 { get; set; }
        public int DAY_ORD_VOL_29 { get; set; }
        public int DAY_ORD_VOL_30 { get; set; }
        public int DAY_ORD_VOL_31 { get; set; }



        public IList<OrderUpdateScreen> listData { get; set; }
    }

    public class DailyAllocationList
    {
        public string DYALOCRATIO { get; set; }
        public int VALUE { get; set; }
    }

    public class DailyHeaderList
    {
        public string Month { get; set; }
        public int Day { get; set; }
        public string WeekDay { get; set; }
    }
}