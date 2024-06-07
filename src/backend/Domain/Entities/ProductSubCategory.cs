using Domain.Common;
using Domain.Entities.Products;
using Domain.Entities.SubCategories;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductSubCategory : BaseEntity, IAggregateRoot, IDatedModification
    {
        public SubCategory SubCategory { get; set; }
        public Guid SubCategoryId { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
