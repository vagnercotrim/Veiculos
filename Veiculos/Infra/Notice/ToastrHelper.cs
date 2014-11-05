using System.Collections.Generic;
using FluentNHibernate.Conventions;
using FluentNHibernate.Utils;
using Veiculos.Infra.Notice;

namespace System.Web.Mvc
{
    public static class ToastrHelper
    {

        public static IDictionary<String, NoticeType> GetNoticesFromTempData(this HtmlHelper htmlHelper)
        {
            var notices = htmlHelper.ViewContext.Controller.TempData["notices"];

            if (notices == null)
                return new Dictionary<String, NoticeType>();

            return (Dictionary<String, NoticeType>)notices;
        }

        private static MvcHtmlString Toastr(this HtmlHelper htmlHelper, IDictionary<String, NoticeType> erros)
        {
            if (erros.IsNotEmpty())
            {
                var html = BuildScript(erros);

                return MvcHtmlString.Create(html);
            }

            return MvcHtmlString.Empty;
        }

        private static string BuildScript(IDictionary<string, NoticeType> erros)
        {
            String toastrOptions = @"toastr.options = {""closeButton"": true,""positionClass"": ""toast-bottom-right"",""onclick"": null,""showDuration"": ""0"",""hideDuration"": ""0"",""timeOut"": ""0"",""showMethod"": ""fadeIn""}";
            String html = @"<script type=""text/javascript"">$(function () {" + toastrOptions + ";#notices#});</script>";
            
            String notices = "";

            foreach (KeyValuePair<string, NoticeType> par in erros)
                notices += String.Format("toastr.{0}('{1}');", par.Value.ToLowerInvariantString(), par.Key);
            
            return html.Replace("#notices#", notices);
        }

        public static MvcHtmlString Toastr(this HtmlHelper htmlHelper)
        {
            return Toastr(htmlHelper, GetNoticesFromTempData(htmlHelper));
        }

    }
}