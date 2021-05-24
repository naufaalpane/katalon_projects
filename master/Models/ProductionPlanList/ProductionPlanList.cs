using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPPSU.Commons.Models;

namespace GPPSU.Models.ProductionPlanList
{
    public class ProductionPlanList
    {
        public string LINE_CD { get; set; }
        public string COMPANY_CD { get; set; }
        public string PROCESS_CD { get; set; }
        public string LINE_NAME { get; set; }
        public string START_YYYYMM { get; set; }
        public string END_YYYYMM { get; set; }
        public IList<ProductionPlanList> listData { get; set; }
    }

}