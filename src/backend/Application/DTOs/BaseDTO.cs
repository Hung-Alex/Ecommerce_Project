namespace Application.DTOs
{
    public record BaseDTO()
    {
        public Guid Id { get; set; }
        //public DateTimeOffset CreatedAt { get; set; }
        //public DateTimeOffset UpdatedAt { get; set; }
        //public Guid? CreatedByUserId { get; set; }
        //public Guid? UpdatedByUserId { get; set; }
    };
}
