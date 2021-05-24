using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPPSU.Models.MCapacityMasterSetUp
{
    public class MCapacityMasterSetUp
    {
        public string COMPANY_CD { get; set; }
        public string LINE_CD { get; set; }
        public string HEIJUNKA_CD { get; set; }
        public string SEQ_NO { get; set; }
        public string TC_FROM { get; set; }
        public string TC_TO { get; set; }
        public string CAPACITY { get; set; }
        public string LINE_NAME { get; set; }
        public string PROCESS_CD { get; set; }
        public string CREATED_BY { get; set; }
        public string CREATED_DT { get; set; }
        public string CHANGED_BY { get; set; }
        public string CHANGED_DT { get; set; }
        public string CREATED_DT_STRING { get; set; }
        public string CHANGED_DT_STRING { get; set; }

        public IList<MCapacityMasterSetUp> listData { get; set; }

    }
}