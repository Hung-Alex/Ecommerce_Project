﻿using Domain.Common;
using Domain.Entities.Products;
using Domain.Shared;


namespace Domain.Entities.Images
{
    public class Image : BaseEntity, IDatedModification, IAggregateRoot
    {
        public Image() : base() { }
        public string ImageUrl { get; set; }
        public string ImageExtension {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}