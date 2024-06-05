using Domain.Entities.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Shared;

namespace Domain.Entities.Products
{
    public class ProductImages : BaseEntity, IDatedModification
    {
        public ProductImages() { }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public Image Image { get; set; }
        public Guid ImageId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

