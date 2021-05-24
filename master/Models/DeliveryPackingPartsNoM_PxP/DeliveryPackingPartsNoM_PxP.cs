using GPPSU.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPPSU.Models.DeliveryPackingPartsNoM_PxP
{
    public class DeliveryPackingPartsNoM_PxP : BaseModel
    {
        public IList<DeliveryPackingPartsNoM_PxP> DPPartsNoM_PxPCollection { get; set; }
        public string DEST_CD { get; set; }
        public string CFC { get; set; }
        public string PART_NO { get; set; }
        public string PART_NO_ONE { get; set; }
        public string PART_NO_TWO { get; set; }
        public string PART_NO_THREE { get; set; }

        public string STATUS_CD { get; set; }
        public string COMPANY_CD { get; set; }
        public string PART_NAME { get; set; }
        public string PROCESS_CD { get; set; }
        public string LOT_SIZE { get; set; }
        public string SEQ_NO { get; set; }
        public string SEL_MATCH_RATIO { get; set; }
        public string TC_FROM { get; set; }
        public string TC_TO { get; set; }

    }
}