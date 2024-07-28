
namespace Application.DTOs.Filters.Comments
{
    public record CommentFilter:SpecificationParams
    {
        public Guid ProductId { get; init; }
    }
}
