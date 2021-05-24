using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPPSU.Models.UnitProductionControlMasterSettings
{
    public class UnitProductionControlMasterSettings
    {
        public string COMPANY_CD { get; set; }
        public string CFC { get; set; }
        public string PART_NO { get; set; }
        public string PART_NO2 { get; set; }
        public string PART_NO3 { get; set; }
        public string LINE_CD { get; set; }
        public string SEQ_NO { get; set; }
        public string STATUS_CD { get; set; }
        public string PART_NAME { get; set; }
        public string UNIT_SIGN { get; set; }
        public string TC_FROM { get; set; }
        public string TC_TO { get; set; }
        public string CREATED_BY { get; set; }
        public string CREATED_DT { get; set; }
        public string CHANGED_BY { get; set; }
        public string CHANGED_DT { get; set; }
        public string CREATED_DT_STR { get; set; }
        public string CHANGED_DT_STR { get; set; }

        public IList<UnitProductionControlMasterSettings> listData { get; set; }

    }
}