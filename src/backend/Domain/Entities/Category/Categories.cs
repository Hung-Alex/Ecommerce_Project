﻿using Domain.Common;
using Domain.Entities.Products;
using Domain.Entities.Users;
using Domain.Shared;


namespace Domain.Entities.Category
{
    public class Categories : BaseEntity, IDatedModification, IAggregateRoot, ICreatedAndUpdatedBy, ISoftDelete
    {
        public Categories() { }
        private Categories(string name, string descripton, string urlSlug, string image, Guid? parrentId) : base()
        {
            Name = name ?? throw new ArgumentNullException();
            Description = descripton ?? throw new ArgumentNullException();
            UrlSlug = urlSlug ?? throw new ArgumentNullException();
            Image = image ?? throw new ArgumentNullException();
            ParrentId = parrentId;
        }
        public string Name { get; set; }
        public Categories ParentCategory { get; set; }
        public Guid? ParrentId { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }//base 64g
        public virtual ICollection<Categories> SubCategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public virtual User UpdatedByUser { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
