namespace Application.DTOs.Responses.Rattings
{
    public record RattingDTO:BaseDTO
    {
        public int Rate { get; init; }
        public string Description { get; init; }
        public Guid ProductId { get; init; }
    }
}
