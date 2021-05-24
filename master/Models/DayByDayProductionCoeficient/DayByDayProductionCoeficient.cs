using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPPSU.Models.DayByDayProductionCoeficient
{
    public class DayByDayProductionCoeficient
    {
        public string COMPANY_CD { get; set; }
        public string PROCESS_CD { get; set; }
        public string YYMM { get; set; }
        public string LINE_CD { get; set; }
        public string WORK_DAY_SYSTEM { get; set; }
        public string WORK_DAY_HAND { get; set; }

        public string DAY_ALOC_RATIO_01 { get; set; }
        public string DAY_ALOC_RATIO_02 { get; set; }
        public string DAY_ALOC_RATIO_03 { get; set; }
        public string DAY_ALOC_RATIO_04 { get; set; }
        public string DAY_ALOC_RATIO_05 { get; set; }
        public string DAY_ALOC_RATIO_06 { get; set; }
        public string DAY_ALOC_RATIO_07 { get; set; }
        public string DAY_ALOC_RATIO_08 { get; set; }
        public string DAY_ALOC_RATIO_09 { get; set; }
        public string DAY_ALOC_RATIO_10 { get; set; }
        public string DAY_ALOC_RATIO_11 { get; set; }
        public string DAY_ALOC_RATIO_12 { get; set; }
        public string DAY_ALOC_RATIO_13 { get; set; }
        public string DAY_ALOC_RATIO_14 { get; set; }
        public string DAY_ALOC_RATIO_15 { get; set; }
        public string DAY_ALOC_RATIO_16 { get; set; }
        public string DAY_ALOC_RATIO_17 { get; set; }
        public string DAY_ALOC_RATIO_18 { get; set; }
        public string DAY_ALOC_RATIO_19 { get; set; }
        public string DAY_ALOC_RATIO_20 { get; set; }
        public string DAY_ALOC_RATIO_21 { get; set; }
        public string DAY_ALOC_RATIO_22 { get; set; }
        public string DAY_ALOC_RATIO_23 { get; set; }
        public string DAY_ALOC_RATIO_24 { get; set; }
        public string DAY_ALOC_RATIO_25 { get; set; }
        public string DAY_ALOC_RATIO_26 { get; set; }
        public string DAY_ALOC_RATIO_27 { get; set; }
        public string DAY_ALOC_RATIO_28 { get; set; }
        public string DAY_ALOC_RATIO_29 { get; set; }
        public string DAY_ALOC_RATIO_30 { get; set; }
        public string DAY_ALOC_RATIO_31 { get; set; }

        public decimal D_WORK_DAY_SYSTEM { get; set; }
        public decimal D_WORK_DAY_HAND { get; set; }

        public decimal D_DAY_ALOC_RATIO_01 { get; set; }
        public decimal D_DAY_ALOC_RATIO_02 { get; set; }
        public decimal D_DAY_ALOC_RATIO_03 { get; set; }
        public decimal D_DAY_ALOC_RATIO_04 { get; set; }
        public decimal D_DAY_ALOC_RATIO_05 { get; set; }
        public decimal D_DAY_ALOC_RATIO_06 { get; set; }
        public decimal D_DAY_ALOC_RATIO_07 { get; set; }
        public decimal D_DAY_ALOC_RATIO_08 { get; set; }
        public decimal D_DAY_ALOC_RATIO_09 { get; set; }
        public decimal D_DAY_ALOC_RATIO_10 { get; set; }
        public decimal D_DAY_ALOC_RATIO_11 { get; set; }
        public decimal D_DAY_ALOC_RATIO_12 { get; set; }
        public decimal D_DAY_ALOC_RATIO_13 { get; set; }
        public decimal D_DAY_ALOC_RATIO_14 { get; set; }
        public decimal D_DAY_ALOC_RATIO_15 { get; set; }
        public decimal D_DAY_ALOC_RATIO_16 { get; set; }
        public decimal D_DAY_ALOC_RATIO_17 { get; set; }
        public decimal D_DAY_ALOC_RATIO_18 { get; set; }
        public decimal D_DAY_ALOC_RATIO_19 { get; set; }
        public decimal D_DAY_ALOC_RATIO_20 { get; set; }
        public decimal D_DAY_ALOC_RATIO_21 { get; set; }
        public decimal D_DAY_ALOC_RATIO_22 { get; set; }
        public decimal D_DAY_ALOC_RATIO_23 { get; set; }
        public decimal D_DAY_ALOC_RATIO_24 { get; set; }
        public decimal D_DAY_ALOC_RATIO_25 { get; set; }
        public decimal D_DAY_ALOC_RATIO_26 { get; set; }
        public decimal D_DAY_ALOC_RATIO_27 { get; set; }
        public decimal D_DAY_ALOC_RATIO_28 { get; set; }
        public decimal D_DAY_ALOC_RATIO_29 { get; set; }
        public decimal D_DAY_ALOC_RATIO_30 { get; set; }
        public decimal D_DAY_ALOC_RATIO_31 { get; set; }

        public IList<DailyAllocationList> DAILYRATIOLIST { get; set; }
        public IList<DailyHeaderList> DailyList { get; set; }

        public IList<DayByDayProductionCoeficient> listData { get; set; }
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