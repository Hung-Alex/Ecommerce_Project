namespace Application.DTOs.Responses.Category
{
    public record CategoryDTO(string Name, string Description, string UrlSlug, Guid? ParrentId) : BaseDTO()
    {
        public string Image { get; init; }
        public IEnumerable<CategoryDTO> SubCategories { get; init; }
    }
}
