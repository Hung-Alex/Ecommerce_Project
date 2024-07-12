using Domain.Entities.Brands;
using Domain.Entities.Category;

namespace Infrastructure.Data.Seed
{
    public class SeedData : ISeed
    {
        private readonly StoreDbContext _dbContext;
        public SeedData(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task InitData()
        {
            _dbContext.Database.EnsureCreated();
            if (_dbContext.Categories.Any())
            {
                return;
            }
            var brandsData = BrandsInit();
            await _dbContext.AddRangeAsync(brandsData);
            var parrentCategory = CategoryParrents();
            await _dbContext.AddRangeAsync(parrentCategory);
            int count = 0;
            foreach (var category in parrentCategory)//16
            {

                _dbContext.AddRange(SubCategories(category.Id, category.Name + count));
                count++;
            }

            await _dbContext.SaveChangesAsync();
        }
        private IEnumerable<Brand> BrandsInit()
        {
            IEnumerable<Brand> vegetableBrands = new List<Brand>()
            {
                new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = "Fresh & Green",
                    UrlSlug = "fresh-green",
                    Description = "Providing fresh and high-quality vegetables directly from farms to your table.",
                    Image = "[Image of Fresh & Green vegetables]" // Replace with actual image URL
                },
                new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = "Organic Valley",
                    UrlSlug = "organic-valley",
                    Description = "Offering a wide range of organic vegetables grown sustainably and without harmful chemicals.",
                    Image = "[Image of Organic Valley vegetables]" // Replace with actual image URL
                },
                // ... Add more brands from your sample data ...
                new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = "Steamies-1",
                    UrlSlug = "steamies-1",
                    Description = "Steamed vegetables packed with freshness and nutrients for a healthy and tasty side dish.",
                    Image = "[Image of Steamies vegetables]" // Replace with actual image URL
                },
                // ... Add more brands from your sample data ...
                new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = "Steamies-2",
                    UrlSlug = "steamies-2",
                    Description = "Steamed vegetables packed with freshness and nutrients for a healthy and tasty side dish.",
                    Image = "[Image of Steamies vegetables]" // Replace with actual image URL
                },
                // ... Add more brands from your sample data ...
                new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = "Steamie-3",
                    UrlSlug = "steamies-3",
                    Description = "Steamed vegetables packed with freshness and nutrients for a healthy and tasty side dish.",
                    Image = "[Image of Steamies vegetables]" // Replace with actual image URL
                }
            };
            return vegetableBrands;
        }
        private IEnumerable<Categories> CategoryParrents()
        {
            IEnumerable<Categories> subCategories = new List<Categories>()
            {
                new Categories() { Id = Guid.NewGuid(), Name = "Water Spinach", Description = "Water spinach is a popular green vegetable in Vietnam.", Image = "waterspinach.jpg", UrlSlug = "water-spinach" },
                new Categories() { Id = Guid.NewGuid(), Name = "Napa Cabbage", Description = "Napa cabbage is a type of Chinese cabbage, commonly used to make kimchi.", Image = "napacabbage.jpg", UrlSlug = "napa-cabbage" },
                new Categories() { Id = Guid.NewGuid(), Name = "Broccoli", Description = "Broccoli is a green vegetable that is often eaten steamed or raw.", Image = "broccoli.jpg", UrlSlug = "broccoli" },
                new Categories() { Id = Guid.NewGuid(), Name = "Spinach", Description = "Spinach is a leafy green vegetable that is rich in iron.", Image = "spinach.jpg", UrlSlug = "spinach" },
                new Categories() { Id = Guid.NewGuid(), Name = "Carrot", Description = "Carrots are root vegetables that are often orange in color.", Image = "carrot.jpg", UrlSlug = "carrot" },
                new Categories() { Id = Guid.NewGuid(), Name = "Tomato", Description = "Tomatoes are red fruits that are commonly used in salads and cooking.", Image = "tomato.jpg", UrlSlug = "tomato" },
                new Categories() { Id = Guid.NewGuid(), Name = "Cucumber", Description = "Cucumbers are long, green vegetables that are often used in salads.", Image = "cucumber.jpg", UrlSlug = "cucumber" },
                new Categories() { Id = Guid.NewGuid(), Name = "Bell Pepper", Description = "Bell peppers are colorful vegetables that are often used in cooking.", Image = "bellpepper.jpg", UrlSlug = "bell-pepper" },
                new Categories() { Id = Guid.NewGuid(), Name = "Zucchini", Description = "Zucchini is a type of summer squash that is often used in cooking.", Image = "zucchini.jpg", UrlSlug = "zucchini" },
                new Categories() { Id = Guid.NewGuid(), Name = "Lettuce", Description = "Lettuce is a leafy green vegetable that is commonly used in salads.", Image = "lettuce.jpg", UrlSlug = "lettuce" }
            };
            return subCategories;
        }
        private IEnumerable<Categories> SubCategories(Guid parrentID, string nameParrent)
        {
            IEnumerable<Categories> subCategories = new List<Categories>()
            {
                new Categories() { Id = Guid.NewGuid(), Name = nameParrent, Description = "Water spinach is a popular green vegetable in Vietnam.", Image = "waterspinach.jpg", UrlSlug = "water-spinach",ParrentId=parrentID },
                new Categories() { Id = Guid.NewGuid(), Name = nameParrent, Description = "Napa cabbage is a type of Chinese cabbage, commonly used to make kimchi.", Image = "napacabbage.jpg", UrlSlug = "napa-cabbage" ,ParrentId=parrentID},
                new Categories() { Id = Guid.NewGuid(), Name = nameParrent, Description = "Broccoli is a green vegetable that is often eaten steamed or raw.", Image = "broccoli.jpg", UrlSlug = "broccoli",ParrentId=parrentID },
                new Categories() { Id = Guid.NewGuid(), Name = nameParrent, Description = "Spinach is a leafy green vegetable that is rich in iron.", Image = "spinach.jpg", UrlSlug = "spinach" ,ParrentId=parrentID},
                new Categories() { Id = Guid.NewGuid(), Name = nameParrent, Description = "Carrots are root vegetables that are often orange in color.", Image = "carrot.jpg", UrlSlug = "carrot" ,ParrentId=parrentID},
                new Categories() { Id = Guid.NewGuid(), Name = nameParrent, Description = "Tomatoes are red fruits that are commonly used in salads and cooking.", Image = "tomato.jpg", UrlSlug = "tomato",ParrentId=parrentID },
                new Categories() { Id = Guid.NewGuid(), Name = nameParrent, Description = "Cucumbers are long, green vegetables that are often used in salads.", Image = "cucumber.jpg", UrlSlug = "cucumber",ParrentId=parrentID },
                new Categories() { Id = Guid.NewGuid(), Name = nameParrent, Description = "Bell peppers are colorful vegetables that are often used in cooking.", Image = "bellpepper.jpg", UrlSlug = "bell-pepper",ParrentId=parrentID },
                new Categories() { Id = Guid.NewGuid(), Name = nameParrent, Description = "Zucchini is a type of summer squash that is often used in cooking.", Image = "zucchini.jpg", UrlSlug = "zucchini" ,ParrentId=parrentID},
                new Categories() { Id = Guid.NewGuid(), Name = nameParrent, Description = "Lettuce is a leafy green vegetable that is commonly used in salads.", Image = "lettuce.jpg", UrlSlug = "lettuce" ,ParrentId=parrentID}
            };
            return subCategories;
        }
    }
}
