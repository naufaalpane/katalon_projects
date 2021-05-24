using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPPSU.Commons.Models;

namespace GPPSU.Models.ProductionPlanPrintInstructions
{
    public class ProductionPlanPrintInstructions
    {
        public string LINE_CD { get; set; }
        public string COMPANY_CD { get; set; }
        public string PROCESS_CD { get; set; }
        public string LINE_NAME { get; set; }
        public string YYYYMM { get; set; }
        public IList<ProductionPlanPrintInstructions> listData { get; set; }
    }

}