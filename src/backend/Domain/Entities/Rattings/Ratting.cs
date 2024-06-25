using Domain.Common;
using Domain.Entities.Products;
using Domain.Shared;

namespace Domain.Entities.Rattings
{
    public class Ratting : BaseEntity, IAggregateRoot, IDatedModification
    {
        public Ratting() : base() { }
        public int Rate { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
