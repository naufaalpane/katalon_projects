using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyota.Common.Database;
using GPPSU.Models.OrderAcceptanceExportOrderBatch;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using GPPSU.Commons.Controllers;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;
using iText.Layout.Borders;
using iText.Kernel.Geom;
using GPPSU.Commons.Models;
using Toyota.Common.Web.Platform;

using GPPSU.Commons.Controllers;

namespace GPPSU.Controllers
{
    public class OrderRecTentativeFromPackingController : BaseController
    {
        public DatabaseManager databaseManager = DatabaseManager.Instance;
        protected override void Startup()
        {
            Settings.Title = "WA0AUE11 Order Rec. Tentative(from Packing)";
            GetCompany(); 
        }

     

        public void GetCompany()
        {
            string USERNAME = GetLoginUserId();
            IDBContext db = databaseManager.GetContext();


            string CompanyCD = db.SingleOrDefault<string>(@"
             select top 1 SEL_COMPANY from TB_R_SELECT_COMPANY where USER_ID = @Username
             ", new { Username = USERNAME });

            ViewData["Company_CD"] = CompanyCD;

        }


    }
}