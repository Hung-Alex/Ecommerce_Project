using Domain.Entities;
using Domain.Entities.Banners;
using Domain.Entities.Brands;
using Domain.Entities.Carts;
using Domain.Entities.Category;
using Domain.Entities.Comments;
using Domain.Entities.Orders;
using Domain.Entities.Payments;
using Domain.Entities.Posts;
using Domain.Entities.Products;
using Domain.Entities.Rattings;
using Domain.Entities.Slides;
using Domain.Entities.Users;
using Domain.Entities.WishLists;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Persistence.Data
{
    public class StoreDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public StoreDbContext(DbContextOptions options) : base(options) { }
        #region DbSet Entities
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<ApplicationPermission> Permissions { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ratting> Rattings { get; set; }
        public DbSet<Comment> Comments { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
        }      
    }
}
