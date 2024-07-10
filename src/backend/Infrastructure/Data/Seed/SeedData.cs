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
            var parrentCategory = CategoryParrents();
            await _dbContext.AddRangeAsync(parrentCategory);
            //foreach (var category in parrentCategory)//16
            //{
            //    foreach (var item in SubCategoryLv2())//10
            //    {
            //        item.ParrentId = category.Id;
            //        await _dbContext.AddAsync(item);
            //        foreach (var subitem in SubCategoryLv3())//7
            //        {
            //            subitem.ParrentId = item.Id;
            //            await _dbContext.AddAsync(subitem);
            //        }
            //    }
            //}

            await _dbContext.SaveChangesAsync();
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
        //private IEnumerable<Categories> CategoryParrents()
        //{
        //    IEnumerable<Categories> subCategories = new List<Categories>()
        //    {
        //        new Categories() {Id=Guid.NewGuid(),Name="Laptop",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Laptop Gaming",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="PC GVN",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Main, CPU, VGA",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Case, Nguồn, Tản",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Ổ cứng, RAM, Thẻ nhớ",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Loa, Micro, Webcam",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Màn hình",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Bàn phím",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Chuột + Lót chuột",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Tai Nghe",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Ghế - Bàn",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Phần mềm, mạng",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Handheld, Console",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Phụ kiện (Hub, sạc, cáp..)",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Thủ thuật - Giải đáp",Description="",Image="",UrlSlug=""},
        //    };
        //    return subCategories;
        //}
        //private IEnumerable<Categories> SubCategoryLv2()
        //{
        //    IEnumerable<Categories> Subcategories = new List<Categories>()
        //    {
        //        new Categories() {Id=Guid.NewGuid(),Name="Thương hiệu",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Giá Bán",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="CPU-Intel-AMD",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Nhu cầu sử dụng",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Linh phụ kiện laptop ",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Laptop Asus",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Laptop Acer",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Laptop MSI",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Laptop DELL",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="Chuột + Lót chuột",Description="",Image="",UrlSlug=""},
        //    };
        //    return Subcategories;

        //}
        //private IEnumerable<Categories> SubCategoryLv3()
        //{
        //    IEnumerable<Categories> Subcategories = new List<Categories>()
        //    {
        //        new Categories() {Id=Guid.NewGuid(),Name="ASUS",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="ACER",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="MSI",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="LENOVO",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="DELL ",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="HP - Pavilion",Description="",Image="",UrlSlug=""},
        //        new Categories() {Id=Guid.NewGuid(),Name="LG - Gram",Description="",Image="",UrlSlug=""},
        //    };
        //    return Subcategories;

        //}
    }
}
