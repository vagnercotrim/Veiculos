using System.Collections.Generic;
using System.Text;
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
            StringBuilder builder = new StringBuilder();

            builder.Append(@"<script type=""text/javascript"">$(function () {toastr.options = {");
            builder.Append(String.Format(@"""closeButton"": ""{0}"", ", closeButton.ToString().ToLower()));
            builder.Append(String.Format(@" ""positionClass"": ""{0}"", ", positionClass));
            builder.Append(@" ""newestOnTop"": ""false"", ");
            builder.Append(@" ""onclick"": null, ");
            builder.Append(@" ""showDuration"": ""0"", ");
            builder.Append(@" ""hideDuration"": ""0"", ");
            builder.Append(@" ""timeOut"": ""0"", ");
            builder.Append(@" ""showMethod"": ""fadeIn"" ");
            builder.Append("};");

            foreach (Notice par in erros)
                builder.Append(ToScript(par));

            builder.Append("});</script>");

            return builder.ToString();
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