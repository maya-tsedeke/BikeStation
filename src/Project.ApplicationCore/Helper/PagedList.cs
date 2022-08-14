using Project.ApplicationCore.Entities.Models;

namespace Project.ApplicationCore.Helper
{
    public class PagedList<T> : List<T>
    {
        private List<BikeStation> items;
        private int count;
        private int pageNumber;

        public int CurrentPage { get; private set; }
        public int TotalPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPage;
        public PagedList(List<T> item, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalCount = count;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(item);
        }

        public PagedList(List<BikeStation> items, int count, int pageNumber, int pageSize)
        {
            this.items = items;
            this.count = count;
            this.pageNumber = pageNumber;
            PageSize = pageSize;
        }

        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
  

    }

}
