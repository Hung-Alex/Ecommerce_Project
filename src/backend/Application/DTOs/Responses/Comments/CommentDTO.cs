
using Domain.Common;

namespace Application.DTOs.Responses.Comments
{
    public record CommentDTO : BaseDTO, IHasImageDTO, ICreatedByDTO
    {
        public Guid? ParentId { get; set; }
        public string Content { get; set; }
        //public virtual IEnumerable<CommentDTO> Replies { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid PostId { get; set; }
        public string CreatedByName { get; set; }
        public string ImageOfCreator { get; set; }
    }
}
