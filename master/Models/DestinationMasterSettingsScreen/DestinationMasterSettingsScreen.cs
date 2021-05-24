using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPPSU.Commons.Models;

namespace GPPSU.Models.DestinationMasterSettingsScreen
{
    public class DestinationMasterSettingsScreen
    {
        public string COMPANY_CD { get; set; }
        public string SEQ_NO { get; set; }
        public string DESTINATION_CODE { get; set; }
        public string DESTINATION_NAME { get; set; }
        public string EXPORT_CODE { get; set; }
        public string LEAD_TIME { get; set; }
        public string E_KANBAN { get; set; }
        public string TC_FROM { get; set; }
        public string TC_TO { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DT { get; set; }
        public string CREATED_DT_STR { get; set; }
        public IList<DestinationMasterSettingsScreen> listData { get; set; }
    }

}