using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPPSU.Models.OrderVolumeDifferentListCreation
{
    public class OrderVolumeDifferentListCreation
    {
        public string COMPANY_CD { get; set; }
        public string DESTINATION_CD { get; set; }
        public string VERSION { get; set; }
        public string VERSION_NAME { get; set; }
        public string REVISION_NO { get; set; }
        public string ORDER_TYPE { get; set; }
        public string CAR_FAMILY_CODE { get; set; }
        public IList<VolumeDifferentList> listDifferent { get; set; }
    }

    public class VolumeDifferentList
    {
        public string CAR_FAMILY_CODE { get; set; }
        public string PART_NO { get; set; }
        public string NMONTH { get; set; }
        public string NMONTH_1 { get; set; }
        public string NMONTH_2 { get; set; }
        public string NMONTH_3 { get; set; }
        public int PREV_N { get; set; }
        public int NOW_N { get; set; }
        public int DIFF_N { get; set; }
        public int PREV_N_1 { get; set; }
        public int NOW_N_1 { get; set; }
        public int DIFF_N_1 { get; set; }
        public int PREV_N_2 { get; set; }
        public int NOW_N_2 { get; set; }
        public int DIFF_N_2 { get; set; }
        public int PREV_N_3 { get; set; }
        public int NOW_N_3 { get; set; }
        public int DIFF_N_3 { get; set; }
    }
}