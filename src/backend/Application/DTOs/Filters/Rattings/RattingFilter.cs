namespace Application.DTOs.Filters.Rattings
{
    public record RattingFilter : SpecificationParams
    {
        public Guid ProductId { get; init; }
    }
}
