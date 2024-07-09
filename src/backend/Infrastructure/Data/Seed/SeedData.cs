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
            foreach (var category in parrentCategory)//16
            {
                foreach (var item in SubCategoryLv2())//10
                {
                    item.ParrentId = category.Id;
                    await _dbContext.AddAsync(item);
                    foreach (var subitem in SubCategoryLv3())//7
                    {
                        subitem.ParrentId = item.Id;
                        await _dbContext.AddAsync(subitem);
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
        }
        private IEnumerable<Categories> CategoryParrents()
        {
            IEnumerable<Categories> subCategories = new List<Categories>()
            {
                new Categories() {Id=Guid.NewGuid(),Name="Laptop",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Laptop Gaming",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="PC GVN",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Main, CPU, VGA",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Case, Nguồn, Tản",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Ổ cứng, RAM, Thẻ nhớ",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Loa, Micro, Webcam",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Màn hình",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Bàn phím",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Chuột + Lót chuột",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Tai Nghe",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Ghế - Bàn",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Phần mềm, mạng",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Handheld, Console",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Phụ kiện (Hub, sạc, cáp..)",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Thủ thuật - Giải đáp",Description="",Image="",UrlSlug=""},
            };
            return subCategories;
        }
        private IEnumerable<Categories> SubCategoryLv2()
        {
            IEnumerable<Categories> Subcategories = new List<Categories>()
            {
                new Categories() {Id=Guid.NewGuid(),Name="Thương hiệu",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Giá Bán",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="CPU-Intel-AMD",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Nhu cầu sử dụng",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Linh phụ kiện laptop ",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Laptop Asus",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Laptop Acer",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Laptop MSI",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Laptop DELL",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="Chuột + Lót chuột",Description="",Image="",UrlSlug=""},
            };
            return Subcategories;

        }
        private IEnumerable<Categories> SubCategoryLv3()
        {
            IEnumerable<Categories> Subcategories = new List<Categories>()
            {
                new Categories() {Id=Guid.NewGuid(),Name="ASUS",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="ACER",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="MSI",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="LENOVO",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="DELL ",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="HP - Pavilion",Description="",Image="",UrlSlug=""},
                new Categories() {Id=Guid.NewGuid(),Name="LG - Gram",Description="",Image="",UrlSlug=""},
            };
            return Subcategories;

        }
    }
}
