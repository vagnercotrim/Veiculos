using System.Collections.Generic;
using FluentNHibernate.Conventions;
using FluentNHibernate.Utils;
using Veiculos.Infra.Notice;

namespace System.Web.Mvc
{
    public static class ToastrHelper
    {

        public static IList<Notice> GetNoticesFromTempData(this HtmlHelper htmlHelper)
        {
            var notices = htmlHelper.ViewContext.Controller.TempData["notices"];

            if (notices == null)
                return new List<Notice>();

            return (IList<Notice>)notices;
        }

        private static MvcHtmlString Toastr(this HtmlHelper htmlHelper, IList<Notice> erros)
        {
            if (erros.IsNotEmpty())
            {
                var html = BuildScript(erros);

                return MvcHtmlString.Create(html);
            }

            return MvcHtmlString.Empty;
        }
        
        private static string BuildScript(IList<Notice> erros)
        {
            String toastrOptions = @"toastr.options = {""closeButton"": true,""positionClass"": ""toast-bottom-right"",""onclick"": null,""showDuration"": ""0"",""hideDuration"": ""0"",""timeOut"": ""0"",""showMethod"": ""fadeIn""}";
            String html = @"<script type=""text/javascript"">$(function () {" + toastrOptions + ";#notices#});</script>";

            String notices = "";

            foreach (Notice par in erros)
                notices += ToScript(par);
            
            return html.Replace("#notices#", notices);
        }

        private static string ToScript(Notice notice)
        {
            if(notice.Title != null)
                return String.Format("toastr.{0}('{1}','{2}');", notice.Type.ToString().ToLower(), notice.Message, notice.Title);

            return String.Format("toastr.{0}('{1}');", notice.Type.ToString().ToLower(), notice.Message);
        }

        public static MvcHtmlString Toastr(this HtmlHelper htmlHelper)
        {
            return Toastr(htmlHelper, GetNoticesFromTempData(htmlHelper));
        }

    }
}