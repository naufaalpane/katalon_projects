using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Toyota.Common.Database;
using Toyota.Common.Web.Platform;

namespace GPPSU.Models.OrderStatusListScreen
{
    public class OrderStatusListScreenRepository : GPPSU.Commons.Repositories.BaseRepo
    {

        #region singleton
        private static OrderStatusListScreenRepository instance = null;
        public static OrderStatusListScreenRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderStatusListScreenRepository();
                }
                return instance;
            }
        }
        #endregion singleton

        #region Search & Count
        public IList<OrderStatusListScreen> Search(IDBContext db, OrderStatusListScreen data, long currentPage, long rowsPerPage)

        {
            dynamic args = new
            {
                STATUS_CD = data.STATUS_CD,
                IMPORTER_CD = data.IMPORTER_CD,
                EXPORTER_CD = data.EXPORTER_CD,
                ORD_TYPE = data.ORD_TYPE,
                PACK_MONTH = data.PACK_MONTH,
                CFC = data.CFC,
                RowStart = currentPage,
                RowEnd = rowsPerPage
            };

            IList<OrderStatusListScreen> result = db.Fetch<OrderStatusListScreen>("OrderStatusListScreen/OrderStatusListScreen_Search", args);
                return result;
        }

        public int SearchCount(IDBContext db, OrderStatusListScreen data)
        {
            dynamic args = new
            {
                STATUS_CD = data.STATUS_CD,
                IMPORTER_CD = data.IMPORTER_CD,
                EXPORTER_CD = data.EXPORTER_CD,
                ORD_TYPE = data.ORD_TYPE,
                PACK_MONTH = data.PACK_MONTH,
                CFC = data.CFC
            };

            int result = db.SingleOrDefault<int>("OrderStatusListScreen/OrderStatusListScreen_SearchCount", args);
            return result;
        }
        #endregion

    }
}