using System.Collections.Generic;
using System.Text;
using Veiculos.Infra.Notice;

namespace System.Web.Mvc
{
    public static class ToastrHelper
    {

        public static IList<Notice> GetNoticesFromTempData(this HtmlHelper htmlHelper)
        {
            var notices = htmlHelper.ViewContext.Controller.TempData["notices"] as IList<Notice>;

            if (notices == null)
                return new List<Notice>();

            return notices;
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

            builder.Append(@"<script type=""text/javascript"">");
            builder.Append("$(function () {toastr.options = {");
            builder.Append(BuildOptions("closeButton", closeButton));
            builder.Append(BuildOptions("positionClass", positionClass));
            builder.Append(BuildOptions("newestOnTop", false));
            builder.Append(@" ""onclick"": null, ");
            builder.Append(@" """": ""0"", ");
            builder.Append(@" ""hideDuration"": ""0"", ");
            builder.Append(@" ""timeOut"": ""0"", ");
            builder.Append(@" ""showMethod"": ""fadeIn"" ");
            builder.Append("};");

            foreach (Notice par in erros)
                builder.Append(ToScript(par));

            builder.Append("});</script>");

            return builder.ToString();
        }

        private static string BuildOptions(String option, object closeButton)
        {
            return String.Format(@" ""{0}"": ""{1}"", ", option, closeButton.ToString().ToLower());
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