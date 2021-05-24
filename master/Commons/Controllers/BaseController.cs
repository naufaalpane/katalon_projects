using Toyota.Common.Web.Platform;
using System.Collections.Generic;
using GPPSU.Commons.Models;
using Toyota.Common.Credential;
using System.Web.Configuration;
using System.Configuration;
using System.Web;
using System;
using System.IO;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace GPPSU.Commons.Controllers
{
    public class BaseController : PageController
    {
        protected User GetLoginUser()
        {
            return Lookup.Get<User>();
        }

        protected string GetLoginUserId()
        {
            return GetLoginUser().Username;
        }

        protected void CopyPropertiesRepoToAjaxResult(RepoResult source, AjaxResult dest)
        {
            if (source == null)
            {
                dest = null;
                return;
            }

            if (dest != null && source != null)
            {
                dest.Result = source.Result;
                dest.ProcessId = source.ProcessId;
                dest.SuccMesgs = source.SuccMesgs;
                dest.ErrMesgs = source.ErrMesgs;
                dest.Params = source.Params;
            }
        }

        public ActionResult ShowInfoMessagesSuccess(List<string> messages)
        {
            ViewData["Messages"] = messages;
            return PartialView("~/Views/Shared/_MessagePanel.cshtml");
        }

        public ActionResult ShowErrorMessages(string messages)
        {
            List<string> ErrorMessages = new List<string>();
            Response.StatusCode = 400;
            ErrorMessages.Add(messages);
            ViewData["Messages"] = ErrorMessages;
            return PartialView("~/Views/Shared/_MessagePanel.cshtml");
        }

        protected void SendDataAsAttachment(string fileName, byte[] data)
        {
            Response.Clear();
            //Response.ContentType = result.MimeType;
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.Expires = -1;
            Response.Buffer = true;

            Response.ContentType = "application/octet-stream";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.ContentType = "application/vnd.ms-excel";

            Response.AddHeader("Content-Length", Convert.ToString(data.Length));

            Response.AddHeader("Content-Disposition", string.Format("{0};FileName=\"{1}\"", "attachment", fileName));
            Response.AddHeader("Set-Cookie", "fileDownload=true; path=/");

            Response.BinaryWrite(data);
            Response.End();
        }

        public FileStreamResult SendToClientBrowser(string _FileName, byte[] hasil)
        {
            MemoryStream output = new MemoryStream();
            Response.ClearContent();
            Response.AddHeader("content-disposition", String.Format("attachment; filename={0}.pdf", _FileName));
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.OutputStream.Write(hasil, 0, hasil.Length);
            //ms.Position = 0;
            Response.End();

            return File(output, "application/pdf");
        }

        // Summary:
        //     Retrieves Max request length
        //
        // Returns:
        //     Max request length in KiloBytes
        //     Can be set on web.config :
        //      <httpRuntime maxRequestLength="10240"/> in KB
        //
        protected int? GetMaxRequestLength()
        {
            int? maxRequestLength = null;
            HttpRuntimeSection section = ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
            if (section != null)
            {
                maxRequestLength = section.MaxRequestLength;
            }

            return maxRequestLength;
        }

        // Summary:
        //     Retrieves Max request length
        //
        // Returns:
        //     Max request length in Bytes
        //     Can be set on web.config :
        //      <httpRuntime maxRequestLength="10240"/> in KB
        //
        protected int? GetMaxRequestLengthInBytes()
        {
            int? maxRequestLength = GetMaxRequestLength();

            return maxRequestLength != null ? maxRequestLength * 1024 : null;
        }

        public string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }
    }
    public class Time_Control
    {
        public DateTime tcFrom { get; set; }
        public DateTime tcTo { get; set; }
    }
}