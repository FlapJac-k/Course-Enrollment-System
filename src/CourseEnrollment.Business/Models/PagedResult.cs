namespace CourseEnrollment.Business.Models
{
    public class PagedResult<T>(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
    {
        public IEnumerable<T> Items { get; } = items;
        public int TotalCount { get; } = totalCount;
        public int PageNumber { get; } = pageNumber;
        public int PageSize { get; } = pageSize;
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;
    }
}

