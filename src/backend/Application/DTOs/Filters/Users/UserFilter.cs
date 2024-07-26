namespace Application.DTOs.Filters.Users
{
    public record UserFilter : SpecificationParams
    {
        public string? Name { get; init; }//first name
    }
}
