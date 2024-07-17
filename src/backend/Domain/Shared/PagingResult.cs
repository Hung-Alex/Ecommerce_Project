

namespace Domain.Shared
{
    public class PagingResult<T> : Result<T> where T : class
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int PageNumber { get { return PageIndex + 1; } set { PageIndex = value - 1; } }
        public long TotalItems { get; private set; }
        public int TotalPages { get { var total = TotalItems / PageSize; if (TotalItems % PageSize > 0) { total++; } return (int)total; } private set { } }
        public PagingResult(T data, int pageNumber, int pageSize, long totalItems) : base(data)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
            IsSuccess=true;
        }
    }
}
