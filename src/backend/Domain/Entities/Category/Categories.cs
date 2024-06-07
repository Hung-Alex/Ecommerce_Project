using Domain.Common;
using Domain.Entities.Products;
using Domain.Entities.SubCategories;
using Domain.Shared;
using System.Collections.ObjectModel;

namespace Domain.Entities.Category
{
    public class Categories : BaseEntity, IDatedModification, IAggregateRoot
    {
        public Categories() : base() { }
        private Categories(string name, string descripton, string urlSlug, string image) : base()
        {
            Name = name ?? throw new ArgumentNullException();
            Description = descripton ?? throw new ArgumentNullException();
            UrlSlug = urlSlug ?? throw new ArgumentNullException();
            Image = image ?? throw new ArgumentNullException();
        }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }//base 64
        private Collection<Product> _products=new Collection<Product>();
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
        public ICollection<SubCategory> SubCategories { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
    }
}
