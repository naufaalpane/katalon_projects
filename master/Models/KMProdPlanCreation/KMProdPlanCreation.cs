using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPPSU.Commons.Models;

namespace GPPSU.Models.KMProdPlanCreation
{
    public class KMProdPlanCreation
    {
        public string LINE_CD { get; set; }
        public string DDMMYYYY { get; set; }
       public string COMPANY_CD { get; set; }
        public string PROCESS_CD { get; set; }
        public string LINE_NAME { get; set; }
        public IList<KMProdPlanCreation> listData { get; set; }
    }

}