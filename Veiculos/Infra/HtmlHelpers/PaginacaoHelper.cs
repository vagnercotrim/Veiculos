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
                TagBuilder li = Li(paging, i, A(url, i));
                paginas.Append(li);
            }

            paginas.Append("</ul>");
            return MvcHtmlString.Create(paginas.ToString());
        }

        private static TagBuilder Li<T>(Paging<T> paging,int i, TagBuilder tag)
        {
            TagBuilder li = new TagBuilder("li");
            li.AddCssClass("pagination-sm");

            if (paging.PageNum == i)
                li.AddCssClass("active");
            
            li.InnerHtml = tag.ToString();

            return li;
        }

        private static TagBuilder A(Func<int, string> url, int i)
        {
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", url(i));
            a.InnerHtml = i.ToString();

            return a;
        }
    }
}