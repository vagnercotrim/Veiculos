using System;
using System.Collections.Generic;

namespace Veiculos.Infra.NHibernate
{
    public class Paging<T>
    {

        public IEnumerable<T> List { get; private set; }
        public int PageSize { get; private set; }
        public int PageNum { get; private set; }
        public int TotalPage { get; private set; }
        public long TotalCount { get; private set; }

        public Paging(IEnumerable<T> list, int pageSize, int pageNum, long totalCount)
        {
            List = list;
            PageSize = pageSize;
            PageNum = pageNum;
            TotalPage = (int)Math.Ceiling(totalCount / (decimal)pageSize);
            TotalCount = totalCount;
        }

    }
}