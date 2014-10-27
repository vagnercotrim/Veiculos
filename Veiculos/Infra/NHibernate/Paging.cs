using System.Collections.Generic;

namespace Veiculos.Infra.NHibernate
{
    public class Paging<T>
    {

        public IList<T> List { get; private set; }
        public int PageSize { get; private set; }
        public int PageNum { get; private set; }
        public int TotalPage { get; private set; }
        public long TotalCount { get; private set; }

        public Paging(IList<T> list, int pageSize, int pageNum, int totalPage, long totalCount)
        {
            List = list;
            PageSize = pageSize;
            PageNum = pageNum;
            TotalPage = totalPage;
            TotalCount = totalCount;
        }

    }
}