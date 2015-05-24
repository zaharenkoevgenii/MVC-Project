using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Utils
{
    public class HtmlResult : ActionResult
    {
        private readonly string content;
        public HtmlResult(string content)
        {
            this.content = content;
        }
        public HtmlResult(string content,string id)
        {
            this.content = @"<table id = """+id+@"""><tr><td>"+content+"</td></tr></table>";
        }
        public override void ExecuteResult(ControllerContext context)
        {
            var resultHtml = new StringBuilder("<!DOCTYPE html>");
            resultHtml.Append("<html>");
            resultHtml.Append("<head>");
            resultHtml.Append("<meta name='viewport' content='width=device-width' />");
            resultHtml.Append("<title>Index</title>");
            resultHtml.Append("</head>");
            resultHtml.Append("<body>");
            resultHtml.Append(content);
            resultHtml.Append("</body></html>");
            context.HttpContext.Response.Write(resultHtml.ToString());
        }
    }
}
