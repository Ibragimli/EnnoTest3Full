using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnnoTest3.Models
{
    public class PagenatedList<T>:List<T>
    {
        public PagenatedList(List<T> items,int count, int pageindex,int pagesize)
        {
            this.AddRange(items);
            PageIndex = pageindex;
            TotalPages = (int)Math.Ceiling(count / (double)pagesize);
        }

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public bool HasPrev
        {
            get => PageIndex > 1;
        }

        public bool HasNext
        {
            get => TotalPages > PageIndex;
        }

        public static PagenatedList<T> Create(List<T> items,int pageindex,int pagesize)
        {
            var Items = items.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return new PagenatedList<T>(Items, items.Count, pageindex, pagesize);
        }
    }
}
