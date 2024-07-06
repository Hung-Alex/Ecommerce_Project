
namespace Application.DTOs.Responses.Comments
{
    public record CommentDTO : BaseDTO
    {
        public Guid? ParentId { get; set; }
        public string Content { get; set; }
        public virtual IEnumerable<CommentDTO> Replies { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid PostId { get; set; }
    }
}
