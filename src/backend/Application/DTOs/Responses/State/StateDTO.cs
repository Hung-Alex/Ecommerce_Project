namespace Application.DTOs.Responses.State
{
    public record StateDTO : BaseDTO
    {
        public string Type { get; set; }
        public string Display { get; set; }
        public string Code { get; set; }
    }
}
