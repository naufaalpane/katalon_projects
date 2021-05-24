using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPPSU.Commons.Controllers;
using System.IO;
using GPPSU.Models.OrderStatusListScreen;

using Toyota.Common.Database;
using Toyota.Common.Web.Platform;


namespace GPPSU.Controllers
{
    public class OrderStatusListScreenController : BaseController
    {
        public OrderStatusListScreenRepository orderRepo = OrderStatusListScreenRepository.Instance;
        public DatabaseManager databaseManager = DatabaseManager.Instance;

        protected override void Startup()
        {
            Settings.Title = "WA0AU101 - Order Status List Screen";
        }


        public void PrintLeave(string noreg, int seq)
        {
            // Get leave data.
            //Leave leave = LeaveRepository.Instance.GetLeaveByKey(
            //                noreg,
            //                seq);
            //string filename_template = null;
            string filename_template = Server.MapPath(Url.Content("~/Content/ReportTemplate/TemplatePrintLeave.htm"));
            try
            {
                TextReader reader = new StreamReader(filename_template);
               // Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                string filename = string.Format("PrintLeave_{0}", DateTime.Now.ToString("yyyyMMdd_HHmmss")).Replace("/", "-");
                String contentBody = reader.ReadToEnd();
                String contentTemp;
                MemoryStream ms = new MemoryStream();
               // PdfWriter PDFWriter = PdfWriter.GetInstance(doc, ms);
                //doc.Open();

                //setValue(ref contentBody, "[Noreg]", leave.NOREG);
                //setValue(ref contentBody, "[Nama]", leave.NAME);
                //setValue(ref contentBody, "[Divisi]", leave.DIV_NAME);
                //setValue(ref contentBody, "[Departemen]", leave.DEPT_NAME);
                //setValue(ref contentBody, "[Section]", leave.SECT_NAME);
                //setValue(ref contentBody, "[Line]", leave.LINE_NAME);
                //setValue(ref contentBody, "[Group]", leave.GROUP_NAME);
                //setValue(ref contentBody, "[KodePerijinan]", leave.LEAVE_TYPE);
                //setValue(ref contentBody, "[DeskripsiPerijinan]", leave.LEAVE_DESC);
                //setValue(ref contentBody, "[TanggalMulaiPerijinan]", String.Format("{0:dd-MM-yyyy}", leave.LEAVE_DATE_FROM));
                //setValue(ref contentBody, "[TanggalSelesaiPerijinan]", String.Format("{0:dd-MM-yyyy}", leave.LEAVE_DATE_TO));
                //setValue(ref contentBody, "[WaktuMulaiPerijinan]", leave.LEAVE_TIME_FROM);
                //setValue(ref contentBody, "[WaktuSelesaiPerijinan]", leave.LEAVE_TIME_TO);
                //setValue(ref contentBody, "[Alasan]", leave.LEAVE_REASON);
                //setValue(ref contentBody, "[TanggalPengajuan]", String.Format("{0:dd-MM-yyyy}", leave.CREATED_DT));
                //setValue(ref contentBody, "[DiajukanOleh]", leave.CREATED_BY);

                //setValue(ref contentBody, "[SectionHeadName]", leave.LEAVE_APP_SH_NAME);
                //setValue(ref contentBody, "[statusSH]", leave.LEAVE_APP_SH_STATUS != null ? leave.LEAVE_APP_SH_STATUS == "3" ? "Disetujui" : leave.LEAVE_APP_SH_STATUS == "2" ? "Ditolak" : string.Empty : string.Empty);
                //setValue(ref contentBody, "[TglWaktuPersetujuanSectionHead]", (leave.LEAVE_APP_SH_DATE != null ? Convert.ToDateTime(leave.LEAVE_APP_SH_DATE).ToString("dd-MM-yyyy hh:mm:ss") : string.Empty));
                //setValue(ref contentBody, "[DeptHeadName]", leave.LEAVE_APP_DPH_NAME);
                //setValue(ref contentBody, "[statusDpH]", leave.LEAVE_APP_DPH_STATUS != null ? leave.LEAVE_APP_DPH_STATUS == "3" ? "Disetujui" : leave.LEAVE_APP_DPH_STATUS == "2" ? "Ditolak" : string.Empty : string.Empty);
                //setValue(ref contentBody, "[TglWaktuPersetujuanDeptHead]", (leave.LEAVE_APP_DPH_DATE != null ? Convert.ToDateTime(leave.LEAVE_APP_DPH_DATE).ToString("dd-MM-yyyy hh:mm:ss") : string.Empty));
                //setValue(ref contentBody, "[DivHeadName]", leave.LEAVE_APP_DH_NAME);
                //setValue(ref contentBody, "[statusDH]", leave.LEAVE_APP_DH_STATUS != null ? leave.LEAVE_APP_DH_STATUS == "3" ? "Disetujui" : leave.LEAVE_APP_DH_STATUS == "2" ? "Ditolak" : string.Empty : string.Empty);
                //setValue(ref contentBody, "[TglWaktuPersetujuanDivHead]", (leave.LEAVE_APP_DH_DATE != null ? Convert.ToDateTime(leave.LEAVE_APP_DH_DATE).ToString("dd-MM-yyyy hh:mm:ss") : string.Empty));

                contentTemp = contentBody;
                //writeDetail(ref doc, ref contentTemp, PDFWriter.DirectContent);
                //}

                //doc.Close();
                reader.Close();
                byte[] result = ms.ToArray();

                SendToClientBrowser(filename, result);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }


        #region Search & Count
        public ActionResult Search(OrderStatusListScreen data, int currentPage, int rowsPerPage)
        {
            try
            {
                DoSearch(data, currentPage, rowsPerPage);
            }
            catch (Exception e)
            {
                return Json("Error: " + e.Message, JsonRequestBehavior.AllowGet);
            }

            return PartialView("_GridView");
        }

        public void DoSearch(OrderStatusListScreen data, int currentPage, int rowsPerPage)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();

            GPPSU.Models.Common.PagingModel paging = new GPPSU.Models.Common.PagingModel(orderRepo.SearchCount(db, data), currentPage, rowsPerPage);
            ViewData["paging"] = paging;

            IList<OrderStatusListScreen> orderStatusListData = orderRepo.Search(db, data, paging.StartData, paging.EndData);
            ViewData["orderStatusListData"] = orderStatusListData;
        }

        #endregion


    }


}