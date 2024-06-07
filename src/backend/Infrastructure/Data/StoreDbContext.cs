using Domain.Entities;
using Domain.Entities.Brands;
using Domain.Entities.Carts;
using Domain.Entities.Category;
using Domain.Entities.Comments;
using Domain.Entities.Coupons;
using Domain.Entities.Images;
using Domain.Entities.Orders;
using Domain.Entities.Payments;
using Domain.Entities.Posts;
using Domain.Entities.Products;
using Domain.Entities.Rattings;
using Domain.Entities.Slides;
using Domain.Entities.SubCategories;
using Domain.Entities.Tags;
using Domain.Entities.WishLists;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreDbContext : IdentityDbContext<CustomUser,IdentityRole<Guid>,Guid>
    {
        public StoreDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<SlidesImage> SlidesImages { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<PostTags> PostTags { get; set; }
        public DbSet<Ratting> Rattings { get; set; }
        public DbSet<ProductSkus> ProductSkus { get; set; }
        public DbSet<ProductSubCategory> ProductSubCategories { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<CouponProduct> CouponProducts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
