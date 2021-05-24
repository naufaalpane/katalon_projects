using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Toyota.Common.Database;
using GPPSU.Commons.Repositories;

namespace GPPSU.Models.OrderRecTentativeNQCInhouse
{
    public class OrderRecTentativeNQCInhouseRepo : BaseRepo
    {
        #region singleton
        private static OrderRecTentativeNQCInhouseRepo instance = null;
        public static OrderRecTentativeNQCInhouseRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderRecTentativeNQCInhouseRepo();
                }
                return instance;
            }
        }
        #endregion singleton

        #region cekDataTentativeNQCOrder

        public OrderRecTentativeNQCInhouse OrderCekTenNQC(IDBContext db, string COMPANY_CD, string VERSION, string userid)
        {
            OrderRecTentativeNQCInhouse result = new OrderRecTentativeNQCInhouse();
            try
            {
                dynamic args = new
                {
                    company = COMPANY_CD,
                    ver = VERSION,
                    userId = userid
                };

                 result = db.SingleOrDefault<OrderRecTentativeNQCInhouse>("OrderRecTentativeNQCInhouse/OrderRecTentativeNQCInhouse_Execute", args);
            }
            finally
            {
                db.Close();
            }

            return result;
        }
        #endregion



    }
}