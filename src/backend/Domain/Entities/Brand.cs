using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Brand : BaseEntity, IDatedModification
    {
        private Brand():base()
        {
            
        }
        
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string LogoImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public IList<Product> Products { get; set; }
    }
}
