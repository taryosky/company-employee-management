using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.RequestFeatures
{
    public class PagedList<T> : List<T>
    {
        public PaginationMetaData MetaData { get; set; }
        public PagedList(List<T> items, int count, int pageSize, int pageNumber)
        {
            //Initialize page metadata
            MetaData = new PaginationMetaData()
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            AddRange(items);
        }

        public static PagedList<T> Paginate(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageSize, pageNumber);
        }
    }
}
