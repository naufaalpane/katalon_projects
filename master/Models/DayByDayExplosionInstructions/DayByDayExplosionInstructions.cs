using GPPSU.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPPSU.Models.DayByDayExplosionInstructions
{
    public class DayByDayExplosionInstructions : BaseModel
    {

        public IList<DayByDayExplosionInstructions> listData { get; set; }
        public string IMPORTER_CD { get; set; }
        public string EXPORTER_CD { get; set; }
        public string ORD_TYPE { get; set; }
        public string PACK_MONTH { get; set; }
        public string PACK_MONTH_FORMAT { get; set; }
        public string CFC { get; set; }
        public string RECEIVE_NO { get; set; }

        public string START_DATE { get; set; }
        public string YEAR { get; set; }
        public string MONTH {get; set; }
        public string STATUS_CD { get; set; }

        public string PROCESS_CD { get; set; }
        public string COMPANY_CD { get; set; }



    }
}