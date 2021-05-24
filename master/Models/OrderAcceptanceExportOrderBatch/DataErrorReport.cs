using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPPSU.Models.OrderAcceptanceExportOrderBatch
{
    public class DataErrorReport
    {
        public string LOCALDATE { get; set; }
        public string OCN { get; set; }
        public string CFC { get; set; }
        public string PARTNO { get; set; }
        public string ORDERLOTSIZE { get; set; }
        public string STATUSCODE { get; set; }
        public string N { get; set; }
        public string NPLUS1 { get; set; }
        public string NPLUS2 { get; set; }
        public string NPLUS3 { get; set; }
        public string ERR_MESG { get; set; }

    }
}