using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPPSU.Models.LineMasterSettingsScreen
{
    public class LineMasterSettingsScreen
    {

        public string COMPANY_CD { get; set; }
        public string LINE_CD { get; set; }
        public long SEQ_NO { get; set; }
        public string TC_FROM { get; set; }
        public string TC_TO { get; set; }
        public string LINE_NAME { get; set; }
        public string PROCESS_CD { get; set; }

        public string CREATED_BY { get; set; }
        public string CREATED_DT { get; set; }
        public string CHANGED_BY { get; set; }
        public string CHANGED_DT { get; set; }

        public string CREATED_DT_STR { get; set; }
        public string CHANGED_DT_STR { get; set; }

        public IList<LineMasterSettingsScreen> listData { get; set; }

    }
}