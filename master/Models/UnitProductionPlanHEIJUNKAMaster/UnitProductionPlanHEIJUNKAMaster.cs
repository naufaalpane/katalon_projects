using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPPSU.Models.UnitProductionPlanHEIJUNKAMaster
{
    public class UnitProductionPlanHEIJUNKAMaster
    {
        public string COMPANY_CD { get; set; }
        public string PART_NO { get; set; }
        public string PART_NO1 { get; set; }
        public string PART_NO2 { get; set; }
        public string PART_NO3 { get; set; }
        public string LINE_CD { get; set; }
        public string CFC { get; set; }
        public string HEIJUNKA_CD { get; set; }
        public string HEIJUNKA_YN_CD { get; set; }
        public string SUM_SIGN { get; set; }
        
        public string CREATED_BY { get; set; }
        public string CREATED_DT { get; set; }
        public string CHANGED_BY { get; set; }
        public string CHANGED_DT { get; set; }
        public string CHANGED_DT_STRING { get; set; }

        public IList<UnitProductionPlanHEIJUNKAMaster> listData { get; set; }

        //LAST LINE
    }
}