using System;
using System.Text;
using System.Web.Mvc;
using Veiculos.Infra.NHibernate;

namespace Veiculos.Infra.HtmlHelpers
{
    public static class PaginacaoHelper
    {

        public static MvcHtmlString Paginas<T>(this HtmlHelper html, Paging<T> paging, Func<int, String> url)
        {
            StringBuilder paginas = new StringBuilder();
            paginas.Append(@"<ul class=""pagination"">");

            for (int i = 1; i <= paging.TotalPage; i++)
            {
                TagBuilder li = new TagBuilder("li");
                li.AddCssClass("pagination-sm");

                if (paging.PageNum == i)
                {
                    li.AddCssClass("active");
                }

                TagBuilder a = new TagBuilder("a");
                a.MergeAttribute("href", url(i));
                a.InnerHtml = i.ToString();

                li.InnerHtml = a.ToString();

                paginas.Append(li.ToString());
            }

            paginas.Append("</ul>");
            return MvcHtmlString.Create(paginas.ToString());
        }

    }
}