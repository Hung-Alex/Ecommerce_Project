namespace Application.DTOs.Responses.Images
{
    public record ImageDTO:BaseDTO
    {
        public string ImageUrl { get; set; }
        public string ImageExtension { get; set; }
    }
}
