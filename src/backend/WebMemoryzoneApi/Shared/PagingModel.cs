using Domain.Interface;

namespace WebMemoryzoneApi.Shared
{
    public class PagingModel : IPagingParams
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string SortColoumn { get; set; }

        public string SortBy { get; set; }
    }
}
