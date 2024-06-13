
namespace Application.DTOs.Filters
{
    public record SpecificationParams
    {
        public int PageSize { get; set; } = 10;

        public int PageNumber { get; set; } = 1;

        public string SortColoumn { get; set; } = "Id";

        public string SortBy { get; set; } = "ASC";
    }
}
