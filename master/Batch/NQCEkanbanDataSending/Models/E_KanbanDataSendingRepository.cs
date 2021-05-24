using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;
using NQCEkanbanDataSending.Models;

namespace NQCEkanbanDataSending.Models
{
    class E_KanbanDataSendingRepository 
    {
        #region singleton
        private static E_KanbanDataSendingRepository instance = null;
        public static E_KanbanDataSendingRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new E_KanbanDataSendingRepository();
                }
                return instance;
            }
        }
        #endregion
        public IList<NQCEkanban> GetNQCEKanban(IDBContext db, string comcd)
        {
            dynamic args = new
            {
                COMPANY_CD = comcd
            };
            IList<NQCEkanban> result = db.Fetch<NQCEkanban>("NQCEKanban", args);
            return result;

        }
    }
}
