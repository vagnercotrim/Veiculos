using System.Collections.Generic;
using Veiculos.Infra.Extensions;
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

        private static MvcHtmlString Toastr(this HtmlHelper htmlHelper, Notice erro, bool closeButton, String positionClass)
        {
            IList<Notice> erros = new List<Notice>();
            erros.Add(erro);

            return Toastr(htmlHelper, erros, closeButton, positionClass);
        }

        private static MvcHtmlString Toastr(this HtmlHelper htmlHelper, IList<Notice> erros, bool closeButton, String positionClass)
        {
            if (erros.IsNullOrEmpty())
                return MvcHtmlString.Empty;

            return MvcHtmlString.Create(BuildScript(erros, closeButton, positionClass));
        }

        private static string BuildScript(IEnumerable<Notice> erros, bool closeButton, String positionClass)
        {
            String toastrOptions = String.Format(@"""closeButton"": ""{0}"",""positionClass"": ""{1}"",""newestOnTop"": ""false"",""onclick"": null,""showDuration"": ""0"",""hideDuration"": ""0"",""timeOut"": ""0"",""showMethod"": ""fadeIn""",
                                                  closeButton.ToString().ToLower(), positionClass);
            String notices = "";

            foreach (Notice par in erros)
                notices += ToScript(par);

            return @"<script type=""text/javascript"">$(function () {toastr.options = {" + toastrOptions + "};" + notices + "});</script>";
        }

        private static string ToScript(Notice notice)
        {
            if (notice.Title != null)
                return String.Format("toastr.{0}('{1}','{2}');", notice.Type.ToString().ToLower(), notice.Message, notice.Title);

            return String.Format("toastr.{0}('{1}');", notice.Type.ToString().ToLower(), notice.Message);
        }

        public static MvcHtmlString Toastr(this HtmlHelper htmlHelper, bool closeButton, String positionClass)
        {
            return Toastr(htmlHelper, GetNoticesFromTempData(htmlHelper), closeButton, positionClass);
        }

    }
}