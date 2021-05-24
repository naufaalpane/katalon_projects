using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using GPPSU.Models.Common;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;
using GPPSU.Commons.Models;

using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using GPPSU.Commons.Controllers;
using System.IO;


namespace GPPSU.Models.DeliveryPackingPlanCreation
{
    public class DeliveryPackingPlanCreationRepository : GPPSU.Commons.Repositories.BaseRepo
    {

        #region singleton
        private static DeliveryPackingPlanCreationRepository instance = null;
        public static DeliveryPackingPlanCreationRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DeliveryPackingPlanCreationRepository();
                }
                return instance;
            }
        }
        #endregion singleton

        #region Search

        public IList<DeliveryPackingPlanCreation> Search(IDBContext db, DeliveryPackingPlanCreation data)
        {
            dynamic args = new
            {
                PROCESS_CD = data.PROCESS_CD
            };

            IList<DeliveryPackingPlanCreation> result = db.Fetch<DeliveryPackingPlanCreation>("DeliveryPackingPlanCreation/DeliveryPackingPlanCreation_Search", args);
            return result;
        }


        #endregion

    }
}