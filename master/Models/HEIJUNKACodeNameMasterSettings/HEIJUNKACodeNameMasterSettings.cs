using GPPSU.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPPSU.Models.HEIJUNKACodeNameMasterSettings
{
     public class HEIJUNKACodeNameMasterSettings : BaseModel
        {
            public IList<HEIJUNKACodeNameMasterSettings> listData { get; set; }
            public string HEIJUNKA_CD { get; set; }
            public string COMPANY_CD { get; set; }
            public string HEIJUNKA_NAME { get; set; }


        
    }
}