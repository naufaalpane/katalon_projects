using GPPSU.Commons.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Toyota.Common.Database;

namespace GPPSU.Models.OrderVolumeDifferentListCreation
{
    public class OrderVolumeDifferentListCreationRepo : BaseRepo
    {
        #region singleton
        private static OrderVolumeDifferentListCreationRepo instance = null;
        public static OrderVolumeDifferentListCreationRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderVolumeDifferentListCreationRepo();
                }
                return instance;
            }
        }

        internal IList<OrderVolumeDifferentListCreation> GetOrderInformation(IDBContext db, string COMPANY_CD)
        {
            IList<OrderVolumeDifferentListCreation> result = new List<OrderVolumeDifferentListCreation>();
            result = db.Fetch<OrderVolumeDifferentListCreation>("OrderVolumeDifferentListCreationController/GetVolumeInformation", new { company_cd = COMPANY_CD });
            return result;
        }

        internal IList<VolumeDifferentList> GetOrderDifferentList(IDBContext db, OrderVolumeDifferentListCreation volumeInfo)
        {
            IList<VolumeDifferentList> result = new List<VolumeDifferentList>();
            dynamic args = new
            {
                company_code = volumeInfo.COMPANY_CD,
                importer_cd  = volumeInfo.DESTINATION_CD,
                version      = volumeInfo.VERSION,
                revision     = volumeInfo.REVISION_NO,
                ord_type     = volumeInfo.ORDER_TYPE,
                cfc          = volumeInfo.CAR_FAMILY_CODE
            };
            result = db.Fetch<VolumeDifferentList>("OrderVolumeDifferentListCreationController/GetDifferentListVolume", args);
            return result;
        }

        internal string recordToBatchStatus(IDBContext db, string COMPANY_CD, string status_batch)
        {
            string result = null;
            dynamic args = new
            {
                company_cd = COMPANY_CD,
                batch_id = "U01-019",
                class_name = "Order Difference List",
                batch_name = "Order Volume Difference List",
                status = status_batch,
                start_time = DateTime.Now,
                changed_dt = DateTime.Now,
                changed_by = "USER"
            };
            result = db.SingleOrDefault<string>("OrderVolumeDifferentListCreationController/RecordToBatchStatus", args);
            return result;
        }

        #endregion singleton
    }
}