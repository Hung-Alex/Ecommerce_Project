using Domain.Entities.Category;

namespace Infrastructure.Data.Seed
{
    public class SeedData : ISeed
    {
        public Task InitData()
        {
            throw new NotImplementedException();
        }
        private IEnumerable<Categories> CategoryParrents()
        {
            IEnumerable<Categories> categories = new List<Categories>()
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
            return categories;
        }
        private IEnumerable<Categories> SubCategoryLv2(IEnumerable<Categories> Parrent)
        {
            var Subcategories
            
        }
    }
}
