using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPPSU.Models.E_KanbanDataSending
{
    public class E_KanbanDataSending
    {
        public string COMPANY_CD { get; set; }
        public string SEQ_NO { get; set; }
        public string DESTINATION_CODE { get; set; }
        public string DESTINATION_NAME { get; set; }
        public string EXPORT_CODE { get; set; }
        public IList<E_KanbanDataSending> ListData { get; set; }
    }
}